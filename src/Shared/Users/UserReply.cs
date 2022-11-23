using System.Runtime.Serialization;
using Squads.Shared.Reservations;

namespace Squads.Shared.Users;

public abstract class UserReply
{
    [DataContract] public class IndexReply
    {
        [DataMember(Order = 1)] public IEnumerable<UserDto.Index> Users { get; set; } = new List<UserDto.Index>();
    }
    [DataContract] public class IdReply
    {
        [DataMember(Order = 1)] public int UserId { get; set; }
    }
    [DataContract] public class ReservationIdReply
    {
        [DataMember(Order = 1)] public int ReservationId { get; set; }
    }
    [DataContract] public class DetailReply
    {
        [DataMember(Order = 1)] public UserDto.Detail User { get; set; } = default!;
    }

    [DataContract]
    public class PlannedReservations
    {
        [DataMember(Order = 1)] public IEnumerable<ReservationDto.Index> Reservations { get; set; } = new List<ReservationDto.Index>();

    }

    [DataContract]
    public class WeekOverview
    {
        [DataMember(Order = 1)] public List<UserDto.WeekItem> WeekItems { get; set; } = new List<UserDto.WeekItem>();

    }
}
