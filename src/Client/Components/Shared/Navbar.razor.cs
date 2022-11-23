using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using MudBlazor;
using System.Text.RegularExpressions;

namespace Squads.Client.Components.Shared;

public partial class Navbar
{
    private bool _open = false;
    private string _icon = Icons.Material.Filled.Menu;
    private bool _trainerRoute = false;
    private string _title;

    protected override async Task OnInitializedAsync()
    {
        _trainerRoute = NavigationManager.ToBaseRelativePath(NavigationManager.Uri).ToString().Contains("trainer");
        SetTitle();
        NavigationManager.LocationChanged += HandleLocationChanged;
    }

    private void SetTitle()
    {
        switch (NavigationManager.ToBaseRelativePath(NavigationManager.Uri).ToString())
        {
            case "":
                _title = "";
                break;
            case "book":
                _title = "Book a session";
                break;
            case "account":
                _title = "My account";
                break;
            case "my-exercises":
                _title = "My exercises";
                break;
            case "my-health":
                _title = "My health";
                break;
            case var s when new Regex(@"my-health\/\d").IsMatch(s):
                _title = "Health";
                break;
            case "my-reservations":
                _title = "My reservations";
                break;
            case "next-session":
                _title = "Next session";
                break;
            case "trainer/next-session":
                _title = "Next session";
                break;
            default:
                _title = "Title not found";
                break;
        }
    }

    private void ToggleDrawer()
    {
        _open = !_open;
        ToggleIcon();
    }

    private void ToggleIcon()
    {
        _icon = _open ? Icons.Filled.Close : Icons.Material.Filled.Menu;
    }

    private void HandleLocationChanged(object sender, LocationChangedEventArgs e)
    {
        _open = false;
        _icon = Icons.Material.Filled.Menu;
        _trainerRoute = NavigationManager.ToBaseRelativePath(NavigationManager.Uri).ToString().Contains("trainer");
        SetTitle();
        StateHasChanged();
    }
}