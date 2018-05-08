using System;
using System.Collections.Generic;
using Checkout.ApiServices.RecurringPayments.RequestModels;

namespace Checkout.ApiServices.Charges.RequestModels
{
    public class CardIdCharge : BaseCharge
    {
        public string Cvv { get; set; }
        public string TransactionIndicator { get; set; }
        public string CardId { get; set; }
        public List<CustomerPaymentPlanCreate> PaymentPlans { get; set; }
    }
}