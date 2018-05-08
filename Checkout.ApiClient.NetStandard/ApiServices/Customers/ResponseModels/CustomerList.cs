using System;
using System.Collections.Generic;

namespace Checkout.ApiServices.Customers.ResponseModels
{
    public class CustomerList
    {
        public int Count{ get; set; }
        public List<Customer> Data { get; set; }
    }
}