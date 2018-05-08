using Checkout.Utilities;
using System;
using System.Collections.Generic;
using CheckoutEnvironment = Checkout.Helpers.Environment;

namespace Checkout
{
    public class AppSettings
    {
        private const string _liveUrl = "https://api2.checkout.com/v2";
        private const string _sandboxUrl = "https://sandbox.checkout.com/api2/v2";
        public const string ClientUserAgentName = "Checkout-DotNetLibraryClient/v2.0.0";
        public const string DefaultContentType = "application/json";
    
    
        public ApiUrls ApiUrls { get; private set; }
        public string SecretKey { get; set; }
        public string PublicKey { get; set; }
        public string BaseApiUri { get; private set; } = _sandboxUrl;
        public int MaxResponseContentBufferSize { get; set; } = 10240;
        public int RequestTimeout { get; set; } = 60;
        public bool DebugMode { get; set; } = false;

        private CheckoutEnvironment _environment = CheckoutEnvironment.Sandbox;
        public CheckoutEnvironment Environment
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
                        BaseApiUri = _liveUrl;
                        break;
                    case CheckoutEnvironment.Sandbox:
                        BaseApiUri = _sandboxUrl;
                        break;
                };
                _environment = value;
                ApiUrls = new ApiUrls(this);
            }
        }

        public AppSettings()
        {
            ApiUrls = new ApiUrls(this);
        }
    }
}