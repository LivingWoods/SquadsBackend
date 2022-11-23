using Squads.Domain.Sessions;
using Squads.Domain.Common;
using Ardalis.GuardClauses;
using Squads.Domain.Users;

namespace Squads.Domain.Reservations;

public class Reservation : Entity
{
    public int UserId { get; }
    public User User { get; }
    public int SessionId { get; }
    public Session Session { get; }
    public DateTime? CanceledOn { get; private set; }

    public bool IsCanceled => CanceledOn is not null;

    /// <summary>
    /// EF constructor
    /// </summary>
    protected Reservation() { }
    /// <summary>
    /// Validates and creates a new reservation
    /// </summary>
    /// <param name="user">The user who wants to book a spot</param>
    /// <param name="session">The session for which the users wants to book a spot</param>
    public Reservation(User user, Session session)
    {
        User = Guard.Against.Null(user, nameof(user));
        Session = Guard.Against.Null(session, nameof(session));
        Guard.Against.OutOfRange(Session.AmountOfTotalReservations + 1, nameof(Session.Reservations), 0, 6);
    }

    /// <summary>
    /// Cancels the reservation
    /// </summary>
    public void CancelReservation()
    {
        CanceledOn = DateTime.UtcNow;
    }
}