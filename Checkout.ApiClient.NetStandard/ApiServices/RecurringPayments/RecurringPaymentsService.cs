using Checkout.ApiServices.RecurringPayments.RequestModels;
using Checkout.ApiServices.RecurringPayments.ResponseModels;
using Checkout.ApiServices.SharedModels;

namespace Checkout.ApiServices.RecurringPayments
{
    public class RecurringPaymentsService : IRecurringPaymentsService
    {
        private IRecurringPaymentsServiceAsync _recurringPaymentsServiceAsync;

        public RecurringPaymentsService(IApiHttpClient apiHttpclient, CheckoutConfiguration configuration)
        {
            _recurringPaymentsServiceAsync = new RecurringPaymentsServiceAsync(apiHttpclient, configuration);
        }

        public HttpResponse<SinglePaymentPlanCreateResponse> CreatePaymentPlan(SinglePaymentPlanCreateRequest requestModel)
        {
            return _recurringPaymentsServiceAsync.CreatePaymentPlanAsync(requestModel).Result;
        }

        public HttpResponse<OkResponse> UpdatePaymentPlan(string planId, PaymentPlanUpdate requestModel)
        {
            return _recurringPaymentsServiceAsync.UpdatePaymentPlanAsync(planId, requestModel).Result;
        }

        public HttpResponse<OkResponse> CancelPaymentPlan(string planId)
        {
            return _recurringPaymentsServiceAsync.CancelPaymentPlanAsync(planId).Result;
        }

        public HttpResponse<ResponsePaymentPlan> GetPaymentPlan(string planId)
        {
            return _recurringPaymentsServiceAsync.GetPaymentPlanAsync(planId).Result;
        }

        public HttpResponse<QueryPaymentPlanResponse> QueryPaymentPlan(QueryPaymentPlanRequest requestModel)
        {
            return _recurringPaymentsServiceAsync.QueryPaymentPlanAsync(requestModel).Result;
        }

        public HttpResponse<QueryCustomerPaymentPlanResponse> QueryCustomerPaymentPlan(QueryCustomerPaymentPlanRequest requestModel)
        {
            return _recurringPaymentsServiceAsync.QueryCustomerPaymentPlanAsync(requestModel).Result;
        }

        public HttpResponse<CustomerPaymentPlan> GetCustomerPaymentPlan(string customerPlanId)
        {
            return _recurringPaymentsServiceAsync.GetCustomerPaymentPlanAsync(customerPlanId).Result;
        }

        public HttpResponse<OkResponse> CancelCustomerPaymentPlan(string customerPlanId)
        {
            return _recurringPaymentsServiceAsync.CancelCustomerPaymentPlanAsync(customerPlanId).Result;
        }

        public HttpResponse<OkResponse> UpdateCustomerPaymentPlan(string customerPlanId, CustomerPaymentPlanUpdate requestModel)
        {
            return _recurringPaymentsServiceAsync.UpdateCustomerPaymentPlanAsync(customerPlanId, requestModel).Result;
        }
    }
}
