using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.ApiServices.RecurringPayments.RequestModels
{
    public class SinglePaymentPlanCreateRequest
    {
        public List<PaymentPlanCreate> PaymentPlans { get; set; }
    }
}
