using Checkout.ApiServices.Charges.RequestModels;
using Checkout.ApiServices.Charges.ResponseModels;
using Checkout.ApiServices.SharedModels;
using System.Threading.Tasks;

namespace Checkout.ApiServices.Charges
{
    public class ChargeServiceAsync : IChargeServiceAsync
    {
        private IApiHttpClient _apiHttpClient;
        private CheckoutConfiguration _configuration;

        public ChargeServiceAsync(IApiHttpClient apiHttpclient, CheckoutConfiguration configuration)
        {
            _apiHttpClient = apiHttpclient;
            _configuration = configuration;
        }

        public Task<HttpResponse<Charge>> ChargeWithCardAsync(CardCharge requestModel)
        {
            return _apiHttpClient.PostRequest<Charge>(_configuration.ApiUrls.CardCharge, _configuration.SecretKey, requestModel);
        }

        public Task<HttpResponse<Charge>> ChargeWithCardIdAsync(CardIdCharge requestModel)
        {
            return _apiHttpClient.PostRequest<Charge>(_configuration.ApiUrls.CardCharge, _configuration.SecretKey, requestModel);
        }

        public Task<HttpResponse<Charge>> ChargeWithCardTokenAsync(CardTokenCharge requestModel)
        {
            return _apiHttpClient.PostRequest<Charge>(_configuration.ApiUrls.CardTokenCharge, _configuration.SecretKey, requestModel);
        }

        public Task<HttpResponse<Charge>> ChargeWithDefaultCustomerCardAsync(DefaultCardCharge requestModel)
        {
            return _apiHttpClient.PostRequest<Charge>(_configuration.ApiUrls.DefaultCardCharge, _configuration.SecretKey, requestModel);
        }

        public Task<HttpResponse<Charge>> ChargeWithLocalPaymentAsync(LocalPaymentCharge requestModel)
        {
            return _apiHttpClient.PostRequest<Charge>(_configuration.ApiUrls.LocalPaymentCharge, _configuration.SecretKey, requestModel);
        }

        public Task<HttpResponse<Void>> VoidChargeAsync(string chargeId, ChargeVoid requestModel)
        {
            var voidChargeApiUri = string.Format(_configuration.ApiUrls.VoidCharge, chargeId);
            return _apiHttpClient.PostRequest<Void>(voidChargeApiUri, _configuration.SecretKey, requestModel);
        }

        public Task<HttpResponse<Refund>> RefundChargeAsync(string chargeId, ChargeRefund requestModel)
        {
            var chargeRefundsApiUri = string.Format(_configuration.ApiUrls.RefundCharge, chargeId);
            return _apiHttpClient.PostRequest<Refund>(chargeRefundsApiUri, _configuration.SecretKey, requestModel);
        }

        public Task<HttpResponse<Capture>> CaptureChargeAsync(string chargeId, ChargeCapture requestModel)
        {
            var captureChargesApiUri = string.Format(_configuration.ApiUrls.CaptureCharge, chargeId);
            return _apiHttpClient.PostRequest<Capture>(captureChargesApiUri, _configuration.SecretKey, requestModel);
        }

        public Task<HttpResponse<OkResponse>> UpdateChargeAsync(string chargeId, ChargeUpdate requestModel)
        {
            var updateChargesApiUri = string.Format(_configuration.ApiUrls.Charge, chargeId);
            return _apiHttpClient.PutRequest<OkResponse>(updateChargesApiUri, _configuration.SecretKey, requestModel);
        }

        public Task<HttpResponse<Charge>> GetChargeAsync(string chargeId)
        {
            var getChargeUri = string.Format(_configuration.ApiUrls.Charge, chargeId);
            return _apiHttpClient.GetRequest<Charge>(getChargeUri, _configuration.SecretKey);
        }

        public Task<HttpResponse<ChargeHistory>> GetChargeHistoryAsync(string chargeId)
        {
            var getChargeHistoryUri = string.Format(_configuration.ApiUrls.ChargeHistory, chargeId);
            return _apiHttpClient.GetRequest<ChargeHistory>(getChargeHistoryUri, _configuration.SecretKey);
        }

        public Task<HttpResponse<Charge>> VerifyChargeAsync(string paymentToken)
        {
            string chargeVerifyApiUri = string.Format(_configuration.ApiUrls.Charge, paymentToken);

            return _apiHttpClient.GetRequest<Charge>(chargeVerifyApiUri, _configuration.SecretKey);
        }
    }
}