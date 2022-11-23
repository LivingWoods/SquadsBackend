using Microsoft.AspNetCore.Components;
using MudBlazor;
using Squads.Shared.Sessions;

namespace Squads.Client.Components.Shared;

public partial class LabelSpotsLeft
{
    
    [Parameter]
    public int LeftOverSpots { get; set; } = 6;


    private Color GetColorByLeftOverSpots()
    {
        if (LeftOverSpots >= 4)
        {
            return Color.Success;
        }
        if (LeftOverSpots == 0)
        {
            return Color.Error;
        }
        if(LeftOverSpots <= 0)
        {
            return Color.Dark;
        }
        return Color.Warning;
    }
    private string GetBookableSpots()
    {
        if(LeftOverSpots == -1)
        {
            return "NO SESSION";
        }
        return LeftOverSpots == 0 ? "FULL" : $"{LeftOverSpots} spots left";
    }

    
    


}

