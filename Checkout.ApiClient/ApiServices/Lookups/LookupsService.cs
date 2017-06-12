using Checkout.ApiServices.Lookups.ResponseModels;
using Checkout.ApiServices.SharedModels;

namespace Checkout.ApiServices.Lookups
{
    public class LookupsService
    {
        public HttpResponse<CountryInfo> GetBinLookup(string bin)
        {
            var uri = string.Format(ApiUrls.BinLookup, bin);
            return new ApiHttpClient().GetRequest<CountryInfo>(uri, AppSettings.SecretKey);
        }

        public HttpResponse<LocalPaymentData> GetLocalPaymentIssuerIds(string lppId)
        {
            var uri = string.Format(ApiUrls.LocalPaymentIssuerIdLookup, lppId);
            return new ApiHttpClient().GetRequest<LocalPaymentData>(uri, AppSettings.SecretKey);
        }
    }
}
