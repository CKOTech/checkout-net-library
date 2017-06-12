using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.ApiServices.RecurringPayments.RequestModels
{
    public class BaseRecurringPlan
    {
        public string Name { get; set; }
        public string PlanTrackId { get; set; }
        public decimal? AutoCapTime { get; set; }
        public string Value { get; set; }
        public string Cycle { get; set; }
        public int? RecurringCount { get; set; }
    }
}
