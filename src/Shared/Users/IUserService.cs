using System.ServiceModel;

namespace Squads.Shared.Users;

[ServiceContract] public interface IUserService
{
    /* CREATE */
    [OperationContract] Task<UserReply.IdReply> CreateUser(UserRequest.MutateRequest request);

    /* READ */
    [OperationContract] Task<UserReply.IndexReply> GetAllUsers(UserRequest.IndexRequest request);
    [OperationContract] Task<UserReply.DetailReply> GetUserByUserId(UserRequest.IdRequest request);

    /* UPDATE */
    [OperationContract] Task<UserReply.IdReply> UpdateUserByUserId(UserRequest.MutateRequest request);
    [OperationContract] Task<UserReply.IdReply> ReserveSession(UserRequest.ReservationRequest request);
    [OperationContract] Task CancelReservationByReservationId(UserRequest.ReservationRequest request);

    /* DELETE */
    [OperationContract] Task DeleteUserByUserId(UserRequest.IdRequest request);
    [OperationContract] Task<UserReply.PlannedReservations> GetPlannedReservations(UserRequest.IdRequest request);
    [OperationContract] Task<UserReply.WeekOverview> GetWeekOverview(UserRequest.WeekOverview request);
}
