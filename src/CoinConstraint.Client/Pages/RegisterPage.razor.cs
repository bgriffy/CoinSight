using CoinConstraint.Application.Requests;

namespace CoinConstraint.Client.Pages;

public partial class RegisterPage
{
    private RegisterRequest RegisterRequest = new RegisterRequest();
    private bool ShowErrors;
    private IEnumerable<string> Errors;

    private async Task HandleRegistration()
    {
        ShowErrors = false;

        var result = await AuthService.Register(RegisterRequest);

        if (result.Successful)
        {
            NavigationManager.NavigateTo("/login");
        }
        else
        {
            Errors = result.Errors;
            ShowErrors = true;
        }
    }
}
