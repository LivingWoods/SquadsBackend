using System.Runtime.Serialization;

namespace Squads.Shared.Sessions;

public abstract class SessionRequest
{
    [DataContract] public class IndexRequest { }
    [DataContract] public class IdRequest
    {
        [DataMember(Order = 1)] public int SessionId { get; set; }
    }
    [DataContract] public class MutateRequest
    {
        [DataMember(Order = 1)] public int? SessionId { get; set; }
        [DataMember(Order = 2)] public SessionDto.Mutate Session { get; set; } = default!;
    }
}
