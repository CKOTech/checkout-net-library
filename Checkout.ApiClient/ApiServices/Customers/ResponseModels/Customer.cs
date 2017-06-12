using Checkout.ApiServices.Cards.ResponseModels;
using Checkout.ApiServices.Customers.RequestModels;
using Checkout.ApiServices.SharedModels;
using System;
using System.Collections.Generic;

namespace Checkout.ApiServices.Customers.ResponseModels
{
    public class Customer:BaseCustomer
    {
        public string Id { get; set; }
        public bool LiveMode { get; set; }
        public string Created { get; set; }
        public string DefaultCard { get; set; }
        public CustomerCard Cards { get; set; }
       
    }
}
