namespace Checkout.ApiServices.Charges.RequestModels
{
    public class LocalPaymentCharge : BaseCharge
    {
        public LocalPaymentCharge()
        {
            ChargeMode = 3;
            AutoCapture = null;
        }

        public string PaymentToken { get; set; }
        public LocalPaymentCreate LocalPayment { get; set; }
    }
}
