using Checkout.ApiServices.Reporting.RequestModels;
using Checkout.ApiServices.Reporting.ResponseModels;
using Checkout.ApiServices.SharedModels;

namespace Checkout.ApiServices.Reporting
{
    public class ReportingService : IReportingService
    {
        private IReportingServiceAsync _reportingServiceAsync;

        public ReportingService(IApiHttpClient apiHttpclient, CheckoutConfiguration configuration)
        {
            _reportingServiceAsync = new ReportingServiceAsync(apiHttpclient, configuration);
        }

        public HttpResponse<QueryTransactionResponse> QueryTransaction(QueryRequest requestModel)
        {
            return _reportingServiceAsync.QueryTransactionAsync(requestModel).Result;
        }

        public HttpResponse<QueryChargebackResponse> QueryChargeback(QueryRequest requestModel)
        {
            return _reportingServiceAsync.QueryChargebackAsync(requestModel).Result;
        }
    }
}
