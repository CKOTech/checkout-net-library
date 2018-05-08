using Checkout.ApiServices.SharedModels;

namespace Checkout.ApiServices.Payouts.RequestModels
{
    public class BasePayout
    {
        public int Value { get; set; }
        public string Currency { get; set; }
        public string  Destination { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
