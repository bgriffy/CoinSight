using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Repository_Interfaces.Serverside;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoinConstraint.Server.Controllers.Budgeting
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetSchedulesController : ControllerBase
    {
        private readonly IBudgetScheduleRepository _budgetSchedulesRepository;

        public BudgetSchedulesController(IBudgetScheduleRepository budgetSchedulesRepository)
        {
            _budgetSchedulesRepository = budgetSchedulesRepository;
        }

        [HttpDelete("DeleteMultiple")]
        public async Task<ActionResult> DeleteBudgetSchedules(List<BudgetSchedule> schedules)
        {
            try
            {
                //foreach (var expense in schedules)
                //{
                //var actionIsAuthorized = await _authorizationService.ActionIsAuthorized(User, budget, Operations.Delete);
                //if (!actionIsAuthorized)
                //{
                //    return Unauthorized();
                //}
                //}

                _budgetSchedulesRepository.RemoveRange(schedules);
                await _budgetSchedulesRepository.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error deleting schedules: {e.Message}");
                throw;
            }
        }
    }
}
