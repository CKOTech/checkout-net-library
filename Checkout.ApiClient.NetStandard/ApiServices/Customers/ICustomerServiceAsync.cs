using Checkout.ApiServices.Customers.RequestModels;
using Checkout.ApiServices.Customers.ResponseModels;
using Checkout.ApiServices.SharedModels;
using System.Threading.Tasks;

namespace Checkout.ApiServices.Customers
{
    public interface ICustomerServiceAsync
    {
        Task<HttpResponse<Customer>> CreateCustomerAsync(CustomerCreate requestModel);
        Task<HttpResponse<OkResponse>> DeleteCustomerAsync(string customerId);
        Task<HttpResponse<Customer>> GetCustomerAsync(string identifier);
        Task<HttpResponse<CustomerList>> GetCustomerListAsync(CustomerGetList request);
        Task<HttpResponse<OkResponse>> UpdateCustomerAsync(string customerId, CustomerUpdate requestModel);
    }
}