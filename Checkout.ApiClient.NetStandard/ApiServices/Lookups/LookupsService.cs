using Checkout.ApiServices.Lookups.ResponseModels;
using Checkout.ApiServices.SharedModels;

namespace Checkout.ApiServices.Lookups
{
    public class LookupsService : ILookupsService
    {
        private ILookupsServiceAsync _lookupsServiceAsync;

        public LookupsService(IApiHttpClient apiHttpclient, CheckoutConfiguration configuration)
        {
            _lookupsServiceAsync = new LookupsServiceAsync(apiHttpclient, configuration);
        }
        public HttpResponse<CountryInfo> GetBinLookup(string bin)
        {
            return _lookupsServiceAsync.GetBinLookupAsync(bin).Result;
        }

        public HttpResponse<LocalPaymentData> GetLocalPaymentIssuerIds(string lppId)
        {
            return _lookupsServiceAsync.GetLocalPaymentIssuerIdsAsync(lppId).Result;
        }
    }
}
