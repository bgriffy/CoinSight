using CoinConstraint.Domain.Enums;
using System.Text.Json.Serialization;

namespace CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities
{
    public class BudgetSchedule
    {
        public int ID { get; set; }

        public int BudgetID { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public ScheduleFrequency ScheduleFrequency { get; set; }

        public DateTime NextScheduledDate { get; set; }

        public DateTime LastScheduledDate { get; set; }

        [NotMapped]
        public bool IsNew { get; set; }

        [NotMapped]
        public bool IsUpdated { get; set; } 
    }
}
