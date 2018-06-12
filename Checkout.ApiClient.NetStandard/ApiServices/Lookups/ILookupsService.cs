using Checkout.ApiServices.Lookups.ResponseModels;
using Checkout.ApiServices.SharedModels;
namespace Checkout.ApiServices.Lookups
{
    public interface ILookupsService
    {
        HttpResponse<CountryInfo> GetBinLookup(string bin);
        HttpResponse<LocalPaymentData> GetLocalPaymentIssuerIds(string lppId);
    }
}