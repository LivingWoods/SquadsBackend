using System.Runtime.Serialization;
using Squads.Shared.Reservations;
using Squads.Shared.Users;

namespace Squads.Shared.Sessions;

public abstract class SessionDto
{
    [DataContract] public class Index
    {
        [DataMember(Order = 1)] public int Id { get; set; }
        [DataMember(Order = 2)] public DateTime StartDate { get; set; }
        [DataMember(Order = 3)] public DateTime EndDate { get; set; }
        [DataMember(Order = 4)] public string SessionType { get; set; } = default!;
        [DataMember(Order = 5)] public string Trainer { get; set; } = default!;
        [DataMember(Order = 6)] public bool CanBeReserved { get; set; }
        [DataMember(Order = 7)] public bool ReservationCanBeCanceled { get; set; }
        [DataMember(Order = 8)] public int AmountOfReservations { get; set; }
    }
    [DataContract] public class Detail
    {
        [DataMember(Order = 1)] public int Id { get; set; }
        [DataMember(Order = 2)] public DateTime StartDate { get; set; }
        [DataMember(Order = 3)] public DateTime EndDate { get; set; }
        [DataMember(Order = 4)] public string SessionType { get; set; } = default!;
        [DataMember(Order = 5)] public IEnumerable<ReservationDto.Index> Reservations { get; set; } = default!;
        [DataMember(Order = 6)] public IEnumerable<UserDto.Index> Waitlist { get; set; } = default!;
    }
    [DataContract] public class Mutate
    {
        [DataMember(Order = 1)] public DateTime StartDate { get; set; }
        [DataMember(Order = 2)] public DateTime EndDate { get; set; }
        [DataMember(Order = 3)] public string SessionType { get; set; } = default!;
        
        // TODO: add user (trainer)
    }
}
