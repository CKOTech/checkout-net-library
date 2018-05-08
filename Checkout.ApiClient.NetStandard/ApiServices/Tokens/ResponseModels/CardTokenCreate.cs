namespace Checkout.ApiServices.Tokens.ResponseModels
{
    public class CardTokenCreate
    {
        public string Id { get; set; }
        public string LiveMode { get; set; }
        public string Created { get; set; }
        public string Used { get; set; }
        public Cards.ResponseModels.Card Card { get; set; }
    }
}
