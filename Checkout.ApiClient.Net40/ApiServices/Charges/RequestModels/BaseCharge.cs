using Checkout.ApiServices.RecurringPayments.RequestModels;
using Checkout.ApiServices.SharedModels;
using System.Collections.Generic;
namespace Checkout.ApiServices.Charges.RequestModels
{
    public class BaseCharge:BaseChargeInfo
    {
        private const string Yes = "y";

        public BaseCharge()
        {
            AutoCapture = Yes;
            ChargeMode = 1;			//Default mode is no 3D
        }

        public string Value { get; set; }
        public string Currency { get; set; }
        public int ChargeMode { get; set; }
        public bool AttemptN3D { get; set; }
        public string AutoCapture { get; set; }
        public decimal AutoCapTime { get; set; }
        public string CustomerIp { get; set; }
        public string Email { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string RedirectUrl { get; set; }
        public List<Product> Products { get; set; }
        public Address ShippingDetails { get; set; }
        public BillingDescriptor Descriptor { get; set; }
        public bool? RiskCheck { get; set; }
    }
}