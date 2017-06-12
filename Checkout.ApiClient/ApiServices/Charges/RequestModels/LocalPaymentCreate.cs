using System.Collections.Generic;

namespace Checkout.ApiServices.Charges.RequestModels
{
    public class LocalPaymentCreate
    {
        public string LppId { get; set; }
        public Dictionary<string, string> UserData { get; set; }
    }
}
