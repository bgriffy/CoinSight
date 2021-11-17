namespace CoinConstraint.Server.Infrastructure.Users;

public class UserRepository : ServersideRepository<User>, IUserRepository
{
    private readonly CoinConstraintContext _context;

    public UserRepository(CoinConstraintContext context) : base(context)
    {
        _context = context;
    }
}
