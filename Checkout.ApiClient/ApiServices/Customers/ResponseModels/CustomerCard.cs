using Checkout.ApiServices.Cards.ResponseModels;
using System;
using System.Collections.Generic;

namespace Checkout.ApiServices.Customers.ResponseModels
{
    public class CustomerCard
    {
        public int Count { get; set; }
        public List<Card> Data { get; set; }
    }
}
