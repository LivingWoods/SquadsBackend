using Squads.Domain.Common;

namespace Squads.Domain.Tokens;

public class Token : Entity
{
    public DateTime? UsedOn { get; private set; }
    public Payment? Payment { get; private set; }

    /// <summary>
    /// Returns wether or not the token has been paid
    /// </summary>
    public bool IsPaid => Payment is not null;
    /// <summary>
    /// Returns wether or not the token has been used
    /// </summary>
    public bool IsUsed => UsedOn is not null;

    /// <summary>
    /// EF constructor
    /// </summary>
    private Token() { }
    /// <summary>
    /// Validates and creates a new token
    /// </summary>
    /// <param name="payment">Wether or not the token has already been paid for</param>
    public Token(Payment? payment)
    {
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

    /// <summary>
    /// Sets the IsUsed property to true
    /// </summary>
    public void UseToken()
    {
        if (!IsUsed)
        {
            UsedOn = DateTime.UtcNow;
        }
    }
}
