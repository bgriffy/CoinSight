using CoinConstraint.Application.Requests;
using CoinSight.Client.Shared;
using System.Threading.Tasks;

namespace CoinSight.Client.Pages
{
    public partial class LoginPage
    {
        private LoginRequest loginRequest = new LoginRequest();
        private bool ShowErrors;
        private string Error = "";
        private LoadSpinner _loadSpinner;
        private bool _isLoading;

        protected override async Task OnInitializedAsync()
        {
            await _loadSpinner.HideLoadSpinner();
        }

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
                NavigationManager.NavigateTo("/", forceLoad:true);
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
