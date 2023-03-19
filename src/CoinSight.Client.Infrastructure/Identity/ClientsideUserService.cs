using CoinConstraint.Application.Identity;

namespace CoinConstraint.Client.Infrastructure.Identity
{
    public class ClientsideUserService : ICurrentUserService
    {
        private readonly AuthenticationStateProvider _authStateProvider;
        private AuthenticationState _authState = null;

        public ClientsideUserService(AuthenticationStateProvider authStateProvider)
        {
            _authStateProvider = authStateProvider;
        }

        public async Task<Guid> GetCurrentUserID()
        {
            if (_authState == null)
            {
                await InitializeAuthState();
            }

            return Guid.Parse(_authState.User.GetUserId());
        }

        public async Task<List<KeyValuePair<string, string>>> GetClaims()
        {
            if (_authState == null)
            {
                await InitializeAuthState();
            }

            return _authState.User.Claims.AsEnumerable().Select(item => new KeyValuePair<string, string>(item.Type, item.Value)).ToList();
        }

        private async Task InitializeAuthState()
        {
            _authState = await _authStateProvider.GetAuthenticationStateAsync();
        }
    }
}
