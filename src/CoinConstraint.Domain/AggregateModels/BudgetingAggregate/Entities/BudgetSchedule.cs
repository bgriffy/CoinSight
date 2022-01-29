using CoinConstraint.Domain.Enums;

namespace CoinConstraint.Domain.AggregateModels.BudgetingAggregate.Entities
{
    public class BudgetSchedule
    {

        public int ID { get; set; }

        public int BudgetID { get; set; }

        public Budget Budget { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public ScheduleFrequency ScheduleFrequency { get; set; }

        public DateTime NextScheduledDate { get; set; }

        public DateTime LastScheduledDate { get; set; }
    }
}
