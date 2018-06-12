using Checkout.ApiServices.Payouts.RequestModels;
using Checkout.ApiServices.Payouts.ResponseModels;
using Checkout.ApiServices.SharedModels;
using System.Threading.Tasks;

namespace Checkout.ApiServices.Payouts
{
    public class PayoutsServiceAsync : IPayoutsServiceAsync
    {
        private IApiHttpClient _apiHttpClient;
        private CheckoutConfiguration _configuration;

        public PayoutsServiceAsync (IApiHttpClient apiHttpclient, CheckoutConfiguration configuration)
        {
            _apiHttpClient = apiHttpclient;
            _configuration = configuration;
        }

        public Task<HttpResponse<Payout>> MakePayoutAsync(BasePayout requestModel)
        {
            var createPayoutsUri = string.Format(_configuration.ApiUrls.Payouts);
            return _apiHttpClient.PostRequest<Payout>(createPayoutsUri, _configuration.SecretKey, requestModel);
        }
    }
}
