using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkout.ApiServices.RecurringPayments.RequestModels;

namespace Checkout.ApiServices.RecurringPayments.ResponseModels
{
    public class ResponsePaymentPlanUpdate : ResponsePaymentPlanCreate
    {
        public RecurringPlanStatus? Status { get; set; }
    }
}

