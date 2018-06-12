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
    public interface IApiClient
    {
        IApiHttpClient ApiHttpClient { get; }

        ICardService CardService { get; }
        IChargeService ChargeService { get; }
        ICustomerService CustomerService { get; }
        ILookupsService LookupsService { get; }
        IPayoutsService PayoutsService { get; }
        IRecurringPaymentsService RecurringPaymentsService { get; }
        IReportingService ReportingService { get; }
        ITokenService TokenService { get; }
    }
}