using System.Runtime.Serialization;

namespace Squads.Shared.Reservations;

public abstract class ReservationDto
{
    [DataContract] public class Index
    {
        [DataMember(Order = 1)] public int ReservationId { get; set; }
        [DataMember(Order = 2)] public int UserId { get; set; }
        [DataMember(Order = 3)] public int SessionId { get; set; }
    }
}
