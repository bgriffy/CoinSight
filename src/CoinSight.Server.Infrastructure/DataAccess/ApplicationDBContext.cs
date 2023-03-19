using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CoinConstraint.Server.Infrastructure.DataAccess;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
}
