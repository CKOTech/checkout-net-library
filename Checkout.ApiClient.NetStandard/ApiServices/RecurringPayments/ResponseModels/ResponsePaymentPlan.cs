using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.ApiServices.RecurringPayments.ResponseModels
{
    public class ResponsePaymentPlan : ResponsePaymentPlanUpdate
    {
        public string PlanId { get; set; }
    }
}

