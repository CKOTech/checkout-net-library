namespace Checkout.ApiServices.Tokens.RequestModels
{
    public class TokenCard
    {
        public string ExpiryMonth { get; set; }
        public string ExpiryYear { get; set; }
        public string Number { get; set; }
        public string Cvv { get; set; }
    }
}