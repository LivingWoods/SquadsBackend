using Microsoft.AspNetCore.Components;
using System;

namespace Squads.Client.Components.Shared;

public partial class NavCard
{
    [Parameter] public string NavCardText { get; set; } = default!;
    [Parameter] public string NavCardImageURL { get; set; } = default!;
    [Parameter] public string NavCardHrefURL { get; set; } = default!;
    [Parameter] public bool IsOutlined { get; set; }

    private string? _styling;

    protected override void OnInitialized()
    {
        _styling = $"background-image: url('{NavCardImageURL}'); background-size: cover; background-repeat: no-repeat; {(IsOutlined ? "border: 2px solid; border-color:white;" : "")}";
    }

    private async void OnClick()
    {
        NavigationManager.NavigateTo(NavCardHrefURL);
    }
}