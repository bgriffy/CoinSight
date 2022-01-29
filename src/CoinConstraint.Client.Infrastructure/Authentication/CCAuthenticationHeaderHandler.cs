using System.Threading;

namespace CoinConstraint.Client.Infrastructure.Authentication
{
    public class CCAuthenticationHeaderHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorage;

        public CCAuthenticationHeaderHandler(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.Authorization?.Scheme != "Bearer")
            {
                var authToken = await _localStorage.GetItemAsync<string>("authToken");

                if (!string.IsNullOrWhiteSpace(authToken))
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
                }
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
