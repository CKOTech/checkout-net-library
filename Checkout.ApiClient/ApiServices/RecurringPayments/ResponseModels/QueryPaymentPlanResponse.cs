using Checkout.ApiServices.RecurringPayments.RequestModels;
using Checkout.ApiServices.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.ApiServices.RecurringPayments.ResponseModels
{
    public class QueryPaymentPlanResponse : IQueryResponse
    {
        public int Count { get; set; }

        public int Offset { get; set; }

        public int TotalRows { get; set; }

        public List<PaymentPlan> Data { get; set; }
    }
}
