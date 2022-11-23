using Squads.Client.Shared;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;

namespace Squads.Client.Components.Shared;

public partial class SwitchAccountMenu
{
    [Inject] FakeAuthenticationProvider? FakeAuthenticationProvider { get; set; }

    void LoginAsVisitor()
    {
        FakeAuthenticationProvider?.ChangeAuthenticationState(FakeAuthenticationProvider.Visitor);
        log();
    }
    void LoginAsTrainee()
    {
        FakeAuthenticationProvider?.ChangeAuthenticationState(FakeAuthenticationProvider.Trainee);
        log();
    }
    void LoginAsTrainer()
    {
        FakeAuthenticationProvider?.ChangeAuthenticationState(FakeAuthenticationProvider.Trainer);
        log();
    }

    private void log()
    {
        Console.WriteLine($"logged in as: {FakeAuthenticationProvider?.Current.FindFirst(ClaimTypes.Name)?.Value}");
    }
}