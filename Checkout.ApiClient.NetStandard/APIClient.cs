using Checkout.ApiServices.Cards;
using Checkout.ApiServices.Charges;
using Checkout.ApiServices.Customers;
using Checkout.ApiServices.Lookups;
using Checkout.ApiServices.Payouts;
using Checkout.ApiServices.Reporting;
using Checkout.ApiServices.RecurringPayments;
using Checkout.ApiServices.Tokens;
using Checkout.Helpers;

namespace Checkout
{
    public sealed class ApiClient : IApiClient
    {
        public CheckoutConfiguration CheckoutConfiguration { get; private set; }
        public IApiHttpClient ApiHttpClient { get; private set; }

        public ICardService CardService { get; private set; }
        public IChargeService ChargeService { get; private set; }
        public ICustomerService CustomerService { get; private set; }
        public ILookupsService LookupsService { get; private set; }
        public IPayoutsService PayoutsService { get; private set; }
        public IRecurringPaymentsService RecurringPaymentsService { get; private set; }
        public IReportingService ReportingService { get; private set; }
        public ITokenService TokenService { get; private set; }

        public ApiClient(CheckoutConfiguration configuration)
        {
            CheckoutConfiguration = configuration;
            ApiHttpClient = new ApiHttpClient(CheckoutConfiguration);

            CardService = new CardService(ApiHttpClient, CheckoutConfiguration);
            ChargeService = new ChargeService(ApiHttpClient, CheckoutConfiguration);
            CustomerService = new CustomerService(ApiHttpClient, CheckoutConfiguration);
            LookupsService = new LookupsService(ApiHttpClient, CheckoutConfiguration);
            PayoutsService = new PayoutsService(ApiHttpClient, CheckoutConfiguration);
            RecurringPaymentsService = new RecurringPaymentsService(ApiHttpClient, CheckoutConfiguration);
            ReportingService = new ReportingService(ApiHttpClient, CheckoutConfiguration);
            TokenService = new TokenService(ApiHttpClient, CheckoutConfiguration);

            ContentAdaptor.Setup();
        }

        public ApiClient(CheckoutConfiguration configuration, IApiHttpClient httpClient)
        {
            CheckoutConfiguration = configuration;
            ApiHttpClient = httpClient;

            CardService = new CardService(ApiHttpClient, CheckoutConfiguration);
            ChargeService = new ChargeService(ApiHttpClient, CheckoutConfiguration);
            CustomerService = new CustomerService(ApiHttpClient, CheckoutConfiguration);
            LookupsService = new LookupsService(ApiHttpClient, CheckoutConfiguration);
            PayoutsService = new PayoutsService(ApiHttpClient, CheckoutConfiguration);
            RecurringPaymentsService = new RecurringPaymentsService(ApiHttpClient, CheckoutConfiguration);
            ReportingService = new ReportingService(ApiHttpClient, CheckoutConfiguration);
            TokenService = new TokenService(ApiHttpClient, CheckoutConfiguration);

            ContentAdaptor.Setup();
        }
    }
}
