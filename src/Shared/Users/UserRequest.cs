using System.Runtime.Serialization;

namespace Squads.Shared.Users;

public abstract class UserRequest
{
    [DataContract] public class IndexRequest { }
    [DataContract] public class IdRequest
    {
        [DataMember(Order = 1)] public int UserId { get; set; }
    }
    [DataContract] public class MutateRequest
    {
        [DataMember(Order = 1)] public int? UserId { get; set; }
        [DataMember(Order = 2)] public UserDto.Mutate User { get; set; } = default!;
    }
    [DataContract] public class ReservationRequest
    {
        [DataMember(Order = 1)] public int? ReservationId { get; set; }
        [DataMember(Order = 2)] public int UserId { get; set; }
        [DataMember(Order = 3)] public int SessionId { get; set; }
    }

    [DataContract]
    public class WeekOverview
    {
        [DataMember(Order = 1)] public int UserId { get; set; }
        [DataMember(Order = 2)] public DateTime StartDate { get; set; } = DateTime.UtcNow;
    }
}
