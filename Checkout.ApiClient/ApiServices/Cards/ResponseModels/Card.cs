using Checkout.ApiServices.Cards.RequestModels;
namespace Checkout.ApiServices.Cards.ResponseModels
{
    public class Card:BaseCard
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string Last4 { get; set; }
        public string Bin { get; set; }
        public string PaymentMethod { get; set; }
        public string FingerPrint { get; set; }
        public string CvvCheck { get; set; }
        public string AvsCheck { get; set; }
        public string ResponseCode { get; set; }
    }
}