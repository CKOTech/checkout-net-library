using System;
using Checkout.ApiServices.Customers.ResponseModels;

namespace Checkout.ApiServices.Reporting.ResponseModels
{
    public class Chargeback
    {
        public string Id { get; set; }

        public string ChargeId { get; set; }

        public string Scheme { get; set; }

        public decimal Amount { get; set; }

        public decimal Value { get; set; }

        public string Currency { get; set; }

        public string TrackId { get; set; }

        public DateTime IssueDate { get; set; }

        public string CardNumber { get; set; }

        public string Indicator { get; set; }

        public string ReasonCode { get; set; }

        public string ARN { get; set; }

        public Customer Customer { get; internal set; }
    }
}