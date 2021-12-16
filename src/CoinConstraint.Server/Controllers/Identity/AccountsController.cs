using CoinConstraint.Server.Infrastructure.Models;
using CoinConstraint.Server.Infrastructure.Responses;
using Microsoft.AspNetCore.Identity;

namespace CoinConstraint.Server.Controllers.Identity;

[Route("api/[controller]")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public AccountsController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] RegisterRequest model)
    {
        var newUser = new IdentityUser { UserName = model.Email, Email = model.Email };

        var result = await _userManager.CreateAsync(newUser, model.Password);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description);

            return Ok(new RegisterResponse { Successful = false, Errors = errors });

        }

        return Ok(new RegisterResponse { Successful = true });
    }
}
