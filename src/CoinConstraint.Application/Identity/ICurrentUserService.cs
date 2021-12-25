using System;

namespace CoinConstraint.Application.Identity;

public interface ICurrentUserService
{
    List<KeyValuePair<string, string>> GetClaims();
    public Guid GetCurrentUserID();
}
