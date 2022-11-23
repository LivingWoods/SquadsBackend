using System;
using Microsoft.AspNetCore.Components;
using Squads.Shared.Users;
using static Squads.Shared.Users.UserReply;

namespace Squads.Client.Trainee;

public partial class WeekOverview
{
    [Inject] public IUserService UserService { get; set; } = default!;

    private List<UserDto.WeekItem>? weekItems;

    protected override async Task OnInitializedAsync()
    {
        await LoadWeekItems();
    }

    private async Task ReserveSession(int sessionId)
    {
        var reserveReply = await UserService.ReserveSession(new UserRequest.ReservationRequest
        {
            SessionId = sessionId,
            UserId = 1,
        });
        await LoadWeekItems();
    }

    private async Task LoadWeekItems()
    {
        UserRequest.WeekOverview request = new UserRequest.WeekOverview
        {
            UserId = 1,
        };
        var response = await UserService.GetWeekOverview(request);
        weekItems = response.WeekItems;
    }
}

