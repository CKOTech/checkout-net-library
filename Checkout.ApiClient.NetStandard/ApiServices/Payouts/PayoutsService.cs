using Checkout.ApiServices.Payouts.RequestModels;
using Checkout.ApiServices.Payouts.ResponseModels;
using Checkout.ApiServices.SharedModels;

namespace Checkout.ApiServices.Payouts
{
    public class PayoutsService
    {
        private ApiHttpClient _apiHttpClient;
        private AppSettings _appSettings;
        public PayoutsService (ApiHttpClient apiHttpclient, AppSettings appSettings)
        {
            _apiHttpClient = apiHttpclient;
            _appSettings = appSettings;
        }

        public HttpResponse<Payout> MakePayout(BasePayout requestModel)
        {
            var createPayoutsUri = string.Format(_appSettings.ApiUrls.Payouts);
            return _apiHttpClient.PostRequest<Payout>(createPayoutsUri, _appSettings.SecretKey, requestModel);
        }
    }
}
