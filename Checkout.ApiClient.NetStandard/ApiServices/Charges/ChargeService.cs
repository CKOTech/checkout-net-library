using Checkout.ApiServices.Charges.RequestModels;
using Checkout.ApiServices.Charges.ResponseModels;
using Checkout.ApiServices.SharedModels;
using Checkout.Utilities;
namespace Checkout.ApiServices.Charges
{
    public class ChargeService
    {
        private ApiHttpClient _apiHttpClient;
        private AppSettings _appSettings;
        public ChargeService(ApiHttpClient apiHttpclient, AppSettings appSettings)
        {
            _apiHttpClient = apiHttpclient;
            _appSettings = appSettings;
        }

        /// <summary>
        /// Creates a charge with full card details.
        /// </summary>
        public HttpResponse<Charge> ChargeWithCard(CardCharge requestModel)
        {
            return _apiHttpClient.PostRequest<Charge>(_appSettings.ApiUrls.CardCharge, _appSettings.SecretKey, requestModel);
        }

        /// <summary>
        /// Creates a charge with card id.
        /// </summary>
        public HttpResponse<Charge> ChargeWithCardId(CardIdCharge requestModel)
        {
            return _apiHttpClient.PostRequest<Charge>(_appSettings.ApiUrls.CardCharge, _appSettings.SecretKey, requestModel);
        }

        /// <summary>
        /// Creates a charge with card token.
        /// </summary>
        public HttpResponse<Charge> ChargeWithCardToken(CardTokenCharge requestModel)
        {
            return _apiHttpClient.PostRequest<Charge>(_appSettings.ApiUrls.CardTokenCharge, _appSettings.SecretKey, requestModel);
        }

        /// <summary>
        /// Creates a charge with the default card of the customer.
        /// </summary>
        public HttpResponse<Charge> ChargeWithDefaultCustomerCard(DefaultCardCharge requestModel)
        {
            return _apiHttpClient.PostRequest<Charge>(_appSettings.ApiUrls.DefaultCardCharge, _appSettings.SecretKey, requestModel);
        }

        /// <summary>
        /// Creates a charge with an alternative/local payment.
        /// </summary>p
        /// <param name="requestModel">The request model.</param>
        /// <returns></returns>
        public HttpResponse<Charge> ChargeWithLocalPayment(LocalPaymentCharge requestModel)
        {
            return _apiHttpClient.PostRequest<Charge>(_appSettings.ApiUrls.LocalPaymentCharge, _appSettings.SecretKey, requestModel);
        }

        /// <summary>
        /// Voids an authorised charge. If charge has been captured you cannot perform void operation.
        /// </summary>
        public HttpResponse<Void> VoidCharge(string chargeId, ChargeVoid requestModel)
        {
            var voidChargeApiUri = string.Format(_appSettings.ApiUrls.VoidCharge, chargeId);
            return _apiHttpClient.PostRequest<Void>(voidChargeApiUri, _appSettings.SecretKey, requestModel);
        }

        /// <summary>
        /// Refunds a captured charge.
        /// </summary>
        public HttpResponse<Refund> RefundCharge(string chargeId, ChargeRefund requestModel)
        {
            var chargeRefundsApiUri = string.Format(_appSettings.ApiUrls.RefundCharge, chargeId);
            return _apiHttpClient.PostRequest<Refund>(chargeRefundsApiUri, _appSettings.SecretKey, requestModel);
        }

        /// <summary>
        /// Captures an authorised charge.
        /// </summary>
        public HttpResponse<Capture> CaptureCharge(string chargeId, ChargeCapture requestModel)
        {
            var captureChargesApiUri = string.Format(_appSettings.ApiUrls.CaptureCharge, chargeId);
            return _apiHttpClient.PostRequest<Capture>(captureChargesApiUri, _appSettings.SecretKey, requestModel);
        }

        /// <summary>
        /// Updates a charge.
        /// </summary>
        public HttpResponse<OkResponse> UpdateCharge(string chargeId, ChargeUpdate requestModel)
        {
            var updateChargesApiUri = string.Format(_appSettings.ApiUrls.Charge, chargeId);
            return _apiHttpClient.PutRequest<OkResponse>(updateChargesApiUri, _appSettings.SecretKey, requestModel);
        }

        /// <summary>
        /// Retrieves a charge by chargeId
        /// </summary>
        public HttpResponse<Charge> GetCharge(string chargeId)
        {
            var getChargeUri = string.Format(_appSettings.ApiUrls.Charge, chargeId);
            return _apiHttpClient.GetRequest<Charge>(getChargeUri, _appSettings.SecretKey);
        }

        /// <summary>
        /// Retrieves charge history by chargeId
        /// </summary>
        public HttpResponse<ChargeHistory> GetChargeHistory(string chargeId)
        {
            var getChargeHistoryUri = string.Format(_appSettings.ApiUrls.ChargeHistory, chargeId);
            return _apiHttpClient.GetRequest<ChargeHistory>(getChargeHistoryUri, _appSettings.SecretKey);
        }

        /// <summary>
        /// Retrieves a charge by payment token or chargeId
        /// </summary>
        public HttpResponse<Charge> VerifyCharge(string paymentToken)
        {
            string chargeVerifyApiUri = string.Format(_appSettings.ApiUrls.Charge, paymentToken);

            return _apiHttpClient.GetRequest<Charge>(chargeVerifyApiUri, _appSettings.SecretKey);
        }
    }
}