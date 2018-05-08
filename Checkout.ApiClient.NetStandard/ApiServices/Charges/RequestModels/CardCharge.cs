using System.Collections.Generic;
using Checkout.ApiServices.Cards.RequestModels;
using Checkout.ApiServices.RecurringPayments.RequestModels;

namespace Checkout.ApiServices.Charges.RequestModels
{
    public class CardCharge : BaseCharge
    {
        public string TransactionIndicator { get; set; }
        public CardCreate Card { get; set; }
        public List<CustomerPaymentPlanCreate> PaymentPlans { get; set; }
    }
}

