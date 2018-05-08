using Checkout.ApiServices.Cards.RequestModels;
using Checkout.ApiServices.Charges.RequestModels;
using Checkout.ApiServices.SharedModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.ApiServices.Tokens.ResponseModels
{
    public class PaymentToken
    {
        public string Id { get; set; }
        public bool LiveMode { get; set; }
    }
}
