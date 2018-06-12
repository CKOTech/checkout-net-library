using Checkout.ApiServices.SharedModels;
using Checkout.ApiServices.Tokens.RequestModels;
using Checkout.ApiServices.Tokens.ResponseModels;
using System.Threading.Tasks;

namespace Checkout.ApiServices.Tokens
{
    public interface ITokenServiceAsync
    {
        Task<HttpResponse<PaymentToken>> CreatePaymentTokenAsync(PaymentTokenCreate requestModel);
        Task<HttpResponse<CardTokenResponse>> CreateVisaCheckoutCardTokenAsync(VisaCheckoutTokenCreate requestModel);
        Task<HttpResponse<CardTokenCreate>> GetCardTokenAsync(TokenCard requestModel);
        Task<HttpResponse<OkResponse>> UpdatePaymentTokenAsync(string paymentToken, PaymentTokenUpdate requestModel);
    }
}