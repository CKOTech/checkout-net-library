using Checkout.ApiServices.Cards.RequestModels;

namespace Checkout.ApiServices.Customers.RequestModels
{
    public class CustomerCreate : BaseCustomer
    {
        public CardCreate Card { get; set; }
    }
}
