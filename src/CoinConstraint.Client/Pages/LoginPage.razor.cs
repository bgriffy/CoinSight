using CoinConstraint.Application.Requests;

namespace CoinConstraint.Client.Pages
{
    public partial class LoginPage
    {
        private LoginRequest loginRequest = new LoginRequest();
        private bool ShowErrors;
        private string Error = "";

        private async Task HandleLogin()
        {
            ShowErrors = false;

            var result = await AuthService.Login(loginRequest);

            if (result.Successful)
            {
                NavigationManager.NavigateTo("/");
            }
            else
            {
                Error = result.Error;
                ShowErrors = true;
            }
        }
    }
}
