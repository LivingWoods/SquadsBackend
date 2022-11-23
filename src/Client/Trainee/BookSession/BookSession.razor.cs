using Squads.Client.Trainee.BookSession.Components;
using Microsoft.AspNetCore.Components;
using Squads.Shared.Sessions;
using MudBlazor.Extensions;
using Squads.Shared.Users;
using FluentDateTime;
using MudBlazor;
using Squads.Client.Shared;
using Squads.Shared.Reservations;

namespace Squads.Client.Trainee.BookSession;


public partial class BookSession
{
    [Inject] public FakeAuthenticationProvider AuthProvider { get; set; } = default!;
    [Inject] public ISessionService SessionService { get; set; } = default!;
    [Inject] public IUserService UserService { get; set; } = default!;

    private IEnumerable<SessionDto.Index>? _sessions { get; set; }
    private List<SessionDto.Index>? _sessionsWithEmpty { get; set; }
    private UserDto.Detail _user { get; set; } = default!;

    private DateTime _beginning = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
    private DateTime _ending = DateTime.Now.AddDays(7).StartOfWeek(DayOfWeek.Monday).AddMilliseconds(-1);
    private bool _nextWeekSelected = false;

    private DateTime[]? _datesOfWeek;

    protected override async Task OnInitializedAsync()
    {
        await GetSessionsFromCurrentWeek();

        int userId = Convert.ToInt32(AuthProvider?.Current.FindFirst("Id")?.Value);

        UserReply.DetailReply reply = await UserService.GetUserByUserId(new UserRequest.IdRequest
        {
            UserId = userId
        });

        _user = reply.User;
    }

    private async Task GetSessionsFromCurrentWeek()
    {
        _nextWeekSelected = false;

        SessionReply.IndexReply reply = await SessionService.GetSessionsFromCurrentWeek(new SessionRequest.IndexRequest());
        _sessions = reply.Sessions;
        _sessionsWithEmpty = GetSessionsWithEmptyDays();
        
        StateHasChanged();
    }

    private async Task GetSessionsFromNextWeek()
    {
        _nextWeekSelected = true;

        SessionReply.IndexReply reply = await SessionService.GetSessionsFromNextWeek(new SessionRequest.IndexRequest());
        _sessions = reply.Sessions;
        _sessionsWithEmpty = GetSessionsWithEmptyDays();

        StateHasChanged();
    }


    public List<SessionDto.Index> GetSessionsWithEmptyDays()
    {
        _datesOfWeek = Enumerable.Range(0, 7).Select(num => _beginning.AddDays(num)).ToArray();
        _sessionsWithEmpty = new();
        foreach(DateTime dt in _datesOfWeek)
        {
            List<SessionDto.Index> cSessions = _sessions.Where(s => s.StartDate.ToShortDateString() == dt.ToShortDateString()).ToList();

            if (cSessions.Any())
            {
                foreach (SessionDto.Index s in cSessions)
                {
                    _sessionsWithEmpty.Add(s);
                }
            } 
            else
            {
                SessionDto.Index s = new();
                s.StartDate = dt;
                s.Id = -1;
                s.AmountOfReservations = -1;
                _sessionsWithEmpty.Add(s);
            }
        }

        return _sessionsWithEmpty;
    }

    //Sign up dialog
    DialogOptions maxWidth = new DialogOptions() { MaxWidth = MaxWidth.Medium, FullWidth = true };
    private void OpenDialog(SessionDto.Index CurrentSession)
    {

        var options = new DialogOptions() { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Medium, FullWidth = true, Position = DialogPosition.Center, CloseButton = true };

        var parameterCurrentSession = new DialogParameters();
        parameterCurrentSession.Add("CurrentSession", CurrentSession);
        parameterCurrentSession.Add("StateTest", EventCallback.Factory.Create(this, StateTest));
        parameterCurrentSession.Add("ReserveSession", EventCallback.Factory.Create<int>(this, ReserveSession));

        Dialog.Show<ConfirmationSignUp>("Session confirmation", parameterCurrentSession, options);
    }

    private async void CancelReservation(int sessionId)
    {
        //reservationdto.index res = _reservations.where(x => x.session.id == sessionid).first();
        //await reservationservice.deletereservationasync(res.id);
        ////await sessionservice.addspots(sessionid, 1);
        //updatereservations();
        ReservationDto.Index? reservation = _user.Reservations.FirstOrDefault(r => r.SessionId == sessionId);

        await UserService.CancelReservationByReservationId(new UserRequest.ReservationRequest
        {
            ReservationId = reservation?.ReservationId,
            SessionId = sessionId,
            UserId = _user.UserId
        });
    }

    private async void ReserveSession(int sessionId)
    {
        var reserveReply = await UserService.ReserveSession(new UserRequest.ReservationRequest
        {
            ReservationId = null,
            SessionId = sessionId,
            UserId = Convert.ToInt32(AuthProvider?.Current.FindFirst("Id")?.Value)
        });

        int userId = Convert.ToInt32(AuthProvider?.Current.FindFirst("Id")?.Value);

        UserReply.DetailReply reply = await UserService.GetUserByUserId(new UserRequest.IdRequest
        {
            UserId = reserveReply.UserId
        });

        _user = reply.User;

        StateHasChanged();
    }

    //public async void UpdateReservations()
    //{
    //    _reservations = await ReservationService.GetFutureReservationsOfTrainee(1);
    //}

    public void StateTest()
    {
        StateHasChanged();
    }
}

