using Microsoft.AspNetCore.Components;

namespace Squads.Client.Components.Shared.Subscriptions;

public partial class TokenSubscriptionCard
{
    public string Animated { get; set; } = "";
    [Parameter]
    public int Tokens { get; set; } = 0;
}