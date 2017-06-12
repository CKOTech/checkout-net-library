using Checkout.ApiServices.Cards;
using Checkout.ApiServices.Charges;
using Checkout.ApiServices.Customers;
using Checkout.ApiServices.Lookups;
using Checkout.ApiServices.Reporting;
using Checkout.ApiServices.RecurringPayments;
using Checkout.ApiServices.Tokens;
using Checkout.Helpers;

namespace Checkout
{
    public sealed class APIClient
    {
        private TokenService _tokenService;
        private CustomerService _customerService;
        private CardService _cardService;
        private ChargeService _chargeService;
        private ReportingService _reportingService;
        private LookupsService _lookupsService;
        private RecurringPaymentsService _recurringPaymentsService;

        public ChargeService ChargeService { get { return _chargeService ?? (_chargeService = new ChargeService()); } }
        public CardService CardService { get { return _cardService ?? (_cardService = new CardService()); } }
        public CustomerService CustomerService { get { return _customerService ?? (_customerService = new CustomerService()); } }
        public TokenService TokenService { get { return _tokenService ?? (_tokenService = new TokenService()); } }
        public ReportingService ReportingService { get { return _reportingService ?? (_reportingService = new ReportingService()); } }
        public LookupsService LookupsService { get { return _lookupsService ?? (_lookupsService = new LookupsService()); } }
        public RecurringPaymentsService RecurringPaymentsService { get { return _recurringPaymentsService ?? (_recurringPaymentsService = new RecurringPaymentsService()); } }

        public APIClient()
        {
            if (AppSettings.Environment == Environment.Undefined)
            {
                AppSettings.SetEnvironmentFromConfig();
            }

            ContentAdaptor.Setup();
        }

        public APIClient(string secretKey, Environment env, bool debugMode, int connectTimeout)
            : this(secretKey, env, debugMode)
        {
            AppSettings.RequestTimeout = connectTimeout;
        }

        public APIClient(string secretKey, Environment env, bool debugMode)
            : this(secretKey, env)
        {
            AppSettings.DebugMode = debugMode;
        }

        public APIClient(string secretKey, Environment env)
        {
            AppSettings.SecretKey = secretKey;
            AppSettings.Environment = env;
            ContentAdaptor.Setup();
        }

        public APIClient(string secretKey, bool debugMode)
            : this(secretKey)
        {
            AppSettings.DebugMode = debugMode;
        }


        public APIClient(string secretKey):this()
        {
            AppSettings.SecretKey = secretKey;
        }
    }
}
