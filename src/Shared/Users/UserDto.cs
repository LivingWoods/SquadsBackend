using Squads.Shared.Reservations;
using System.Runtime.Serialization;

namespace Squads.Shared.Users;

public abstract class UserDto
{
    [DataContract] public class Index
    {
        [DataMember(Order = 1)] public int UserId { get; set; } = default!;
        [DataMember(Order = 2)] public string FirstName { get; set; } = default!;
        [DataMember(Order = 3)] public string LastName { get; set; } = default!;
    }
    [DataContract] public class Detail
    {
        [DataMember(Order = 1)] public int UserId { get; set; } = default!;
        [DataMember(Order = 2)] public string FirstName { get; set; } = default!;
        [DataMember(Order = 3)] public string LastName { get; set; } = default!;
        [DataMember(Order = 4)] public DateTime BirthDate { get; set; }
        [DataMember(Order = 5)] public string Email { get; set; } = default!;
        [DataMember(Order = 6)] public string PhoneNumber { get; set; } = default!;
        [DataMember(Order = 7)] public AddressDto.Index Address { get; set; } = default!;
        [DataMember(Order = 8)] public bool HasSubscription { get; set; }
        [DataMember(Order = 9)] public string? PhysicalIssues { get; set; }
        [DataMember(Order = 10)] public string? DrugsUsed { get; set; }
        [DataMember(Order = 11)] public int Length { get; set; }
        [DataMember(Order = 12)] public bool OptedInOnNewsletter { get; set; }
        [DataMember(Order = 13)] public bool OptedInOnWhatsapp { get; set; }
        [DataMember(Order = 14)] public bool HasSignedHouseRules { get; set; }
        [DataMember(Order = 15)] public bool IsTrainer { get; set; }
        [DataMember(Order = 16)] public int AmountOfPlannedReservation { get; set; }
        [DataMember(Order = 17)] public IEnumerable<ReservationDto.Index> Reservations { get; set; } = new List<ReservationDto.Index>();
    }
    [DataContract] public class Mutate
    {
        [DataMember(Order = 1)] public string FirstName { get; set; } = default!;
        [DataMember(Order = 2)] public string LastName { get; set; } = default!;
        [DataMember(Order = 3)] public DateTime BirthDate { get; set; }
        [DataMember(Order = 4)] public string Email { get; set; } = default!;
        [DataMember(Order = 5)] public string PhoneNumber { get; set; } = default!;
        [DataMember(Order = 6)] public AddressDto.Index Address { get; set; } = default!;
        [DataMember(Order = 7)] public string? PhysicalIssues { get; set; }
        [DataMember(Order = 8)] public string? DrugsUsed { get; set; }
        [DataMember(Order = 9)] public int Length { get; set; }
        [DataMember(Order = 10)] public bool OptedInOnNewsletter { get; set; }
        [DataMember(Order = 11)] public bool OptedInOnWhatsapp { get; set; }
        [DataMember(Order = 12)] public bool HasSignedHouseRules { get; set; }
        [DataMember(Order = 13)] public bool IsTrainer { get; set; }
    }
}
