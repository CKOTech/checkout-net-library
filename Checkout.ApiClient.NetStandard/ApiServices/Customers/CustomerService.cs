using Checkout.ApiServices.Customers.RequestModels;
using Checkout.ApiServices.Customers.ResponseModels;
using Checkout.ApiServices.SharedModels;
using Checkout.Utilities;

namespace Checkout.ApiServices.Customers

{
    public class CustomerService
    {
        private ApiHttpClient _apiHttpClient;
        private AppSettings _appSettings;
        public CustomerService(ApiHttpClient apiHttpclient, AppSettings appSettings)
        {
            _apiHttpClient = apiHttpclient;
            _appSettings = appSettings;
        }
        
        private string GetCustomerURI(string identifier)
        {
            // check for an email that contains a + character like: john.smith+checkout@email.com
            if(identifier.Contains("@") && identifier.Contains("+")) // TODO: Improve with RegEx 
            {
                return _appSettings.ApiUrls.CustomerViaEmail;
            } else
            {
                return _appSettings.ApiUrls.Customer;
            }
        }

        public HttpResponse<Customer> CreateCustomer(CustomerCreate requestModel)
        {
            return _apiHttpClient.PostRequest<Customer>(_appSettings.ApiUrls.Customers, _appSettings.SecretKey, requestModel);
        }

        public HttpResponse<OkResponse> UpdateCustomer(string customerId, CustomerUpdate requestModel)
        {
            var updateCustomerUri = string.Format(_appSettings.ApiUrls.Customer, customerId);
            return _apiHttpClient.PutRequest<OkResponse>(updateCustomerUri, _appSettings.SecretKey, requestModel);
        }

        public HttpResponse<OkResponse> DeleteCustomer(string customerId)
        {
            var deleteCustomerUri = string.Format(_appSettings.ApiUrls.Customer, customerId);
            return _apiHttpClient.DeleteRequest<OkResponse>(deleteCustomerUri, _appSettings.SecretKey);
        }

        public HttpResponse<Customer> GetCustomer(string identifier)
        {
            var getCustomerUri = string.Format(GetCustomerURI(identifier), identifier);
            return _apiHttpClient.GetRequest<Customer>(getCustomerUri, _appSettings.SecretKey);
        }

        public HttpResponse<CustomerList> GetCustomerList(CustomerGetList request)
        {
            var getCustomerListUri = _appSettings.ApiUrls.Customers;

            if (request.Count.HasValue)
            {
                getCustomerListUri = UrlHelper.AddParameterToUrl(getCustomerListUri, "count", request.Count.ToString());
            }

            if (request.Offset.HasValue)
            {
                getCustomerListUri = UrlHelper.AddParameterToUrl(getCustomerListUri, "offset", request.Offset.ToString());
            }

            if (request.FromDate.HasValue)
            {
                getCustomerListUri = UrlHelper.AddParameterToUrl(getCustomerListUri, "fromDate", DateTimeHelper.FormatAsUtc(request.FromDate.Value));
            }

            if (request.ToDate.HasValue)
            {
                getCustomerListUri = UrlHelper.AddParameterToUrl(getCustomerListUri, "toDate", DateTimeHelper.FormatAsUtc(request.ToDate.Value));
            }

            return _apiHttpClient.GetRequest<CustomerList>(getCustomerListUri, _appSettings.SecretKey);
        }
    }

}