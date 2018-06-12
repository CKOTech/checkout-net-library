using Checkout.ApiServices.Cards;
using Checkout.ApiServices.Charges;
using Checkout.ApiServices.Customers;
using Checkout.ApiServices.Lookups;
using Checkout.ApiServices.Payouts;
using Checkout.ApiServices.RecurringPayments;
using Checkout.ApiServices.Reporting;
using Checkout.ApiServices.Tokens;

namespace Checkout
{
    public interface IApiClientAsync
    {
        IApiHttpClient ApiHttpClient { get; }

        ICardServiceAsync CardServiceAsync { get; }
        IChargeServiceAsync ChargeServiceAsync { get; }
        ICustomerServiceAsync CustomerServiceAsync { get; }
        ILookupsServiceAsync LookupsServiceAsync { get; }
        IPayoutsServiceAsync PayoutsServiceAsync { get; }
        IRecurringPaymentsServiceAsync RecurringPaymentsServiceAsync { get; }
        IReportingServiceAsync ReportingServiceAsync { get; }
        ITokenServiceAsync TokenServiceAsync { get; }
    }
}