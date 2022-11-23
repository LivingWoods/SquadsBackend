using System;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using static MudBlazor.CategoryTypes;


namespace Squads.Client.Components.Shared;

public partial class ButtonComponent
{
    [Parameter]
    public String TextButton { get; set; } = "CONFIRM";
    [Parameter]
    public int Width { get; set; } = 10;

    [Parameter]
    public EventCallback OnClickFunction { get; set; }

    [Parameter]
    public string Color { get; set; } = "black";




}
