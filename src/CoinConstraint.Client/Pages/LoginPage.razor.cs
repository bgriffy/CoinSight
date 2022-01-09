using CoinConstraint.Application.Requests;
using CoinConstraint.Client.Components;

namespace CoinConstraint.Client.Pages
{
    public partial class LoginPage
    {
        private LoginRequest loginRequest = new LoginRequest();
        private bool ShowErrors;
        private string Error = "";
        private LoadSpinnerComponent _loadSpinner;
        private bool _isLoading;

        protected override void OnInitialized()
        {
            _isLoading = false; 
        }

        private async Task HandleLogin()
        {
            await _loadSpinner.ShowLoadSpinner();

            _isLoading = true;

            ShowErrors = false;

            var result = await AuthService.Login(loginRequest);

            if (result.Successful)
            {
                _isLoading = false;
                NavigationManager.NavigateTo("/");
            }
            else
            {
                Error = result.Error;
                ShowErrors = true;
            }

            _isLoading = false;
            await _loadSpinner.HideLoadSpinner();
        }
    }
}
