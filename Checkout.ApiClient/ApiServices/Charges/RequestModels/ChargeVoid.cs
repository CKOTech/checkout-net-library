using Checkout.ApiServices.SharedModels;
using System.Collections.Generic;
namespace Checkout.ApiServices.Charges.RequestModels
{
    public class ChargeVoid : BaseChargeInfo
    {
        public List<Product> Products { get; set; }
    }
}