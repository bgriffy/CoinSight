using System;
using System.Data.SqlClient;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities;
using System.Collections.Generic;

namespace CoinConstraint.BudgetScheduler;

public class BudgetScheduler
{
    [FunctionName("ScheduleBudgets")]
    
    //public static async Task Run([TimerTrigger("0 */5 * * * *")] 
    //TimerInfo myTimer, ILogger log,
    //[Sql("dbo.ToDo", ConnectionStringSetting = Environment.GetEnvironmentVariable("coinconstraint_connection"))] out BudgetSchedule[] newSchedules)

    public static void Run(
    [TimerTrigger("0 */5 * * * *")]
    TimerInfo myTimer, ILogger log,
    [Sql("SELECT * FROM CoinConstraint.dbo.BudgetSchedules",
    ConnectionStringSetting = "SqlConnectionString")]
    out List<BudgetSchedule> output)
    {
        log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

        // Get the connection string from app settings and use it to create a connection.
        var connectionString = Environment.GetEnvironmentVariable("coinconstraint_connection");
        var conn = new SqlConnection(connectionString);
        conn.Open();

        var sql = "SELECT * FROM CoinConstraint.dbo.BudgetSchedules";
        var cmd = new SqlCommand(sql, conn);
        var rows = cmd.ExecuteReader();

        foreach (var row in rows)
        {

        }

        output = new List<BudgetSchedule>();    

        Console.WriteLine(rows);

        log.LogInformation($"{rows} rows were updated");
    }
}
