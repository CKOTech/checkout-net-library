using Checkout.ApiServices.SharedModels;
using System.Collections.Generic;
namespace Checkout.ApiServices.Charges.RequestModels
{
    public class ChargeCapture: BaseChargeInfo
    {
        /// <summary>
        /// Charge amount to be captured.
        /// </summary>
        public string Value { get; set; }
        public List<Product> Products { get; set; }
    }
}
