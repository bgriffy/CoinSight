using CoinConstraint.Domain.AggregateModels.BudgetAggregate;
using CoinConstraint.Domain.AggregateModels.BudgetAggregate.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoinConstraint.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{
    private readonly IExpenseRepository _expenseRepository;

    public ExpensesController(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<Expense>>> GetExpensesAsync()
    {
        try
        {
            var expenses = await _expenseRepository.GetAllAsync();
            return Ok(expenses);
        }
        catch (System.Exception e)
        {
            Console.WriteLine($"There was an error retrieving expenses: {e.Message}");
            throw;
        }
    }

    [HttpPost]
    public async Task SaveNewExpenseAsync(Expense expense)
    {
        try
        {
            //TODO: Need to revisit and refactor IRepository/ICLientside repository logic.
            //Should be able to call Update instead of UpdateAsync here. 
            await _expenseRepository.AddAsync(expense);
            await _expenseRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error saving new expense: {e.Message}");
            throw;
        }
    }

    [HttpPut]
    public async Task UpdateExistingExpense(Expense expense)
    {
        try
        {
            //TODO: Need to revisit and refactor IRepository/ICLientside repository logic.
            //Should be able to call Update instead of UpdateAsync here. 
            await _expenseRepository.UpdateAsync(expense);
            await _expenseRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error updating existing expense: {e.Message}");
            throw;
        }
    }

    [HttpDelete]
    public async Task DeleteExpense(Expense expense)
    {
        try
        {
            //TODO: Need to revisit and refactor IRepository/ICLientside repository logic.
            //Should be able to call Remove instead of RemoveAsync here. 
            await _expenseRepository.RemoveAsync(expense);
            await _expenseRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error deleting expense: {e.Message}");
            throw;
        }
    }
}
