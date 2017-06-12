using Checkout.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using CheckoutEnvironment = Checkout.Helpers.Environment;
namespace Checkout
{
    /// <summary>
    /// Holds application settings that is read from the app.config or web.config
    /// </summary>
    public sealed class AppSettings
    {
        private static CheckoutEnvironment _environment = CheckoutEnvironment.Undefined;
        private static string _secretKey;
        private static string _publicKey;
        private static string _baseApiUri;
        private static int? _maxResponseContentBufferSize;
        private static int? _requestTimeout;
        private static bool? _debugMode;
        private const string _liveUrl = "https://api2.checkout.com/v2";
        private const string _sandboxUrl = "https://sandbox.checkout.com/api2/v2";
        public const string ClientUserAgentName = "Checkout-DotNetLibraryClient/v1.0";
        public const string DefaultContentType = "application/json";

        public static string BaseApiUri
        {
            get { return _baseApiUri; }
            set { _baseApiUri = value; }
        }
        public static string SecretKey
        {
            get { return _secretKey ?? (_secretKey = ReadConfig("Checkout.SecretKey", true)); }
            set { _secretKey = value; }
        }
        public static string PublicKey
        {
            get { return _publicKey ?? (_publicKey = ReadConfig("Checkout.PublicKey", true)); }
            set { _publicKey = value; }
        }

        public static int RequestTimeout
        {
            get
            {
                if (_requestTimeout == null)
                {
                   var value = ReadConfig("Checkout.RequestTimeout");
                   _requestTimeout = (!string.IsNullOrEmpty(value) ? int.Parse(value) : 60);
                }

                return _requestTimeout.Value;
            }
            set { _requestTimeout = value; }
        }
        public static int MaxResponseContentBufferSize { 
            get { 
                
                 if (_maxResponseContentBufferSize == null)
                {
                   var value = ReadConfig("Checkout.MaxResponseContentBufferSize");
                   _maxResponseContentBufferSize = (!string.IsNullOrEmpty(value) ? int.Parse(value) : 1000000);
                }

                return _maxResponseContentBufferSize.Value; 
            }

            set { _maxResponseContentBufferSize = value; } 
        }
        public static bool DebugMode
        {
            get
            {
                if (_debugMode == null)
                {
                    var value = ReadConfig("Checkout.DebugMode");
                    _debugMode = (!string.IsNullOrEmpty(value) ? bool.Parse(value) : false);
                }

                return _debugMode.Value;
            }
            set { _debugMode = value; }
        }
        public static CheckoutEnvironment Environment
        {
            get
            {
                return _environment;
            }

            set
            {
                switch (value)
                {
                    case CheckoutEnvironment.Live:
                        _baseApiUri = _liveUrl;
                        break;
                    case CheckoutEnvironment.Sandbox:
                        _baseApiUri = _sandboxUrl;
                        break;
                };
                _environment = value;
                ApiUrls.ResetApiUrls();

            }
        }

        public static void SetEnvironmentFromConfig()
        {
            CheckoutEnvironment selectedEnvironment;
            if (Enum.TryParse<CheckoutEnvironment>(ReadConfig("Checkout.Environment", true), out selectedEnvironment) && Enum.IsDefined(typeof(CheckoutEnvironment), selectedEnvironment))
            { Environment = selectedEnvironment; }
            else
            { throw new KeyNotFoundException("Config value is invalid for: Environment"); }
        }

        private static string ReadConfig(string key,bool throwIfnotExist=false)
        {
            try
            {
                return ConfigurationManager.AppSettings[key].ToString();
            }
            catch (Exception)
            {
                if (throwIfnotExist)
                {
                    throw new KeyNotFoundException("App settings Key not found for: " + key);
                }

                return null;
            }
        }
    }
}
