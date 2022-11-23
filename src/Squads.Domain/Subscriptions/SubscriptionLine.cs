using Ardalis.GuardClauses;
using Squads.Domain.Common;

namespace Squads.Domain.Subscriptions;

public class SubscriptionLine : Entity
{
    public DateTime ValidFrom { get; }
    public DateTime ValidTill { get; }
    public Payment? Payment { get; private set; }

    /// <summary>
    /// Returns wether or not the token has been paid
    /// </summary>
    public bool IsPaid => Payment is not null;
    /// <summary>
    /// Validates and creates a new subscription line
    /// </summary>
    /// <param name="validFrom">The date from which the subscription line is valid</param>
    /// <param name="payment">Wether or not this subscription line has been paid for already</param>
    protected SubscriptionLine() { }
    public SubscriptionLine(DateTime validFrom, Payment? payment)
    {
        ValidFrom = Guard.Against.OutOfRange(validFrom, nameof(validFrom), DateTime.UtcNow, DateTime.MaxValue);
        ValidTill = validFrom.AddMonths(1);
        Payment = payment;
    }

    /// <summary>
    /// Adds a payment for the token
    /// </summary>
    /// <param name="payment">The payment for the token</param>
    public void AddPayment(Payment payment)
    {
        if (!IsPaid)
        {
            Payment = payment;
        }
    }
}
