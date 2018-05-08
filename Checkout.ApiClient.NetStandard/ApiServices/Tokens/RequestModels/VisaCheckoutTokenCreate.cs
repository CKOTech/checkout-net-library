namespace Checkout.ApiServices.Tokens.RequestModels
{
    public class VisaCheckoutTokenCreate
    {
        public string CallId { get; set; }

        public bool IncludeBinData { get; set; }
    }
}
