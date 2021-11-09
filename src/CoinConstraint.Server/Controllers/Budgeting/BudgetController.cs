using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinConstraint.Server.Controllers.Budgeting;

[Route("api/[controller]")]
[ApiController]
public class BudgetController : ControllerBase
{
    private readonly IBudgetRepository _budgetRepository;

    public BudgetController(IBudgetRepository budgetRepository)
    {
        _budgetRepository = budgetRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Budget>>> GetBudgetsAsync()
    {
        try
        {
            var budgets = await _budgetRepository.GetAllAsync();
            return Ok(budgets);
        }
        catch (System.Exception e)
        {
            Console.WriteLine($"There was an error retrieving budgets: {e.Message}");
            throw;
        }
    }
}
