using Checkout.ApiServices.SharedModels;
using Checkout.Helpers;
using Checkout.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout
{
    /// <summary>
    /// Handles http requests and responses
    /// </summary>
    ///

    public sealed class ApiHttpClient : IApiHttpClient
    {
        public CheckoutConfiguration configuration;
        private HttpClientHandler requestHandler;

        private HttpClient httpClient;

        public ApiHttpClient(CheckoutConfiguration configuration)
        {
            this.configuration = configuration;
            ResetHandler();
        }

        private void ResetHandler()
        {
            if (requestHandler != null)
            {
                requestHandler.Dispose();
            }

            requestHandler = new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip,
                AllowAutoRedirect = false,
                UseDefaultCredentials = false,
                UseCookies = false
            };

            if (httpClient != null)
            {
                httpClient.Dispose();
            }

            httpClient = new HttpClient(requestHandler)
            {
                MaxResponseContentBufferSize = configuration.MaxResponseContentBufferSize,
                Timeout = TimeSpan.FromSeconds(configuration.RequestTimeout)
            };

            SetHttpRequestHeader("User-Agent", CheckoutConfiguration.ClientUserAgentName);
            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("Gzip"));
        }

        public void SetHttpRequestHeader(string name, string value)
        {
            if (httpClient.DefaultRequestHeaders.Contains(name))
            {
                httpClient.DefaultRequestHeaders.Remove(name);
            }

            if (value != null)
            { httpClient.DefaultRequestHeaders.Add(name, value); }
        }

        public string GetHttpRequestHeader(string name)
        {
            httpClient.DefaultRequestHeaders.TryGetValues(name, out IEnumerable<string> values);

            if (values != null && values.Any())
            { return values.First(); }

            return null;
        }

        public string callerSection = "";
        /// <summary>
        /// Submits a get request to the given web address with default content type e.g. text/plain
        /// </summary>
        public Task<HttpResponse<T>> GetRequest<T>(string requestUri, string authenticationKey, [CallerMemberName] string callerFunction = "")
        {
            var httpRequestMsg = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(requestUri)
            };
            httpRequestMsg.Headers.Add("Accept", CheckoutConfiguration.DefaultContentType);

            SetHttpRequestHeader("Authorization", authenticationKey);

            callerSection = callerFunction;
            return SendRequest<T>(httpRequestMsg);
        }

        /// <summary>
        /// Submits a post request to the given web address
        /// </summary>
        public Task<HttpResponse<T>> PostRequest<T>(string requestUri, string authenticationKey, object requestPayload = null, [CallerMemberName] string callerFunction = "")
        {
            var httpRequestMsg = new HttpRequestMessage(HttpMethod.Post, requestUri);
            var requestPayloadAsString = GetObjectAsString(requestPayload);

            httpRequestMsg.Content = new StringContent(requestPayloadAsString, Encoding.UTF8, CheckoutConfiguration.DefaultContentType);
            httpRequestMsg.Headers.Add("Accept", CheckoutConfiguration.DefaultContentType);

            SetHttpRequestHeader("Authorization", authenticationKey);

            callerSection = callerFunction;
            return SendRequest<T>(httpRequestMsg, requestPayloadAsString);
        }

        /// <summary>
        /// Submits a put request to the given web address
        /// </summary>
        public Task<HttpResponse<T>> PutRequest<T>(string requestUri, string authenticationKey, object requestPayload = null, [CallerMemberName] string callerFunction = "")
        {
            var httpRequestMsg = new HttpRequestMessage(HttpMethod.Put, requestUri);
            var requestPayloadAsString = GetObjectAsString(requestPayload);

            httpRequestMsg.Content = new StringContent(requestPayloadAsString, Encoding.UTF8, CheckoutConfiguration.DefaultContentType);
            httpRequestMsg.Headers.Add("Accept", CheckoutConfiguration.DefaultContentType);

            SetHttpRequestHeader("Authorization", authenticationKey);

            callerSection = callerFunction;
            return SendRequest<T>(httpRequestMsg, requestPayloadAsString);
        }

        /// <summary>
        /// Submits a delete request to the given web address
        /// </summary>
        public Task<HttpResponse<T>> DeleteRequest<T>(string requestUri, string authenticationKey, [CallerMemberName] string callerFunction = "")
        {
            var httpRequestMsg = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(requestUri)
            };
            httpRequestMsg.Headers.Add("Accept", CheckoutConfiguration.DefaultContentType);

            SetHttpRequestHeader("Authorization", authenticationKey);

            callerSection = callerFunction;
            return SendRequest<T>(httpRequestMsg);
        }

        /// <summary>
        /// Sends a http request with the given object. All headers should be set manually here e.g. content type=application/json
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        private async Task<HttpResponse<T>> SendRequest<T>(HttpRequestMessage request, string payload = null)
        {
            HttpResponse<T> response = null;
            HttpResponseMessage responseMessage = null;
            string responseAsString = null;
            string responseCode = null;

            try
            {
#if NET45 
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
#endif
                responseMessage = await httpClient.SendAsync(request);

                responseCode = responseMessage.StatusCode.ToString();

                var responseContent = await responseMessage.Content.ReadAsByteArrayAsync();

                if (responseContent != null && responseContent.Length > 0)
                {
                    responseAsString = Encoding.UTF8.GetString(responseContent);

                    if (configuration.DebugMode)
                    {
                        Console.WriteLine(string.Format("\n<{0}>", callerSection));
                        Console.WriteLine(string.Format("\n- HttpRequest\n\tof Type:\t\t\t{0}\n\tto API Endpoint:\t\t{1}\n\twith Authorization:\t{2}", request.Method.ToString().ToUpper(), request.RequestUri, request.Headers.Authorization.ToString()));
                        if (payload != null)
                        {
                            Console.WriteLine(string.Format("\twith Payload:\t\t{0}", payload));
                        }
                        Console.WriteLine(string.Format("\n- HttpResponse\n\treturns Status:\t\t{0}\n\twith Payload:\t\t{1}\n", responseMessage.StatusCode, responseAsString));
                        Console.WriteLine(string.Format("</{0}>\n", callerSection));
                        callerSection = "";
                    }
                }
                response = CreateHttpResponse<T>(responseAsString, responseMessage.StatusCode);
            }
            catch (Exception ex)
            {
                if (configuration.DebugMode)
                {
                    Console.WriteLine(string.Format("\n- Exception thrown: {0}\treturns Status:\t{1}\n\twith Payload:\t{2}", ExceptionHelper.FlattenExceptionMessages(ex), (responseMessage != null ? responseMessage.StatusCode.ToString() : string.Empty), responseAsString));
                }

                responseCode = "Exception: " + ex.Message;

                throw;
            }
            finally
            {
                request.Dispose();
                ResetHandler();
            }

            return response;
        }

        private HttpResponse<T> CreateHttpResponse<T>(string responseAsString, HttpStatusCode httpStatusCode)
        {
            if (httpStatusCode == HttpStatusCode.OK && responseAsString != null)
            {
                return new HttpResponse<T>(GetResponseAsObject<T>(responseAsString))
                {
                    HttpStatusCode = httpStatusCode
                };
            }
            else if (responseAsString != null)
            {
                return new HttpResponse<T>(default(T))
                {
                    Error = GetResponseAsObject<ResponseError>(responseAsString),
                    HttpStatusCode = httpStatusCode
                };
            }

            return null;
        }

        private string GetObjectAsString(object requestModel)
        {
            return ContentAdaptor.ConvertToJsonString(requestModel);
        }

        private T GetResponseAsObject<T>(string responseAsString)
        {
            return ContentAdaptor.JsonStringToObject<T>(responseAsString);
        }
    }
}