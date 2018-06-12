using Checkout.ApiServices.Payouts.RequestModels;
using Checkout.ApiServices.Payouts.ResponseModels;
using Checkout.ApiServices.SharedModels;

namespace Checkout.ApiServices.Payouts
{
    public class PayoutsService : IPayoutsService
    {
        private IPayoutsServiceAsync _payoutsServiceAsync;

        public PayoutsService (IApiHttpClient apiHttpclient, CheckoutConfiguration configuration)
        {
            _payoutsServiceAsync = new PayoutsServiceAsync(apiHttpclient, configuration);
        }

        public HttpResponse<Payout> MakePayout(BasePayout requestModel)
        {
            return _payoutsServiceAsync.MakePayoutAsync(requestModel).Result;
        }
    }
}
