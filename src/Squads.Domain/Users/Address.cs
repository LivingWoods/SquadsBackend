using Ardalis.GuardClauses;
using Squads.Domain.Common;

namespace Squads.Domain.Users;

public class Address : ValueObject
{
    public string AddressLine1 { get; }
    public string? AddressLine2 { get; }
    public string ZipCode { get; }
    public string City { get; }

    /// <summary>
    /// EF constructor
    /// </summary>
    protected Address() { }
    public Address(string addressLine1, string? addressLine2, string zipCode, string city)
    {
        AddressLine1 = Guard.Against.NullOrWhiteSpace(addressLine1, nameof(addressLine1));
        AddressLine2 = addressLine2;
        ZipCode = Guard.Against.NullOrWhiteSpace(zipCode, nameof(zipCode));
        City = Guard.Against.NullOrWhiteSpace(city, nameof(city));
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return AddressLine1.ToLower();
        yield return AddressLine2?.ToLower();
        yield return ZipCode.ToLower();
        yield return City.ToLower();
    }
}
