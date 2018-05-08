using Checkout.ApiServices.RecurringPayments.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Checkout.ApiServices.SharedModels;

namespace Checkout.ApiServices.RecurringPayments.ResponseModels
{
    public class QueryCustomerPaymentPlanResponse : IQueryResponse
    {
        public int TotalRows { get; set; }
        public int Offset { get; set; }
        public int Count { get; set; }

        public List<CustomerPaymentPlan> Data { get; set; }
    }
}
