using System;
using System.Data.SqlClient;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;
using System.Collections.Generic;
using CoinConstraint.Domain.Enums;
using CoinConstraint.Server.Infrastructure.DataAccess;
using System.Linq;

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
        await Task.Delay(5000);

        var theSchedukes = _context.BudgetSchedules.ToList();

        log.LogInformation($"C# Timer trigger function  executed at: {DateTime.Now}");

        // Get the connection string from app settings and use it to create a connection.
        var connectionString = Environment.GetEnvironmentVariable("coinconstraint_connection");
        var conn = new SqlConnection(connectionString);
        conn.Open();

        var budgetSchedules = GetSchedules(conn);

        foreach (var budgetSchedule in budgetSchedules)
        {
            var budget = GetBudgetFromSchedule(budgetSchedule, conn);
        }
    }

    private static List<BudgetSchedule> GetSchedules(SqlConnection conn)
    {
        var sql = "SELECT * FROM CoinConstraint.dbo.BudgetSchedules";
        var cmd = new SqlCommand(sql, conn);
        SqlDataReader rows = cmd.ExecuteReader();

        var budgetSchedules = new List<BudgetSchedule>();
         
        while (rows.Read())
        {
            var budgetSchedule = new BudgetSchedule()
            {
                ID = (int)rows["id"],
                BudgetID = (int)rows["BudgetID"],
                EndDate = (DateTime)rows["EndDate"],
                StartDate = (DateTime)rows["StartDate"],
                LastScheduledDate = (DateTime)rows["LastScheduledDate"],
                NextScheduledDate = (DateTime)rows["NextScheduledDate"],
                ScheduleFrequency = (ScheduleFrequency)(int)rows["ScheduleFrequency"]
            };

            budgetSchedules.Add(budgetSchedule);
        }

        return budgetSchedules;
    }

    private static Budget GetBudgetFromSchedule(BudgetSchedule schedule, SqlConnection conn)
    {
        var budget = new Budget();
        var sql = "SELECT TOP 1 * FROM CoinConstraint.dbo.Budgets WHERE BudgetID = @BudgetID";
        var cmd = new SqlCommand(sql, conn);
        cmd.Parameters["@BudgetID"].Value = schedule.BudgetID;
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            budget.ID= (int?)reader["id"];
        }

        return budget;

    }
}
