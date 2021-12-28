using System;

namespace CoinConstraint.Application.Identity;

public interface ICurrentUserService
{
    Task<List<KeyValuePair<string, string>>> GetClaims();
    public Task<Guid> GetCurrentUserID();
}
