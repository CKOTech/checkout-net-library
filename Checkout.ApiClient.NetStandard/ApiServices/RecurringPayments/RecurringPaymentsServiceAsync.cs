using Checkout.ApiServices.RecurringPayments.RequestModels;
using Checkout.ApiServices.RecurringPayments.ResponseModels;
using Checkout.ApiServices.SharedModels;
using System.Threading.Tasks;

namespace Checkout.ApiServices.RecurringPayments
{
    public class RecurringPaymentsServiceAsync : IRecurringPaymentsServiceAsync
    {
        private IApiHttpClient _apiHttpClient;
        private CheckoutConfiguration _configuration;

        public RecurringPaymentsServiceAsync(IApiHttpClient apiHttpclient, CheckoutConfiguration configuration)
        {
            _apiHttpClient = apiHttpclient;
            _configuration = configuration;
        }

        public Task<HttpResponse<SinglePaymentPlanCreateResponse>> CreatePaymentPlanAsync(SinglePaymentPlanCreateRequest requestModel)
        {
            return _apiHttpClient.PostRequest<SinglePaymentPlanCreateResponse>(_configuration.ApiUrls.RecurringPaymentPlans, _configuration.SecretKey, requestModel);
        }

        public Task<HttpResponse<OkResponse>> UpdatePaymentPlanAsync(string planId, PaymentPlanUpdate requestModel)
        {
            return _apiHttpClient.PutRequest<OkResponse>(string.Format(_configuration.ApiUrls.RecurringPaymentPlan, planId), _configuration.SecretKey, requestModel);
        }

        public Task<HttpResponse<OkResponse>> CancelPaymentPlanAsync(string planId)
        {
            return _apiHttpClient.DeleteRequest<OkResponse>(string.Format(_configuration.ApiUrls.RecurringPaymentPlan, planId), _configuration.SecretKey);
        }

        public Task<HttpResponse<ResponsePaymentPlan>> GetPaymentPlanAsync(string planId)
        {
            return _apiHttpClient.GetRequest<ResponsePaymentPlan>(string.Format(_configuration.ApiUrls.RecurringPaymentPlan, planId), _configuration.SecretKey);
        }

        public Task<HttpResponse<QueryPaymentPlanResponse>> QueryPaymentPlanAsync(QueryPaymentPlanRequest requestModel)
        {
            return _apiHttpClient.PostRequest<QueryPaymentPlanResponse>(_configuration.ApiUrls.RecurringPaymentPlanSearch, _configuration.SecretKey, requestModel);
        }

        public Task<HttpResponse<QueryCustomerPaymentPlanResponse>> QueryCustomerPaymentPlanAsync(QueryCustomerPaymentPlanRequest requestModel)
        {
            return _apiHttpClient.PostRequest<QueryCustomerPaymentPlanResponse>(_configuration.ApiUrls.RecurringCustomerPaymentPlanSearch, _configuration.SecretKey, requestModel);
        }

        public Task<HttpResponse<CustomerPaymentPlan>> GetCustomerPaymentPlanAsync(string customerPlanId)
        {
            return _apiHttpClient.GetRequest<CustomerPaymentPlan>(string.Format(_configuration.ApiUrls.RecurringCustomerPaymentPlan, customerPlanId), _configuration.SecretKey);
        }

        public Task<HttpResponse<OkResponse>> CancelCustomerPaymentPlanAsync(string customerPlanId)
        {
            return _apiHttpClient.DeleteRequest<OkResponse>(string.Format(_configuration.ApiUrls.RecurringCustomerPaymentPlan, customerPlanId), _configuration.SecretKey);
        }

        public Task<HttpResponse<OkResponse>> UpdateCustomerPaymentPlanAsync(string customerPlanId, CustomerPaymentPlanUpdate requestModel)
        {
            return _apiHttpClient.PutRequest<OkResponse>(string.Format(_configuration.ApiUrls.RecurringCustomerPaymentPlan, customerPlanId), _configuration.SecretKey, requestModel);
        }
    }
}
