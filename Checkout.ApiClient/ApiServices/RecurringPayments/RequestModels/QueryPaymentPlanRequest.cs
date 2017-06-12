using Checkout.ApiServices.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout.ApiServices.RecurringPayments.RequestModels
{
    public class QueryPaymentPlanRequest : PaymentPlanUpdate, IQueryRequest
    {
        public int? Count { get; set; }
        public int? Offset { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
