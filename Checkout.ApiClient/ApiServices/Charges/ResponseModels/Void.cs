using Checkout.ApiServices.Charges.RequestModels;
using Checkout.ApiServices.SharedModels;
using System.Collections.Generic;

namespace Checkout.ApiServices.Charges.ResponseModels
{
    public class Void:BaseChargeInfo
    {
        public string Id { get; set; }
        public string OriginalId { get; set; }
        public bool LiveMode { get; set; }
        public string Created { get; set; }
        public decimal Value { get; set; }
        public string Currency { get; set; }
        public string ResponseMessage { get; set; }
        public string ResponseAdvancedInfo { get; set; }
        public string ResponseCode { get; set; }
        public string Status { get; set; }
        public List<Product> Products { get; set; }
    }
}