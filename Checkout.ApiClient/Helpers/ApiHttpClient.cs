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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Checkout
{
    /// <summary>
    /// Handles http requests and responses
    /// </summary>
    public sealed class ApiHttpClient
    {
        private WebRequestHandler requestHandler;
        private HttpClient httpClient;

        public ApiHttpClient()
        {
            ResetHandler();
        }

        public void ResetHandler()
        {
            if (requestHandler != null)
            {
                requestHandler.Dispose();
            }
            requestHandler = new WebRequestHandler
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

            httpClient = new HttpClient(requestHandler);
            httpClient.MaxResponseContentBufferSize = AppSettings.MaxResponseContentBufferSize;
            httpClient.Timeout = TimeSpan.FromSeconds(AppSettings.RequestTimeout);
            SetHttpRequestHeader("User-Agent",AppSettings.ClientUserAgentName);
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
            IEnumerable<string> values = null;
            httpClient.DefaultRequestHeaders.TryGetValues(name, out values);

            if (values != null && values.Any())
            { return values.First(); }

            return null;
        }

       
        /// <summary>
        /// Submits a get request to the given web address with default content type e.g. text/plain
        /// </summary>
        public HttpResponse<T> GetRequest<T>(string requestUri,string authenticationKey)
        {
            var httpRequestMsg = new HttpRequestMessage();

            httpRequestMsg.Method = HttpMethod.Get;
            httpRequestMsg.RequestUri = new Uri(requestUri);
            httpRequestMsg.Headers.Add("Accept", AppSettings.DefaultContentType);

            SetHttpRequestHeader("Authorization", authenticationKey);

            if (AppSettings.DebugMode)
            {
                Console.WriteLine(string.Format("\n\n** Request ** Post {0}", requestUri));
            }

#if DEBUGNET45
            return SendRequest<T>(httpRequestMsg).Result;
#elif RELEASENET45
           
            return SendRequest<T>(httpRequestMsg).Result;
#else
            return SendRequest<T>(httpRequestMsg);
#endif
        }

        /// <summary>
        /// Submits a post request to the given web address
        /// </summary>
        public HttpResponse<T> PostRequest<T>(string requestUri,string authenticationKey, object requestPayload = null)
        {
            var httpRequestMsg = new HttpRequestMessage(HttpMethod.Post, requestUri);
            var requestPayloadAsString = GetObjectAsString(requestPayload);

            httpRequestMsg.Content = new StringContent(requestPayloadAsString, Encoding.UTF8, AppSettings.DefaultContentType);
            httpRequestMsg.Headers.Add("Accept", AppSettings.DefaultContentType);
            
            SetHttpRequestHeader("Authorization", authenticationKey);
            
            if (AppSettings.DebugMode)
            {
                Console.WriteLine(string.Format("\n\n** Request ** Post {0}", requestUri));
                Console.WriteLine(string.Format("\n\n** Payload ** \n {0} \n", requestPayloadAsString));
            }

#if DEBUGNET45
            return SendRequest<T>(httpRequestMsg).Result;
#elif RELEASENET45
           
            return SendRequest<T>(httpRequestMsg).Result;
#else
            return SendRequest<T>(httpRequestMsg);
#endif
        }

        /// <summary>
        /// Submits a put request to the given web address
        /// </summary>
        public HttpResponse<T> PutRequest<T>(string requestUri, string authenticationKey, object requestPayload = null)
        {
            var httpRequestMsg = new HttpRequestMessage(HttpMethod.Put, requestUri);
            var requestPayloadAsString = GetObjectAsString(requestPayload);

            httpRequestMsg.Content = new StringContent(requestPayloadAsString, Encoding.UTF8, AppSettings.DefaultContentType);
            httpRequestMsg.Headers.Add("Accept", AppSettings.DefaultContentType);

            SetHttpRequestHeader("Authorization", authenticationKey);

            if (AppSettings.DebugMode)
            {
                Console.WriteLine(string.Format("\n\n** Request ** Put {0}", requestUri));
                Console.WriteLine(string.Format("\n\n** Payload ** \n {0} \n", requestPayloadAsString));
            }

#if DEBUGNET45
            return SendRequest<T>(httpRequestMsg).Result;
#elif RELEASENET45
           
            return SendRequest<T>(httpRequestMsg).Result;
#else
            return SendRequest<T>(httpRequestMsg);
#endif
        }

        /// <summary>
        /// Submits a delete request to the given web address
        /// </summary>
        public HttpResponse<T> DeleteRequest<T>(string requestUri, string authenticationKey)
        {
            var httpRequestMsg = new HttpRequestMessage();

            httpRequestMsg.Method = HttpMethod.Delete;
            httpRequestMsg.RequestUri = new Uri(requestUri);
            httpRequestMsg.Headers.Add("Accept", AppSettings.DefaultContentType);

            SetHttpRequestHeader("Authorization", authenticationKey);

            if (AppSettings.DebugMode)
            {
                Console.WriteLine(string.Format("\n\n** Request ** Delete {0}", requestUri));
            }

#if DEBUGNET45
            return SendRequest<T>(httpRequestMsg).Result;
#elif RELEASENET45
           
            return SendRequest<T>(httpRequestMsg).Result;
#else
            return SendRequest<T>(httpRequestMsg);
#endif
        }

        /// <summary>
        /// Sends a http request with the given object. All headers should be set manually here e.g. content type=application/json
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
#if DEBUGNET45
        private Task<HttpResponse<T>> SendRequest<T>(HttpRequestMessage request)
#elif RELEASENET45
        private Task<HttpResponse<T>> SendRequest<T>(HttpRequestMessage request)
#else
        private HttpResponse<T> SendRequest<T>(HttpRequestMessage request)
#endif
        {
#if DEBUGNET45
            Task<HttpResponse<T>> response = null;
#elif RELEASENET45
            Task<HttpResponse<T>> response = null;
#else
            HttpResponse<T> response = null;
#endif
            HttpResponseMessage responseMessage = null;
            string responseAsString = null;
            string responseCode = null;

            try
            {
#if DEBUGNET45
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
#elif RELEASENET45
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
#endif
                responseMessage = httpClient.SendAsync(request).Result; 
               
                responseCode = responseMessage.StatusCode.ToString();

                var responseContent = responseMessage.Content.ReadAsByteArrayAsync().Result;

                if (responseContent != null && responseContent.Length > 0)
                {
                    responseAsString = Encoding.UTF8.GetString(responseContent);

                    if (AppSettings.DebugMode)
                    {
                        Console.WriteLine(string.Format("\n** HttpResponse - Status {0}**\n {1}\n", responseMessage.StatusCode, responseAsString));
                    }
                }

#if DEBUGNET45
                response = Task.FromResult(CreateHttpResponse<T>(responseAsString, responseMessage.StatusCode));
#elif RELEASENET45
                response = Task.FromResult(CreateHttpResponse<T>(responseAsString, responseMessage.StatusCode));
#else
                response = CreateHttpResponse<T>(responseAsString, responseMessage.StatusCode);
#endif
            }
            catch (Exception ex)
            {
                if (AppSettings.DebugMode)
                {
                    Console.WriteLine(string.Format(@"\n** Exception - HttpStatuscode:\n{0}**\n\n 
                        ** ResponseString {1}\n ** Exception Messages{2}\n ", (responseMessage != null ? responseMessage.StatusCode.ToString() : string.Empty), responseAsString, ExceptionHelper.FlattenExceptionMessages(ex)));
                }

                responseCode = "Exception" + ex.Message;

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
