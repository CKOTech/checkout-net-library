using Checkout.ApiServices.SharedModels;
using System;
using System.Collections.Generic;

namespace Checkout.ApiServices.Customers.RequestModels
{
    public class BaseCustomer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Phone  Phone{ get; set; }
        public string Description { get; set; }
        public Dictionary<string, string> Metadata { get; set; }

    }
}
