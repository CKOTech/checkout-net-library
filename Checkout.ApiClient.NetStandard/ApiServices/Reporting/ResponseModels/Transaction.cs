using Checkout.ApiServices.Customers.ResponseModels;
using System;

namespace Checkout.ApiServices.Reporting.ResponseModels
{
    public class Transaction
    {
        public string Id { get; set; }
        public string OriginId { get; set; }
        public DateTime? Date { get; set; }
        public string Status { get; set; }
        public string Type { get; set; }
        public string Amount { get; set; }
        public string Scheme { get; set; }
        public string ResponseCode { get; set; }
        public string Currency { get; set; }
        public bool LiveMode { get; set; }
        public string BusinessName { get; set; }
        public string ChannelName { get; set; }
        public string TrackId { get; set; }
        public Customer Customer { get; set; }
    }
}
