using Squads.Domain.PersonalRecords;
using Squads.Domain.Subscriptions;
using Squads.Domain.Reservations;
using Squads.Domain.Measurements;
using Squads.Domain.Exercises;
using Squads.Domain.Sessions;
using Squads.Domain.Common;
using Squads.Domain.Tokens;
using Ardalis.GuardClauses;

namespace Squads.Domain.Users;

public class User : Entity
{
    private List<Reservation> _reservations = new();
    private List<Measurement> _measurements = new();
    private List<PersonalRecord> _personalRecords = new();
    private List<Token> _tokens = new();

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public Email Email { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public Address Address { get; set; }
    public Subscription? Subscription { get; private set; }
    public string? PhysicalIssues { get; set; }
    public string? DrugsUsed { get; set; }
    public int Length { get; set; }
    public bool OptedInOnNewsletter { get; set; }
    public bool OptedInOnWhatsapp { get; set; }
    public bool HasSignedHouseRules { get; set; }
    public bool IsTrainer { get; set; }

    /// <summary>
    /// Returns the reservations list as an immutable read only list
    /// </summary>
    public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();
    /// <summary>
    /// Returns only the reservations that lie in the future list as an immutable read only list
    /// </summary>
    public IReadOnlyList<Reservation> PlannedReservations => _reservations.FindAll(r => r.Session.StartDate > DateTime.UtcNow).AsReadOnly();
    /// <summary>
    /// Returns only the reservations that lie in the past list as an immutable read only list
    /// </summary>
    public IReadOnlyList<Reservation> PassedReservations => _reservations.FindAll(r => r.Session?.EndDate < DateTime.UtcNow).AsReadOnly();
    /// <summary>
    /// Returns the amount of planned reservations for this user
    /// </summary>
    public int AmountOfPlannedReservations => _reservations.Count(r => r.Session.StartDate > DateTime.UtcNow);
    /// <summary>
    /// Returns the amount of reservations in the past for this user
    /// </summary>
    public int AmoutOfPassedReservations => _reservations.Count(_r => _r.Session.EndDate < DateTime.UtcNow);
    /// <summary>
    /// Returns the amount of all reservations for this user
    /// </summary>
    public int AmountOfTotalReservations => _reservations.Count;
    /// <summary>
    /// Returns the tokens as an immutable read only list
    /// </summary>
    public IReadOnlyList<Token> Tokens => _tokens.AsReadOnly();
    /// <summary>
    /// Returns the amount of available tokens
    /// </summary>
    public int AmountOfAvailableTokens => _tokens.Count(t => !t.IsUsed);
    /// <summary>
    /// Returns the first available token or null when there are no more available tokens
    /// </summary>
    public Token? FirstAvailableToken => _tokens.FirstOrDefault(t => !t.IsUsed);
    /// <summary>
    /// Returns the amount of unpaid tokens
    /// </summary>
    public int AmountOfUnpaidTokens => _tokens.Count(t => !t.IsPaid);
    /// <summary>
    /// Returns wether or not the user is able to book a reservation
    /// </summary>
    public bool HasActiveSubscription => DateTime.UtcNow >= Subscription?.GetLatestSubscriptionLine.ValidFrom && DateTime.UtcNow <= Subscription?.GetLatestSubscriptionLine.ValidTill;
    /// <summary>
    /// Returns the measurements as an immutable read only list
    /// </summary>
    public IReadOnlyList<Measurement> Measurements => _measurements.AsReadOnly();
    /// <summary>
    /// Returns the latest measurement if measurements exists otherwise it returns null
    /// </summary>
    public Measurement? LatestMeasurement => _measurements.LastOrDefault();
    /// <summary>
    /// Returns the personal records as an immutable read only list
    /// </summary>
    public IReadOnlyList<PersonalRecord> PersonalRecords => _personalRecords.AsReadOnly();

    /// <summary>
    /// EF constructor
    /// </summary>
    protected User() { }

    public User(string firstName, string lastName, DateTime birthDate, Email email, PhoneNumber phoneNumber, Address address, string? physicalIssues, string? drugsUsed, int length, bool optedInOnNewsletter, bool optedInOnWhatsapp, bool hasSignedHouseRules, bool isTrainer)
    {
        FirstName = Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
        LastName = Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
        BirthDate = birthDate;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        Subscription = null;
        PhysicalIssues = physicalIssues;
        DrugsUsed = drugsUsed;
        Length = Guard.Against.NegativeOrZero(length, nameof(length));
        OptedInOnNewsletter = optedInOnNewsletter;
        OptedInOnWhatsapp = optedInOnWhatsapp;
        HasSignedHouseRules = hasSignedHouseRules;
        IsTrainer = isTrainer;
        _tokens.Add(new Token(new Payment(1)));
    }

    /// <summary>
    /// Creates a new reservation for a given session and adds it to the reservations of the user
    /// </summary>
    /// <param name="session">The session for which a new reservation is created</param>
    public void ReserveSession(Session session)
    {
        if (AmountOfPlannedReservations < 3)
        {
            if (HasActiveSubscription)
            {
                _reservations.Add(new Reservation(this, session));
            }
            else if (AmountOfAvailableTokens > 0)
            {
                _reservations.Add(new Reservation(this, session));
                FirstAvailableToken?.UseToken();
            }
        }
    }

    public void CancelReservation(int reservationId)
    {
        Reservation? reservation = _reservations.FirstOrDefault(r => r.Id == reservationId);

        if (reservation is not null)
        {
            reservation.CancelReservation();
        }
    }

    /// <summary>
    /// Adds the specified amount of tokens to the user
    /// </summary>
    /// <param name="amountOfTokens">The amount of tokens</param>
    /// <param name="isPaid">Wether the tokens have already been paid for</param>
    public void OrderTokens(int amountOfTokens, bool isPaid, double? paymentAmount)
    {
        for (int i = 0; i < amountOfTokens; i++)
        {
            _tokens.Add(new Token(isPaid && paymentAmount is not null ? new Payment((double)paymentAmount) : null));
        }
    }

    /// <summary>
    /// Checks wether or not the user already has a subscription and adds a new subscription line
    /// </summary>
    /// <param name="validFrom"></param>
    /// <param name="isPaid"></param>
    public void AddSubscription(DateTime validFrom, bool isPaid, double? paymentAmount)
    {
        if (Subscription is null)
        {
            Subscription = new Subscription();
        }

        if (Subscription.IsCanceled)
        {
            Subscription.ReactivateSubscription();
        }

        Subscription.RenewSubscription(validFrom, isPaid && paymentAmount is not null ? new Payment((double)paymentAmount) : null);
    }

    /// <summary>
    /// Cancels the subscription of the user
    /// </summary>
    public void CancelSubscription()
    {
        if (Subscription is not null && !Subscription.IsCanceled)
        {
            Subscription.CancelSubscription();
        }
    }

    /// <summary>
    /// Adds a new measurement to the measurements list
    /// </summary>
    /// <param name="weight"></param>
    /// <param name="fatPercentage"></param>
    /// <param name="musclePercentage"></param>
    /// <param name="waistCircumference"></param>
    public void AddMeasurement(double weight, double fatPercentage, double musclePercentage, double waistCircumference)
    {
        _measurements.Add(new Measurement(weight, fatPercentage, musclePercentage, waistCircumference));
    }

    /// <summary>
    /// Adds a new personal record to the personal records list
    /// </summary>
    /// <param name="achievedOn"></param>
    /// <param name="reps"></param>
    /// <param name="weightsUsed"></param>
    /// <param name="exercise"></param>
    public void AddPersonalRecord(DateTime achievedOn, int reps, double weightsUsed, Exercise exercise)
    {
        _personalRecords.Add(new PersonalRecord(achievedOn, reps, weightsUsed, exercise));
    }
}