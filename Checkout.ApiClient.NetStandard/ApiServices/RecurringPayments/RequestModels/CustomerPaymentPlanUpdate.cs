using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.ApiServices.RecurringPayments.RequestModels
{
    public class CustomerPaymentPlanUpdate
    {
        public string CardId { get; set; }
        public RecurringPlanStatus? Status { get; set; }
    }
}
