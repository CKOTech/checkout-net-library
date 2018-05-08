namespace Checkout
{
    public class ApiUrls
    {
        private AppSettings _appSettings;

        public ApiUrls(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        private string HasUrl(string endpoint)
        {
            return string.Concat(_appSettings.BaseApiUri, endpoint);
        }

        public string Charges => HasUrl("/charges");
        public string Charge => HasUrl("/charges/{0}");
        public string ChargeHistory => HasUrl("/charges/{0}/history");
        public string CaptureCharge => HasUrl("/charges/{0}/capture");
        public string RefundCharge => HasUrl("/charges/{0}/refund");
        public string VoidCharge => HasUrl("/charges/{0}/void");
        public string DefaultCardCharge => HasUrl("/charges/customer");
        public string LocalPaymentCharge => HasUrl("/charges/localpayment");
        public string CardTokenCharge => HasUrl("/charges/token");
        public string CardCharge => HasUrl("/charges/card");
        public string CardToken => HasUrl("/tokens/card");
        public string PaymentToken => HasUrl("/tokens/payment");
        public string UpdatePaymentToken => HasUrl("/tokens/payment/{0}");
        public string VisaCheckout => HasUrl("/tokens/card/visa-checkout");
        public string Customers => HasUrl("/customers");
        public string Customer => HasUrl("/customers/{0}");
        public string CustomerViaEmail => HasUrl("/customers?email={0}");
        public string Cards => HasUrl("/customers/{0}/cards");
        public string Card => HasUrl("/customers/{0}/cards/{1}");
        public string ReportingTransactions => HasUrl("/reporting/transactions");
        public string ReportingChargebacks => HasUrl("/reporting/chargebacks");
        public string BinLookup => HasUrl("/lookups/bins/{0}");
        public string LocalPaymentIssuerIdLookup => HasUrl("/lookups/localpayments/{0}/tags/issuerid");
        public string RecurringPaymentPlans => HasUrl("/recurringPayments/plans");
        public string RecurringPaymentPlan => HasUrl("/recurringPayments/plans/{0}");
        public string RecurringPaymentPlanSearch => HasUrl("/recurringPayments/plans/search");
        public string RecurringCustomerPaymentPlanSearch => HasUrl("/recurringPayments/customers/search");
        public string RecurringCustomerPaymentPlan => HasUrl("/recurringPayments/customers/{0}");
        public string Payouts => HasUrl("/payouts");
    }
}