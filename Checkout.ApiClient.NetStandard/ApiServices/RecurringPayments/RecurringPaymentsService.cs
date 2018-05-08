using Checkout.ApiServices.RecurringPayments.RequestModels;
using Checkout.ApiServices.RecurringPayments.ResponseModels;
using Checkout.ApiServices.SharedModels;

namespace Checkout.ApiServices.RecurringPayments
{
    public class RecurringPaymentsService
    {
        private ApiHttpClient _apiHttpClient;
        private AppSettings _appSettings;
        public RecurringPaymentsService(ApiHttpClient apiHttpclient, AppSettings appSettings)
        {
            _apiHttpClient = apiHttpclient;
            _appSettings = appSettings;
        }
        /// <summary>
        /// Creates a Payment Plan that can exist independently and act as template. 
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public HttpResponse<SinglePaymentPlanCreateResponse> CreatePaymentPlan(SinglePaymentPlanCreateRequest requestModel)
        {
            return _apiHttpClient.PostRequest<SinglePaymentPlanCreateResponse>(_appSettings.ApiUrls.RecurringPaymentPlans, _appSettings.SecretKey, requestModel);
        }

        /// <summary>
        /// Updates an existing Payment Plan to the amended values of the parameters passed in the request.
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public HttpResponse<OkResponse> UpdatePaymentPlan(string planId, PaymentPlanUpdate requestModel)
        {
            return _apiHttpClient.PutRequest<OkResponse>(string.Format(_appSettings.ApiUrls.RecurringPaymentPlan, planId), _appSettings.SecretKey, requestModel);
        }

        /// <summary>
        /// Cancels an existing Payment Plan.
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public HttpResponse<OkResponse> CancelPaymentPlan(string planId)
        {
            return _apiHttpClient.DeleteRequest<OkResponse>(string.Format(_appSettings.ApiUrls.RecurringPaymentPlan, planId), _appSettings.SecretKey);
        }

        /// <summary>
        ///  Retrieves an existing Payment Plan.
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public HttpResponse<ResponsePaymentPlan> GetPaymentPlan(string planId)
        {
            return _apiHttpClient.GetRequest<ResponsePaymentPlan>(string.Format(_appSettings.ApiUrls.RecurringPaymentPlan, planId), _appSettings.SecretKey);
        }

        /// <summary>
        /// Searches for all Payment Plans created under a business based on the parameters passed in the request.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public HttpResponse<QueryPaymentPlanResponse> QueryPaymentPlan(QueryPaymentPlanRequest requestModel)
        {
            return _apiHttpClient.PostRequest<QueryPaymentPlanResponse>(_appSettings.ApiUrls.RecurringPaymentPlanSearch, _appSettings.SecretKey, requestModel);
        }

        /// <summary>
        /// Searches for all customers and their associated Payment Plans created under a business based on the parameters passed in the request.
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public HttpResponse<QueryCustomerPaymentPlanResponse> QueryCustomerPaymentPlan(QueryCustomerPaymentPlanRequest requestModel)
        {
            return _apiHttpClient.PostRequest<QueryCustomerPaymentPlanResponse>(_appSettings.ApiUrls.RecurringCustomerPaymentPlanSearch, _appSettings.SecretKey, requestModel);
        }

        /// <summary>
        /// Retrieves an existing Customer Payment Plan.
        /// </summary>
        /// <param name="customerPlanId"></param>
        /// <returns></returns>
        public HttpResponse<CustomerPaymentPlan> GetCustomerPaymentPlan(string customerPlanId)
        {
            return _apiHttpClient.GetRequest<CustomerPaymentPlan>(string.Format(_appSettings.ApiUrls.RecurringCustomerPaymentPlan, customerPlanId), _appSettings.SecretKey);
        }

        /// <summary>
        /// Cancels an existing Customer Payment Plan.
        /// </summary>
        /// <param name="customerPlanId"></param>
        /// <returns></returns>
        public HttpResponse<OkResponse> CancelCustomerPaymentPlan(string customerPlanId)
        {
            return _apiHttpClient.DeleteRequest<OkResponse>(string.Format(_appSettings.ApiUrls.RecurringCustomerPaymentPlan, customerPlanId), _appSettings.SecretKey);
        }

        /// <summary>
        /// Updates the card associated with the Customer Payment Plan and/or its status
        /// </summary>
        /// <param name="customerPlanId"></param>
        /// <returns></returns>
        public HttpResponse<OkResponse> UpdateCustomerPaymentPlan(string customerPlanId, CustomerPaymentPlanUpdate requestModel)
        {
            return _apiHttpClient.PutRequest<OkResponse>(string.Format(_appSettings.ApiUrls.RecurringCustomerPaymentPlan, customerPlanId), _appSettings.SecretKey, requestModel);
        }
    }
}
