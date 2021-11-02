using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;
using CoinConstraint.Domain.AggregateModels.UserAggregate;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinConstraint.Server.Controllers.Users
{
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
                var users = await _userRepository.GetAllAsync();
                return Ok(users);
            }
            catch (System.Exception e)
            {
                Console.WriteLine($"There was an error retrieving totals: {e.Message}");
                throw;
            }
        }
    }
}
