namespace CoinConstraint.Server.Infrastructure.Identity;

public class CurrentUserService : ICurrentUserService
{
    private Guid _userID;
    private List<KeyValuePair<string, string>> _claims;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
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

    public Guid GetCurrentUserID()
    {
        return _userID;
    }

    public List<KeyValuePair<string, string>> GetClaims()
    {
        return _claims;
    }
}
