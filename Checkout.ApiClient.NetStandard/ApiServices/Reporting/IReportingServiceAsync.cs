using Checkout.ApiServices.Reporting.RequestModels;
using Checkout.ApiServices.Reporting.ResponseModels;
using Checkout.ApiServices.SharedModels;
using System.Threading.Tasks;

namespace Checkout.ApiServices.Reporting
{
    public interface IReportingServiceAsync
    {
        Task<HttpResponse<QueryChargebackResponse>> QueryChargebackAsync(QueryRequest requestModel);
        Task<HttpResponse<QueryTransactionResponse>> QueryTransactionAsync(QueryRequest requestModel);
    }
}