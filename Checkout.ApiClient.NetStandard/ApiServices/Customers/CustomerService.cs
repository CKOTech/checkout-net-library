using Checkout.ApiServices.Customers.RequestModels;
using Checkout.ApiServices.Customers.ResponseModels;
using Checkout.ApiServices.SharedModels;

namespace Checkout.ApiServices.Customers

{
    public class CustomerService : ICustomerService
    {
        private ICustomerServiceAsync _customerServiceAsync;

        public CustomerService(IApiHttpClient apiHttpclient, CheckoutConfiguration configuration)
        {
            _customerServiceAsync = new CustomerServiceAsync(apiHttpclient, configuration);
        }

        public HttpResponse<Customer> CreateCustomer(CustomerCreate requestModel)
        {
            return _customerServiceAsync.CreateCustomerAsync(requestModel).Result;
        }

        public HttpResponse<OkResponse> UpdateCustomer(string customerId, CustomerUpdate requestModel)
        {
            return _customerServiceAsync.UpdateCustomerAsync(customerId, requestModel).Result;
        }

        public HttpResponse<OkResponse> DeleteCustomer(string customerId)
        {
            return _customerServiceAsync.DeleteCustomerAsync(customerId).Result;
        }

        public HttpResponse<Customer> GetCustomer(string identifier)
        {
            return _customerServiceAsync.GetCustomerAsync(identifier).Result;
        }

        public HttpResponse<CustomerList> GetCustomerList(CustomerGetList request)
        {
            return _customerServiceAsync.GetCustomerListAsync(request).Result;
        }
    }
}