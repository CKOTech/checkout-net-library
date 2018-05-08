using Checkout.ApiServices.SharedModels;
using System.Collections.Generic;
namespace Checkout.ApiServices.Charges.RequestModels
{
    public class ChargeRefund : BaseChargeInfo
    {
        /// <summary>
        /// Charge amount to be refunded.
        /// </summary>
        public string Value { get; set; }
        public List<Product> Products { get; set; }
    }
}