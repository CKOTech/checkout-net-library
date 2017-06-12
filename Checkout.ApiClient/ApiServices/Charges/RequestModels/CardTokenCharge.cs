using System;
using System.Collections.Generic;
using Checkout.ApiServices.RecurringPayments.RequestModels;

namespace Checkout.ApiServices.Charges.RequestModels
{
    public class CardTokenCharge : BaseCharge
    {
        public string TransactionIndicator { get; set; }
        public string CardToken { get; set; }
        public List<CustomerPaymentPlanCreate> PaymentPlans { get; set; }
    }
}
