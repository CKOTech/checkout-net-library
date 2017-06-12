using Checkout.ApiServices.RecurringPayments.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.ApiServices.RecurringPayments.ResponseModels
{
    public class SinglePaymentPlanCreateResponse
    {
        public List<PaymentPlan> PaymentPlans { get; set; }

        public long TotalCollectionCount { get; set; }

        public long TotalCollectionValue { get; set; }
    }
}
