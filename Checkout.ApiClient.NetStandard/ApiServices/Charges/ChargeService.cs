using Checkout.ApiServices.Charges.RequestModels;
using Checkout.ApiServices.Charges.ResponseModels;
using Checkout.ApiServices.SharedModels;

namespace Checkout.ApiServices.Charges
{
    public class ChargeService : IChargeService
    {
        private IChargeServiceAsync _chargeServiceAsync;

        public ChargeService(IApiHttpClient apiHttpclient, CheckoutConfiguration configuration)
        {
            _chargeServiceAsync = new ChargeServiceAsync(apiHttpclient, configuration);
        }

        public HttpResponse<Charge> ChargeWithCard(CardCharge requestModel)
        {
            return _chargeServiceAsync.ChargeWithCardAsync(requestModel).Result;
        }

        public HttpResponse<Charge> ChargeWithCardId(CardIdCharge requestModel)
        {
            return _chargeServiceAsync.ChargeWithCardIdAsync(requestModel).Result;
        }

        public HttpResponse<Charge> ChargeWithCardToken(CardTokenCharge requestModel)
        {
            return _chargeServiceAsync.ChargeWithCardTokenAsync(requestModel).Result;
        }

        public HttpResponse<Charge> ChargeWithDefaultCustomerCard(DefaultCardCharge requestModel)
        {
            return _chargeServiceAsync.ChargeWithDefaultCustomerCardAsync(requestModel).Result;
        }

        public HttpResponse<Charge> ChargeWithLocalPayment(LocalPaymentCharge requestModel)
        {
            return _chargeServiceAsync.ChargeWithLocalPaymentAsync(requestModel).Result;
        }

         public HttpResponse<Void> VoidCharge(string chargeId, ChargeVoid requestModel)
        {
            return _chargeServiceAsync.VoidChargeAsync(chargeId, requestModel).Result;
        }

        public HttpResponse<Refund> RefundCharge(string chargeId, ChargeRefund requestModel)
        {
            return _chargeServiceAsync.RefundChargeAsync(chargeId, requestModel).Result;
        }

        public HttpResponse<Capture> CaptureCharge(string chargeId, ChargeCapture requestModel)
        {
            return _chargeServiceAsync.CaptureChargeAsync(chargeId, requestModel).Result;
        }

        public HttpResponse<OkResponse> UpdateCharge(string chargeId, ChargeUpdate requestModel)
        {
            return _chargeServiceAsync.UpdateChargeAsync(chargeId, requestModel).Result;
        }

        public HttpResponse<Charge> GetCharge(string chargeId)
        {
            return _chargeServiceAsync.GetChargeAsync(chargeId).Result;
        }

        public HttpResponse<ChargeHistory> GetChargeHistory(string chargeId)
        {
            return _chargeServiceAsync.GetChargeHistoryAsync(chargeId).Result;
        }

        public HttpResponse<Charge> VerifyCharge(string paymentToken)
        {
            return _chargeServiceAsync.VerifyChargeAsync(paymentToken).Result;
        }
    }
}