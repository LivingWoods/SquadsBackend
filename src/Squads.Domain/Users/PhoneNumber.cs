using Ardalis.GuardClauses;
using Squads.Domain.Common;

namespace Squads.Domain.Users;

public class PhoneNumber : ValueObject
{
    private static string _validPhoneNumberRegex = @"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$";

    public string Value { get; } = default!;

    /// <summary>
    /// EF constructor
    /// </summary>
    protected PhoneNumber() { }
    public PhoneNumber(string number)
    {
        number = number.Trim();

        Guard.Against.NullOrWhiteSpace(number, nameof(number));
        Guard.Against.InvalidFormat(number, nameof(number), _validPhoneNumberRegex);

        Value = number.ToLower();
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}