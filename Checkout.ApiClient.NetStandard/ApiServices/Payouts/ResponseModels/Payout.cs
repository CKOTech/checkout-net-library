namespace Checkout.ApiServices.Payouts.ResponseModels
{
    public class Payout
    {
        public string Id { get; set; }
	    public string Destination { get; set; }
        public string CustomerId { get; set; }
	    public string Currency { get; set; }
	    public int Value { get; set; }
	    public string ResponseCode { get; set; }
        public string ResponseSummary { get; set; }
        public string ResponseDetails { get; set; }
        public string AuthCode { get; set; }
        public string Status { get; set; }
    }
}
