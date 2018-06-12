using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Checkout.ApiServices.SharedModels;

namespace Checkout
{
    public interface IApiHttpClient
    {
        Task<HttpResponse<T>> DeleteRequest<T>(string requestUri, string authenticationKey, [CallerMemberName] string callerFunction = "");
        Task<HttpResponse<T>> GetRequest<T>(string requestUri, string authenticationKey, [CallerMemberName] string callerFunction = "");
        Task<HttpResponse<T>> PostRequest<T>(string requestUri, string authenticationKey, object requestPayload = null, [CallerMemberName] string callerFunction = "");
        Task<HttpResponse<T>> PutRequest<T>(string requestUri, string authenticationKey, object requestPayload = null, [CallerMemberName] string callerFunction = "");
    }
}