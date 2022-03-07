using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;
using CoinConstraint.Domain.Enums;
using CoinConstraint.Server.Infrastructure.DataAccess;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoinConstraint.BudgetScheduler;

public class BudgetScheduler
{
    private readonly CoinConstraintContext _context;

    public BudgetScheduler(CoinConstraintContext context)
    {
        _context = context;
    }

    [FunctionName("ScheduleBudgets")]

    public async Task Run([TimerTrigger("0 */5 * * * *", RunOnStartup = true)] TimerInfo myTimer, ILogger log)
    {
        await Task.Delay(1000);

        try
        {
            var budgetSchedules = _context.BudgetSchedules.Where(s => s.NextScheduledDate <= DateTime.Now).ToList();

            log.LogInformation($"C# Timer trigger function  executed at: {DateTime.Now}");
            budgetSchedules.ForEach(async s => await ScheduleBudget(s));
        }
        catch (Exception e)
        {
            Console.WriteLine($"There was an error scheduling budgets. Error message: {e.Message}");
            throw;
        }
    }

    private async Task ScheduleBudget(BudgetSchedule schedule)
    {
        if ((schedule.EndDate ?? DateTime.Now) <= DateTime.Now)
        {
            return;
        }

        SetScheduleDates(schedule);

        var newBudget = GetNewBudgetFromSchedule(schedule);

        _context.Budgets.Add(newBudget);

        await SaveChanges();
    }

    private Budget GetNewBudgetFromSchedule(BudgetSchedule schedule)
    {
        var budget = _context.Budgets.FirstOrDefault(b => b.ID == schedule.BudgetID);
        var newBudget = budget.Clone();

        newBudget.StartDate = DateTime.Now;

        var budgetLengthInDays = ((DateTime)budget.EndDate - (DateTime)budget.StartDate).Days;
        newBudget.EndDate = ((DateTime)newBudget.StartDate).AddDays(budgetLengthInDays);

        return newBudget;
    }

    private void SetScheduleDates(BudgetSchedule schedule)
    {
        schedule.LastScheduledDate = schedule.NextScheduledDate;

        switch (schedule.ScheduleFrequency)
        {
            case ScheduleFrequency.Daily:
                schedule.NextScheduledDate = DateTime.Now.AddDays(1);
                break;
            case ScheduleFrequency.Weekly:
                schedule.NextScheduledDate = DateTime.Now.AddDays(7);
                break;
            case ScheduleFrequency.Monthly:
                schedule.NextScheduledDate = DateTime.Now.AddMonths(1);
                break;
            case ScheduleFrequency.Yearly:
                schedule.NextScheduledDate = DateTime.Now.AddYears(1);
                break;
            default:
                return;
        }
    }

    private async Task SaveChanges()
    {
        await _context.SaveChangesAsync();
    }
}
