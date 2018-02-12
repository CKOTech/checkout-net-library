using Checkout.ApiServices.Cards.ResponseModels;
using Checkout.ApiServices.Charges.RequestModels;
using Checkout.ApiServices.SharedModels;
using System;
using System.Collections.Generic;
using Checkout.ApiServices.RecurringPayments.RequestModels;
using Newtonsoft.Json;

namespace Checkout.ApiServices.Charges.ResponseModels
{
    public class Charge:BaseCharge
    {
        public string Id { get; set; }
        public string OriginalId { get; set; }
        public bool LiveMode { get; set; }
        public string Created { get; set; }
        public int ChargeMode { get; set; }
        public string CustomerId { get; set; }
        public string TransactionIndicator { get; set; }
        public bool Used { get; set; }
        public Card Card { get; set; }
        public string ResponseMessage { get; set; }
        public string ResponseAdvancedInfo { get; set; }
        public string ResponseCode { get; set; }
        public string Status { get; set; }
        public bool IsCascaded { get; set; }
        public string AuthCode { get; set; }
        public Address BillingDetails { get; set; }
        public Address ShippingDetails { get; set; }
        public List<CustomerPaymentPlan> CustomerPaymentPlans { get; set; }
        public LocalPayment LocalPayment { get; set; }
        public string SuccessUrl { get; set; }
        public string FailUrl { get; set; }
        public string Udf1 { get; set; }
        public string Udf2 { get; set; }
        public string Udf3 { get; set; }
        public string Udf4 { get; set; }
        public string Udf5 { get; set; }
        public string Enrolled { get; set; }
        public string Xid { get; set; }
        public string SignatureValid { get; set; }
        public string AuthenticationResponse { get; set; }
        public string Eci { get; set; }
        public string Cavv { get; set; }
    }
}