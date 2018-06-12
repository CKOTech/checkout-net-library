using Checkout.ApiServices.SharedModels;
using Checkout.ApiServices.Tokens.RequestModels;
using Checkout.ApiServices.Tokens.ResponseModels;
using System.Threading.Tasks;

namespace Checkout.ApiServices.Tokens
{
    public class TokenServiceAsync : ITokenServiceAsync
    {
        private IApiHttpClient _apiHttpClient;
        private CheckoutConfiguration _configuration;

        public TokenServiceAsync(IApiHttpClient apiHttpclient, CheckoutConfiguration configuration)
        {
            _apiHttpClient = apiHttpclient;
            _configuration = configuration;
        }
        public Task<HttpResponse<PaymentToken>> CreatePaymentTokenAsync(PaymentTokenCreate requestModel)
        {
            return _apiHttpClient.PostRequest<PaymentToken>(_configuration.ApiUrls.PaymentToken, _configuration.SecretKey, requestModel);
        }

        public Task<HttpResponse<OkResponse>> UpdatePaymentTokenAsync(string paymentToken, PaymentTokenUpdate requestModel)
        {
            var updatePaymentTokenUri = string.Format(_configuration.ApiUrls.UpdatePaymentToken, paymentToken);
            return _apiHttpClient.PutRequest<OkResponse>(updatePaymentTokenUri, _configuration.SecretKey, requestModel);
        }

        public Task<HttpResponse<CardTokenResponse>> CreateVisaCheckoutCardTokenAsync(VisaCheckoutTokenCreate requestModel)
        {
            return _apiHttpClient.PostRequest<CardTokenResponse>(_configuration.ApiUrls.VisaCheckout, _configuration.PublicKey, requestModel);
        }

        // Do not use the GetCardTokenAsync() method in live production. The cardToken is part of the response when you Checkout.com solutions like Checkout.js and Frames in your shop.
        public Task<HttpResponse<CardTokenCreate>> GetCardTokenAsync(TokenCard requestModel)
        {
            return _apiHttpClient.PostRequest<CardTokenCreate>(_configuration.ApiUrls.CardToken, _configuration.PublicKey, requestModel);
        }
    }
}
