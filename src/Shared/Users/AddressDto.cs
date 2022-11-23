using System.Runtime.Serialization;

namespace Squads.Shared.Users;

public abstract class AddressDto
{
    [DataContract] public class Index
    {
        [DataMember(Order = 1)] public string AddressLine1 { get; set; } = default!;
        [DataMember(Order = 2)] public string? AddressLine2 { get; set; } = default!;
        [DataMember(Order = 3)] public string ZipCode { get; set; } = default!;
        [DataMember(Order = 4)] public string City { get; set; } = default!;
    }
}
