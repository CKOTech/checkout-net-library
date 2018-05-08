using Checkout.ApiServices.Reporting.RequestModels;
using Checkout.ApiServices.Reporting.ResponseModels;
using Checkout.ApiServices.SharedModels;

namespace Checkout.ApiServices.Reporting
{
    public class ReportingService
    {
        private ApiHttpClient _apiHttpClient;
        private AppSettings _appSettings;
        public ReportingService(ApiHttpClient apiHttpclient, AppSettings appSettings)
        {
            _apiHttpClient = apiHttpclient;
            _appSettings = appSettings;
        }
        /// <summary>
        /// Search for a customer’s transaction by a date range and then drill down using further filters.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public HttpResponse<QueryTransactionResponse> QueryTransaction(QueryRequest requestModel)
        {
            return _apiHttpClient.PostRequest<QueryTransactionResponse>(_appSettings.ApiUrls.ReportingTransactions, _appSettings.SecretKey, requestModel);
        }

        /// <summary>
        /// Search for a customer’s chargebacks by a date range and then drill down using further filters.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public HttpResponse<QueryChargebackResponse> QueryChargeback(QueryRequest requestModel)
        {
            return _apiHttpClient.PostRequest<QueryChargebackResponse>(_appSettings.ApiUrls.ReportingChargebacks, _appSettings.SecretKey, requestModel);
        }
    }
}
