using System.ServiceModel;

namespace Squads.Shared.Sessions;

[ServiceContract] public interface ISessionService
{
    /* CREATE */
    [OperationContract] Task<SessionReply.IdReply> CreateNewSession(SessionRequest.MutateRequest request);

    /* READ */
    [OperationContract] Task<SessionReply.IndexReply> GetSessionsFromCurrentWeek(SessionRequest.IndexRequest request);
    [OperationContract] Task<SessionReply.IndexReply> GetSessionsFromNextWeek(SessionRequest.IndexRequest request);
    [OperationContract] Task<SessionReply.DetailReply> GetNextSession(SessionRequest.IndexRequest request);
    [OperationContract] Task<SessionReply.DetailReply> GetSessionBySessionId(SessionRequest.IdRequest request);

    /* UPDATE */
    [OperationContract] Task<SessionReply.IdReply> UpdateSessionBySessionId(SessionRequest.MutateRequest request);
    // TODO: implement AddUserToWaitlist()
    //Task<SessionReply> AddUserToWaitlist(SessionRequest)

    /* DELETE */
    [OperationContract] Task DeleteSessionBySessionId(SessionRequest.IdRequest request);
}
