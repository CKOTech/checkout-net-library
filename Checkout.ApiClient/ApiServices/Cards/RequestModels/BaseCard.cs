using Checkout.ApiServices.SharedModels;
namespace Checkout.ApiServices.Cards.RequestModels
{
    public class BaseCard
    {
        public string Name { get; set; }
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public Address BillingDetails { get; set; }
    }
}
