using Checkout.ApiServices.Reporting.RequestModels;
using Checkout.ApiServices.Reporting.ResponseModels;
using Checkout.ApiServices.SharedModels;
using System.Threading.Tasks;

namespace Checkout.ApiServices.Reporting
{
    public class ReportingServiceAsync : IReportingServiceAsync
    {
        private IApiHttpClient _apiHttpClient;
        private CheckoutConfiguration _configuration;

        public ReportingServiceAsync(IApiHttpClient apiHttpclient, CheckoutConfiguration configuration)
        {
            _apiHttpClient = apiHttpclient;
            _configuration = configuration;
        }

        public Task<HttpResponse<QueryTransactionResponse>> QueryTransactionAsync(QueryRequest requestModel)
        {
            return _apiHttpClient.PostRequest<QueryTransactionResponse>(_configuration.ApiUrls.ReportingTransactions, _configuration.SecretKey, requestModel);
        }

        public Task<HttpResponse<QueryChargebackResponse>> QueryChargebackAsync(QueryRequest requestModel)
        {
            return _apiHttpClient.PostRequest<QueryChargebackResponse>(_configuration.ApiUrls.ReportingChargebacks, _configuration.SecretKey, requestModel);
        }
    }
}
