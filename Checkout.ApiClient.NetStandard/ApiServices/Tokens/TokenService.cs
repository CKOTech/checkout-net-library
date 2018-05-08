using Checkout.ApiServices.SharedModels;
using Checkout.ApiServices.Tokens.RequestModels;
using Checkout.ApiServices.Tokens.ResponseModels;

namespace Checkout.ApiServices.Tokens
{
    public class TokenService
    {
        private ApiHttpClient _apiHttpClient;
        private AppSettings _appSettings;
        public TokenService(ApiHttpClient apiHttpclient, AppSettings appSettings)
        {
            _apiHttpClient = apiHttpclient;
            _appSettings = appSettings;
        }
        public HttpResponse<PaymentToken> CreatePaymentToken(PaymentTokenCreate requestModel)
        {
            return _apiHttpClient.PostRequest<PaymentToken>(_appSettings.ApiUrls.PaymentToken, _appSettings.SecretKey, requestModel);
        }

        public HttpResponse<OkResponse> UpdatePaymentToken(string paymentToken, PaymentTokenUpdate requestModel)
        {
            var updatePaymentTokenUri = string.Format(_appSettings.ApiUrls.UpdatePaymentToken, paymentToken);
            return _apiHttpClient.PutRequest<OkResponse>(updatePaymentTokenUri, _appSettings.SecretKey, requestModel);
        }

        public HttpResponse<CardTokenResponse> CreateVisaCheckoutCardToken(VisaCheckoutTokenCreate requestModel)
        {
            return _apiHttpClient.PostRequest<CardTokenResponse>(_appSettings.ApiUrls.VisaCheckout, _appSettings.PublicKey, requestModel);
        }

        public HttpResponse<CardTokenCreate> GetCardToken(TokenCard requestModel)
        {
            return _apiHttpClient.PostRequest<CardTokenCreate>(_appSettings.ApiUrls.CardToken, _appSettings.PublicKey, requestModel);
        }
    }
}
