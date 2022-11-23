using Microsoft.AspNetCore.Components;

namespace Squads.Client.Components.Shared.Subscriptions;

public partial class SubscriptionCard
{
    [Parameter]
    public bool ShowBack { get; set; } = false;
    public string Animated { get; set; } = "";
    [Parameter] public RenderFragment Front { get; set; } = default!;
    [Parameter] public RenderFragment Back { get; set; } = default!;
    [Parameter] public bool IsActive{ get; set; } = default!;

    public async void ChangeInformation()
    {
        Animated = "rotate-in";
        ShowBack = !ShowBack;
        StateHasChanged();
        await Task.Delay(500);
        Animated = "";
        StateHasChanged();
    }
}