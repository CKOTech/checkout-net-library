using Checkout.ApiServices.Lookups.ResponseModels;
using Checkout.ApiServices.SharedModels;

namespace Checkout.ApiServices.Lookups
{
    public class LookupsService
    {
        private ApiHttpClient _apiHttpClient;
        private AppSettings _appSettings;
        public LookupsService(ApiHttpClient apiHttpclient, AppSettings appSettings)
        {
            _apiHttpClient = apiHttpclient;
            _appSettings = appSettings;
        }
        public HttpResponse<CountryInfo> GetBinLookup(string bin)
        {
            var uri = string.Format(_appSettings.ApiUrls.BinLookup, bin);
            return _apiHttpClient.GetRequest<CountryInfo>(uri, _appSettings.SecretKey);
        }

        public HttpResponse<LocalPaymentData> GetLocalPaymentIssuerIds(string lppId)
        {
            var uri = string.Format(_appSettings.ApiUrls.LocalPaymentIssuerIdLookup, lppId);
            return _apiHttpClient.GetRequest<LocalPaymentData>(uri, _appSettings.SecretKey);
        }
    }
}
