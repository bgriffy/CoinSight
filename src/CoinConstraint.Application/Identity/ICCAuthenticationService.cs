using CoinConstraint.Application.Requests;
using CoinConstraint.Application.Responses;

namespace CoinConstraint.Application.Identity;

public interface ICCAuthenticationService
{
    Task<LoginResponse> Login(LoginRequest loginModel);
    Task Logout();
    Task<RegisterResponse> Register(RegisterRequest registerModel);
}
