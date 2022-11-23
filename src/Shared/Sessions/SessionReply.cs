using System.Runtime.Serialization;

namespace Squads.Shared.Sessions;

public abstract class SessionReply
{
    [DataContract] public class IndexReply 
    {
        [DataMember(Order = 1)] public IEnumerable<SessionDto.Index> Sessions { get; set; } = new List<SessionDto.Index>();
    }
    [DataContract] public class IdReply
    {
        [DataMember(Order = 1)] public int SessionId { get; set; }
    }
    [DataContract] public class DetailReply
    {
        [DataMember(Order = 1)] public SessionDto.Detail Session { get; set; } = default!;
    }
}
