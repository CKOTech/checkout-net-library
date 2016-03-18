using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.ApiServices.RecurringPayments.RequestModels
{
    public class PaymentPlanCreate : BaseRecurringPlan
    {
        public string Currency { get; set; }
    }
}
