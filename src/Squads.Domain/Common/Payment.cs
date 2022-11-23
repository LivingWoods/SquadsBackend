using Ardalis.GuardClauses;

namespace Squads.Domain.Common;

public class Payment : ValueObject
{
	public DateTime PaidOn { get; }
	public double Amount { get; }

	protected Payment() { }

	public Payment(double amount)
	{
		Amount = Guard.Against.NegativeOrZero(amount, nameof(amount));
		PaidOn = DateTime.UtcNow;
	}

	protected override IEnumerable<object?> GetEqualityComponents()
	{
		yield return Math.Round(Amount, 2);
		yield return PaidOn;
	}
}
