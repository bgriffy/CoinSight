namespace CoinConstraint.Server.Infrastructure.Identity;

public class ServersideUserService : ICurrentUserService
{
    private Guid _userID;
    private List<KeyValuePair<string, string>> _claims;

    public ServersideUserService(IHttpContextAccessor httpContextAccessor)
    {
        var userID = httpContextAccessor.HttpContext?.User?.GetUserId();

        if (string.IsNullOrEmpty(userID))
        {
            _userID = Guid.Empty;
        }
        else
        {
            _userID = Guid.Parse(userID);
        }

        _claims = httpContextAccessor.HttpContext?.User?.Claims.AsEnumerable().Select(item => new KeyValuePair<string, string>(item.Type, item.Value)).ToList();
    }

    public Task<Guid> GetCurrentUserID()
    {
        return Task.FromResult(_userID);
    }

    public Task<List<KeyValuePair<string, string>>> GetClaims()
    {
        return Task.FromResult(_claims);
    }
}
