using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Squads.Client.Shared;

public class FakeAuthenticationProvider : AuthenticationStateProvider
{
    public static ClaimsPrincipal Visitor => new(new ClaimsIdentity(
        new[]
        {
            new Claim(ClaimTypes.Name, "Visitor")
        }));
    public static ClaimsPrincipal Trainee => new(new ClaimsIdentity(
        new[]
        {
            new Claim("Id", "1"),
            new Claim(ClaimTypes.Name, "Michiel Van Herreweghe"),
            new Claim(ClaimTypes.Email, "michiel.vanherreweghe@outlook.com"),
            new Claim(ClaimTypes.Role, "Trainee"),
        }, "Fake Authentication"));
    public static ClaimsPrincipal Trainer => new(new ClaimsIdentity(
        new[]
        {
            new Claim("Id", "2"),
            new Claim(ClaimTypes.Name, "Helleni"),
            new Claim(ClaimTypes.Email, "helleni.squads@outlook.com"),
            new Claim(ClaimTypes.Role, "Trainer"),
        }, "Fake Authentication"));

    public ClaimsPrincipal Current { get; set; } = Trainee;

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.FromResult(new AuthenticationState(Current));
    }

    public void ChangeAuthenticationState(ClaimsPrincipal claimsPrincipal)
    {
        Current = claimsPrincipal;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public bool CheckVisitorOrNot()
    {
        if(Current.FindFirst(ClaimTypes.Name)?.Value == "Visitor")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
