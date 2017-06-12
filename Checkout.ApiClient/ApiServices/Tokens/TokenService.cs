using Checkout.ApiServices.SharedModels;
using Checkout.ApiServices.Tokens.RequestModels;
using Checkout.ApiServices.Tokens.ResponseModels;

namespace Checkout.ApiServices.Tokens
{
    public class TokenService 
    {
        public HttpResponse<PaymentToken> CreatePaymentToken(PaymentTokenCreate requestModel)
        {
            return new ApiHttpClient().PostRequest<PaymentToken>(ApiUrls.PaymentToken, AppSettings.SecretKey, requestModel);
        }

        public HttpResponse<OkResponse> UpdatePaymentToken(string paymentToken, PaymentTokenUpdate requestModel)
        {
            var updatePaymentTokenUri = string.Format(ApiUrls.UpdatePaymentToken, paymentToken);
            return new ApiHttpClient().PutRequest<OkResponse>(updatePaymentTokenUri, AppSettings.SecretKey, requestModel);
        }

        public HttpResponse<CardTokenResponse> CreateVisaCheckoutCardToken(VisaCheckoutTokenCreate requestModel)
        {
            return new ApiHttpClient().PostRequest<CardTokenResponse>(ApiUrls.VisaCheckout, AppSettings.PublicKey, requestModel);
        }
    }
}
