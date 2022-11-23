using Microsoft.AspNetCore.Components;
using Squads.Shared.Reservations;
using Squads.Shared.Sessions;
using Squads.Client.Shared;
using MudBlazor;

namespace Squads.Client.Trainee.BookSession.Components;

public partial class ConfirmationSignUp
{

    [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;
    [Parameter] public SessionDto.Index CurrentSession { get; set; } = default!;
    [Parameter] public Color Color { get; set; }
    [Parameter] public EventCallback StateTest { get; set; }
    [Parameter] public EventCallback<int> ReserveSession { get; set; }
    [Inject] FakeAuthenticationProvider FakeAuthenticationProvider { get; set; } = default!;



    //When clicked on confirmation in dialog, this will function will be fired.
    //It checkes if you are loged in as vistior/not registered in the system, if not it will go you a snackbar
    //If you passed the check you there will be a function in the service layer. (making a reservation)
    async void Submit()
    {
        if (FakeAuthenticationProvider.CheckVisitorOrNot())
        {
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
            Snackbar.Add("You need to sign in.", Severity.Warning, config =>
            {


                config.Action = "Login";
                config.ActionColor = Color.Primary;
                config.Onclick = snackbar =>
                {
                    //Here comes the redirect to /login
                    GoToLoginPage();
                    return Task.CompletedTask;
                };
            });
        }
        else
        {

            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
            Snackbar.Add("Booking succesfull", Severity.Success);
            MudDialog.Close(DialogResult.Ok(true));

            await ReserveSession.InvokeAsync(CurrentSession.Id);
        }
    }

    void Cancel() => MudDialog.Cancel();

    void GoToLoginPage()
    {
        Console.WriteLine("going to login page…");
    }
}
