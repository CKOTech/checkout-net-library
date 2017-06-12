using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Checkout.Utilities
{
    public class UrlHelper
    {
        /////// <summary>
        /////// Appends the given sub path to the domain uri
        /////// </summary>
        /////// <param name="apiSubPath">subpath e.g. /customers</param>
        /////// <returns>The full api url e.g. http://dev.checkout.com/api.gw3/v1/customers </returns>
        //public static String BuildApiUrl(String apiSubPath)
        //{

        //    if (apiSubPath == null)
        //    {
        //        throw new ArgumentNullException("Api Path is invalid");
        //    }

        //    return String.Format("{0}{1}", baseUri, apiSubPath);
        //}

        public static string AddParameterToUrl(string url, string parameterName, string parameterValue)
        {
            return string.Format("{0}{1}{2}={3}", url, (url.Contains("?") ? "&" : "?"), parameterName, HttpUtility.UrlEncode(parameterValue));
        }

        public static string AddParameterToUrl(string url, Dictionary<string,string> parameterList)
        {
            var formattedUri = string.Empty;

            foreach (var parameter in parameterList)
            {
                formattedUri = AddParameterToUrl(url, parameter.Key, parameter.Value);
            }

            return formattedUri;
        }
    }
}
