using MudBlazor;

namespace Squads.Client.Shared;

public partial class MainLayout
{
    MudTheme _squadsTheme = new MudTheme()
    {
        Palette = new Palette()
        {
            Primary = Colors.Shades.Black,
            Secondary = Colors.Shades.White,
            Tertiary = Colors.Amber.Darken4,
        },
        
        Typography = new Typography()
        {
            Default = new Default()
            {
                FontFamily = new[] { "Gotham", "Arial", "sans-serif" }
            }
        }
    };
}