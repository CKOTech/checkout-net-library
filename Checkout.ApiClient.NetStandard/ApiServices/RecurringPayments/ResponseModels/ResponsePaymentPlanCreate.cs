using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.ApiServices.RecurringPayments.ResponseModels
{
    public class ResponsePaymentPlanCreate : BaseResponseRecurringPlan
    {
        public string Currency { get; set; }
    }
}
