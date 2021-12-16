namespace CoinConstraint.Server.Controllers.Users;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUsersAsync()
    {
        try
        {
            var users = _userRepository.GetAll();
            return Ok(users);
        }
        catch (System.Exception e)
        {
            Console.WriteLine($"There was an error retrieving totals: {e.Message}");
            throw;
        }
    }
}
