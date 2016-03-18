using Checkout.ApiServices.SharedModels;
using System.Collections.Generic;
using Checkout.ApiServices.RecurringPayments.RequestModels;

namespace Checkout.ApiServices.Charges.RequestModels
{
    public class DefaultCardCharge : BaseCharge
    {
        public string TransactionIndicator { get; set; }
        public List<CustomerPaymentPlanCreate> PaymentPlans { get; set; }
    }
}