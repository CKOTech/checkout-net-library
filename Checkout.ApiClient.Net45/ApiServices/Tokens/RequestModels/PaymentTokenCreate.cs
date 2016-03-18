using Checkout.ApiServices.Cards.RequestModels;
using Checkout.ApiServices.Charges.RequestModels;
using Checkout.ApiServices.SharedModels;
using System;
using System.Collections.Generic;
using System.Text;
using Checkout.ApiServices.RecurringPayments.RequestModels;

namespace Checkout.ApiServices.Tokens.RequestModels
{
    public class PaymentTokenCreate:BaseCharge
    {
        public Address ShippingDetails { get; set; }
        public List<CustomerPaymentPlanCreate> PaymentPlans { get; set; }
    }
}
