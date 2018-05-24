namespace Checkout
{
    public class ApiUrls
    {
        private static string _cardTokensApiUri;
        private static string _paymentTokensApiUri;
        private static string _updatePaymentTokensApiUri;
        private static string _cardChargesApiUri;
        private static string _cardTokenChargesApiUri;
        private static string _defaultCardChargesApiUri;
        private static string _chargeRefundsApiUri;
        private static string _chargeVoidApiUri;
        private static string _captureChargesApiUri;
        private static string _updateChargesApiUri;
        private static string _localPaymentChargesApiUri;

        private static string _chargesApiUri;
        private static string _chargeApiUri;
        private static string _chargeHistoryApiUri;

        private static string _customersApiUri;
        private static string _customerApiUri;

        private static string _cardsApiUri;
        private static string _cardApiUri;

        private static string _reportingTransactionsUri;
        private static string _reportingChargebacksUri;

        private static string _binLookupUri;
        private static string _localPaymentIssuerIdLookup;

        private static string _recurringPaymentPlanCreateApiUri;
        private static string _recurringPaymentPlanApiUri;
        private static string _recurringPaymentPlanSearchApiUri;
        private static string _recurringCustomerPaymentPlanSearchApiUri;
        private static string _recurringCustomerPaymentPlanApiUri;
        private static string _visaCheckout;

        public static void ResetApiUrls()
        {
            _cardTokensApiUri = null;
            _paymentTokensApiUri = null;
            _updatePaymentTokensApiUri = null;
            _cardChargesApiUri = null;
            _cardTokenChargesApiUri = null;
            _defaultCardChargesApiUri = null;
            _chargeRefundsApiUri = null;
            _chargeVoidApiUri = null;
            _captureChargesApiUri = null;
            _updateChargesApiUri = null;
            _chargesApiUri = null;
            _chargeApiUri = null;
            _chargeHistoryApiUri = null;
            _customersApiUri = null;
            _customerApiUri = null;
            _cardsApiUri = null;
            _cardApiUri = null;
            _reportingTransactionsUri = null;
            _reportingChargebacksUri = null;
            _binLookupUri = null;
            _visaCheckout = null;
            _localPaymentIssuerIdLookup = null;

            _recurringPaymentPlanApiUri = null;
            _recurringPaymentPlanCreateApiUri = null;
            _recurringPaymentPlanSearchApiUri = null;
            _recurringCustomerPaymentPlanSearchApiUri = null;
            _recurringCustomerPaymentPlanApiUri = null;
            _localPaymentChargesApiUri = null;
        }

        public static string Charges
            => _chargesApiUri ?? (_chargesApiUri = string.Concat(AppSettings.BaseApiUri, "/charges"));

        public static string Charge
            => _chargeApiUri ?? (_chargeApiUri = string.Concat(AppSettings.BaseApiUri, "/charges/{0}"));

        public static string ChargeHistory
            =>
                _chargeHistoryApiUri ??
                (_chargeHistoryApiUri = string.Concat(AppSettings.BaseApiUri, "/charges/{0}/history"));

        public static string CaptureCharge
            =>
                _captureChargesApiUri ??
                (_captureChargesApiUri = string.Concat(AppSettings.BaseApiUri, "/charges/{0}/capture"));

        public static string RefundCharge
            =>
                _chargeRefundsApiUri ??
                (_chargeRefundsApiUri = string.Concat(AppSettings.BaseApiUri, "/charges/{0}/refund"));

        public static string VoidCharge
            => _chargeVoidApiUri ?? (_chargeVoidApiUri = string.Concat(AppSettings.BaseApiUri, "/charges/{0}/void"));

        public static string DefaultCardCharge
            =>
                _defaultCardChargesApiUri ??
                (_defaultCardChargesApiUri = string.Concat(AppSettings.BaseApiUri, "/charges/customer"));

        public static string LocalPaymentCharge
            =>
                _localPaymentChargesApiUri ??
                (_localPaymentChargesApiUri = string.Concat(AppSettings.BaseApiUri, "/charges/localpayment"));

        public static string CardTokenCharge
            =>
                _cardTokenChargesApiUri ??
                (_cardTokenChargesApiUri = string.Concat(AppSettings.BaseApiUri, "/charges/token"));

        public static string CardCharge
            => _cardChargesApiUri ?? (_cardChargesApiUri = string.Concat(AppSettings.BaseApiUri, "/charges/card"));

        public static string CardToken
            => _cardTokensApiUri ?? (_cardTokensApiUri = string.Concat(AppSettings.BaseApiUri, "/tokens/card"));

        public static string PaymentToken
            => _paymentTokensApiUri ?? (_paymentTokensApiUri = string.Concat(AppSettings.BaseApiUri, "/tokens/payment"))
            ;

        public static string UpdatePaymentToken
            =>
                _updatePaymentTokensApiUri ??
                (_paymentTokensApiUri = string.Concat(AppSettings.BaseApiUri, "/tokens/payment/{0}"));

        public static string VisaCheckout
            => _visaCheckout ?? (_visaCheckout = string.Concat(AppSettings.BaseApiUri, "/tokens/card/visa-checkout"));

        public static string Customers
            => _customersApiUri ?? (_customersApiUri = string.Concat(AppSettings.BaseApiUri, "/customers"));

        public static string Customer
            => _customerApiUri ?? (_customerApiUri = string.Concat(AppSettings.BaseApiUri, "/customers/{0}"));

        public static string CustomerByEmail
            => _customerApiUri ?? (_customerApiUri = string.Concat(AppSettings.BaseApiUri, "/customers?email={0}"));

        public static string Cards
            => _cardsApiUri ?? (_cardsApiUri = string.Concat(AppSettings.BaseApiUri, "/customers/{0}/cards"));

        public static string Card
            => _cardApiUri ?? (_cardApiUri = string.Concat(AppSettings.BaseApiUri, "/customers/{0}/cards/{1}"));

        public static string ReportingTransactions
            =>
                _reportingTransactionsUri ??
                (_reportingTransactionsUri = string.Concat(AppSettings.BaseApiUri, "/reporting/transactions"));

        public static string ReportingChargebacks
            =>
                _reportingChargebacksUri ??
                (_reportingChargebacksUri = string.Concat(AppSettings.BaseApiUri, "/reporting/chargebacks"));

        public static string BinLookup
            => _binLookupUri ?? (_binLookupUri = string.Concat(AppSettings.BaseApiUri, "/lookups/bins/{0}"));

        public static string LocalPaymentIssuerIdLookup
            => _localPaymentIssuerIdLookup ?? (_localPaymentIssuerIdLookup = string.Concat(AppSettings.BaseApiUri, "/lookups/localpayments/{0}/tags/issuerid"));

        public static string RecurringPaymentPlans
            =>
                _recurringPaymentPlanCreateApiUri ??
                (_recurringPaymentPlanCreateApiUri = string.Concat(AppSettings.BaseApiUri, "/recurringPayments/plans"));

        public static string RecurringPaymentPlan
            =>
                _recurringPaymentPlanApiUri ??
                (_recurringPaymentPlanApiUri = string.Concat(AppSettings.BaseApiUri, "/recurringPayments/plans/{0}"));

        public static string RecurringPaymentPlanSearch
            =>
                _recurringPaymentPlanSearchApiUri ??
                (_recurringPaymentPlanSearchApiUri =
                    string.Concat(AppSettings.BaseApiUri, "/recurringPayments/plans/search"));

        public static string RecurringCustomerPaymentPlanSearch
            =>
                _recurringCustomerPaymentPlanSearchApiUri ??
                (_recurringCustomerPaymentPlanSearchApiUri =
                    string.Concat(AppSettings.BaseApiUri, "/recurringPayments/customers/search"));

        public static string RecurringCustomerPaymentPlan
            =>
                _recurringCustomerPaymentPlanApiUri ??
                (_recurringCustomerPaymentPlanApiUri =
                    string.Concat(AppSettings.BaseApiUri, "/recurringPayments/customers/{0}"));
    }
}