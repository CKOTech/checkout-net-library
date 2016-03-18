using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.ApiServices.RecurringPayments.RequestModels
{
    public class CustomerPaymentPlan : CustomerPaymentPlanCreate
    {
        public string CustomerPlanId { get; set; }
        public string Currency { get; set; }
        public string CardId { get; set; }
        public string CustomerId { get; set; }
        public RecurringPlanStatus? Status { get; set; }
        public int? RecurringCountLeft { get; set; }
        public int? TotalCollectedValue { get; set; }
        public int? TotalCollectedCount { get; set; }
        public string PreviousRecurringDate { get; set; }
        public string NextRecurringDate { get; set; }
    }
}
