using Checkout.ApiServices.Reporting.RequestModels;
using Checkout.ApiServices.Reporting.ResponseModels;
using Checkout.ApiServices.SharedModels;

namespace Checkout.ApiServices.Reporting
{
    public class ReportingService
    {
        /// <summary>
        /// Search for a customer’s transaction by a date range and then drill down using further filters.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public HttpResponse<QueryTransactionResponse> QueryTransaction(QueryRequest requestModel)
        {
            return new ApiHttpClient().PostRequest<QueryTransactionResponse>(ApiUrls.ReportingTransactions, AppSettings.SecretKey, requestModel);
        }

        /// <summary>
        /// Search for a customer’s chargebacks by a date range and then drill down using further filters.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public HttpResponse<QueryChargebackResponse> QueryChargeback(QueryRequest requestModel)
        {
            return new ApiHttpClient().PostRequest<QueryChargebackResponse>(ApiUrls.ReportingChargebacks, AppSettings.SecretKey, requestModel);
        }
    }
}
