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
    public sealed class ApiClientAsync : IApiClientAsync
    {
        public CheckoutConfiguration CheckoutConfiguration { get; private set; }
        public IApiHttpClient ApiHttpClient { get; private set; }

        public ICardServiceAsync CardServiceAsync { get; private set; }
        public IChargeServiceAsync ChargeServiceAsync { get; private set; }
        public ICustomerServiceAsync CustomerServiceAsync { get; private set; }
        public ILookupsServiceAsync LookupsServiceAsync { get; private set; }
        public IPayoutsServiceAsync PayoutsServiceAsync { get; private set; }
        public IRecurringPaymentsServiceAsync RecurringPaymentsServiceAsync { get; private set; }
        public IReportingServiceAsync ReportingServiceAsync { get; private set; }
        public ITokenServiceAsync TokenServiceAsync { get; private set; }

        public ApiClientAsync(CheckoutConfiguration configuration)
        {
            CheckoutConfiguration = configuration;
            ApiHttpClient = new ApiHttpClient(CheckoutConfiguration);

            CardServiceAsync = new CardServiceAsync(ApiHttpClient, CheckoutConfiguration);
            ChargeServiceAsync = new ChargeServiceAsync(ApiHttpClient, CheckoutConfiguration);
            CustomerServiceAsync = new CustomerServiceAsync(ApiHttpClient, CheckoutConfiguration);
            LookupsServiceAsync = new LookupsServiceAsync(ApiHttpClient, CheckoutConfiguration);
            PayoutsServiceAsync = new PayoutsServiceAsync(ApiHttpClient, CheckoutConfiguration);
            RecurringPaymentsServiceAsync = new RecurringPaymentsServiceAsync(ApiHttpClient, CheckoutConfiguration);
            ReportingServiceAsync = new ReportingServiceAsync(ApiHttpClient, CheckoutConfiguration);
            TokenServiceAsync = new TokenServiceAsync(ApiHttpClient, CheckoutConfiguration);

            ContentAdaptor.Setup();
        }

        public ApiClientAsync(CheckoutConfiguration configuration, IApiHttpClient httpClient)
        {
            CheckoutConfiguration = configuration;
            ApiHttpClient = httpClient;

            CardServiceAsync = new CardServiceAsync(ApiHttpClient, CheckoutConfiguration);
            ChargeServiceAsync = new ChargeServiceAsync(ApiHttpClient, CheckoutConfiguration);
            CustomerServiceAsync = new CustomerServiceAsync(ApiHttpClient, CheckoutConfiguration);
            LookupsServiceAsync = new LookupsServiceAsync(ApiHttpClient, CheckoutConfiguration);
            PayoutsServiceAsync = new PayoutsServiceAsync(ApiHttpClient, CheckoutConfiguration);
            RecurringPaymentsServiceAsync = new RecurringPaymentsServiceAsync(ApiHttpClient, CheckoutConfiguration);
            ReportingServiceAsync = new ReportingServiceAsync(ApiHttpClient, CheckoutConfiguration);
            TokenServiceAsync = new TokenServiceAsync(ApiHttpClient, CheckoutConfiguration);

            ContentAdaptor.Setup();
        }
    }
}
