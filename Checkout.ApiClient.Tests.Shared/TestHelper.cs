using Checkout.ApiServices.Cards.RequestModels;
using Checkout.ApiServices.Charges.RequestModels;
using Checkout.ApiServices.Customers.RequestModels;
using Checkout.ApiServices.Payouts.RequestModels;
using Checkout.ApiServices.Reporting.RequestModels;
using Checkout.ApiServices.RecurringPayments.RequestModels;
using Checkout.ApiServices.SharedModels;
using Checkout.ApiServices.Tokens.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using Tests.Utils;
using FilterAction = Checkout.ApiServices.SharedModels.Action;

namespace Tests
{
    public class TestHelper
    {
        private static RandomData _randomData;

        public static RandomData RandomData { get { return _randomData ?? (_randomData = new RandomData()); } }

        #region Recurring Plans Helpers

        public static SinglePaymentPlanCreateRequest GetSinglePaymentPlanCreateModel(int? value = null, string currency = null)
        {
            return new SinglePaymentPlanCreateRequest
            {
                PaymentPlans = new List<PaymentPlanCreate>
                {
                    new PaymentPlanCreate
                    {
                        Name = RandomData.String,
                        AutoCapTime = RandomData.GetNumber(0, 168),
                        Currency = currency ?? "USD",
                        PlanTrackId = RandomData.UniqueString,
                        Value = value ?? 100,
                        RecurringCount = RandomData.GetNumber(1, 19),
                        Cycle = "1d"
                    }
                }
            };
        }

        public static PaymentPlanUpdate GetPaymentPlanUpdateModel(int? value = null, string currency = null)
        {
            return new PaymentPlanUpdate
            {
                Name = RandomData.String,
                AutoCapTime = RandomData.GetNumber(0, 168),
                Currency = currency ?? "GBP",
                PlanTrackId = RandomData.UniqueString,
                Value = value ?? 100,
                RecurringCount = RandomData.GetNumber(1, 19),
                Cycle = "3d",
                Status = RecurringPlanStatus.Suspended
            };
        }

        public static CustomerPaymentPlanCreate GetCustomerPaymentPlanCreateModel(int? value = null, string currency = null)
        {
            return new CustomerPaymentPlanCreate
            {
                Name = RandomData.String,
                AutoCapTime = 0,
                PlanTrackId = RandomData.UniqueString,
                Value = value ?? 100,
                RecurringCount = RandomData.GetNumber(1, 19),
                Cycle = "3d"
            };
        }

        public static CustomerPaymentPlanUpdate GetCustomerPaymentPlanUpdateModel(string cardId, RecurringPlanStatus status)
        {
            return new CustomerPaymentPlanUpdate
            {
                Status = status,
                CardId = cardId
            };
        }

        public static CardCharge GetCardChargeCreateModelWithNewPaymentPlan(string customerEmail = null, string customerId = null)
        {
            return new CardCharge
            {
                CustomerId = customerId,
                Email = customerEmail,
                AutoCapture = "N",
                AutoCapTime = 0,
                Currency = "Usd",
                TrackId = "TRK12345",
                TransactionIndicator = "1",
                CustomerIp = "82.23.168.254",
                Description = RandomData.String,
                Value = "100",
                Card = GetCardCreateModel(),
                Descriptor = new BillingDescriptor { Name = "Amigo ltd.", City = "London" },
                Products = GetProducts(),
                ShippingDetails = GetAddress(),
                Metadata = new Dictionary<string, string>() { { "extraInformation", RandomData.CompanyName } },
                Udf1 = RandomData.String,
                Udf2 = RandomData.String,
                Udf3 = RandomData.String,
                Udf4 = RandomData.String,
                Udf5 = RandomData.String,
                PaymentPlans = new List<CustomerPaymentPlanCreate> { GetCustomerPaymentPlanCreateModel() }
            };
        }

        public static CardCharge GetCardChargeCreateModelWithExistingPaymentPlan(string planId, string startDate = null,
            string customerEmail = null, string customerId = null)
        {
            return new CardCharge
            {
                CustomerId = customerId,
                Email = customerEmail,
                AutoCapture = "N",
                AutoCapTime = 0,
                Currency = "Usd",
                TrackId = "TRK12345",
                TransactionIndicator = "1",
                CustomerIp = "82.23.168.254",
                Description = RandomData.String,
                Value = "100",
                Card = GetCardCreateModel(),
                Descriptor = new BillingDescriptor { Name = "Amigo ltd.", City = "London" },
                Products = GetProducts(),
                ShippingDetails = GetAddress(),
                Metadata = new Dictionary<string, string>() { { "extraInformation", RandomData.CompanyName } },
                Udf1 = RandomData.String,
                Udf2 = RandomData.String,
                Udf3 = RandomData.String,
                Udf4 = RandomData.String,
                Udf5 = RandomData.String,
                PaymentPlans = new List<CustomerPaymentPlanCreate> { new CustomerPaymentPlan { PlanId = planId, StartDate = startDate } }
            };
        }

        public static QueryPaymentPlanRequest GetCustomQueryPaymentPlanRequest(string propertyName, object value, string currency = null)
        {
            var queryRequest = new QueryPaymentPlanRequest();
            ReflectionHelper.SetPropertyValue(queryRequest, propertyName, value);

            // set currency in request when querying by value
            if (propertyName == "Value")
            {
                queryRequest.Currency = currency;
            }

            return queryRequest;
        }

        public static QueryCustomerPaymentPlanRequest GetCustomQueryCustomerPaymentPlanRequest(string propertyName, object value, string currency = null)
        {
            var queryRequest = new QueryCustomerPaymentPlanRequest();
            ReflectionHelper.SetPropertyValue(queryRequest, propertyName, value);

            // set currency in request when querying by value
            if (propertyName == "Value")
            {
                queryRequest.Currency = currency;
            }

            return queryRequest;
        }

        public static object GetRecurringPlanPropertyValue(object source, string propertyName)
        {
            var propertyValue = ReflectionHelper.GetPropertyValue(source, propertyName);

            // converts date into YYYY-MM-DD format
            if (propertyName.IndexOf("Date", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                propertyValue = Convert.ToDateTime(propertyValue).ToString("yyyy-MM-dd");
            }

            return propertyValue;
        }

        #endregion

        #region Token Helpers

        public static PaymentTokenCreate GetPaymentTokenCreateModel(string email, int chargeMode = 1, string currency = "usd")
        {
            return new PaymentTokenCreate()
            {
                Currency = currency,
                Value = "100",
                AutoCapTime = 1,
                AutoCapture = "N",
                ChargeMode = chargeMode,
                AttemptN3D = true,
                Email = email,
                CustomerIp = "82.23.168.254",
                TrackId = "TRK12345",
                Description = RandomData.String,
                Products = GetProducts(),
                ShippingDetails = GetAddress(),
                Metadata = new Dictionary<string, string>() { { "extraInformation", RandomData.CompanyName } },
                Udf1 = RandomData.String,
                Udf2 = RandomData.String,
                Udf3 = RandomData.String,
                Udf4 = RandomData.String,
                Udf5 = RandomData.String,
            };
        }

        public static PaymentTokenCreate GetPaymentTokenUpdateModel(string email, int chargeMode = 1, string currency = "usd")
        {
            return new PaymentTokenCreate()
            {
                TrackId = "TRK12345",
                Metadata = new Dictionary<string, string>() { { "extraInformation", RandomData.CompanyName } },
                Udf1 = RandomData.String,
                Udf2 = RandomData.String,
                Udf3 = RandomData.String,
                Udf4 = RandomData.String,
                Udf5 = RandomData.String,
            };
        }

        #endregion

        #region Card Helpers
        public static Address GetAddressModel()
        {
            return new Address()
            {
                AddressLine1 = RandomData.StreetAddress,
                AddressLine2 = RandomData.String,
                City = RandomData.City,
                Country = RandomData.CountryISO2,
                Phone = GetPhone(),
                Postcode = RandomData.PostCode,
                State = RandomData.City
            };
        }

        public static TokenCard GetTokenCardModel()
        {
            return new TokenCard()
            {
                ExpiryMonth = "06",
                ExpiryYear = "2022",
                Number = "4242424242424242",
                Cvv = "100"
            };
        }

        public static BaseCard GetBaseCardModel(CardProvider cardProvider = CardProvider.Visa)
        {
            if (cardProvider == CardProvider.Visa)
            {
                return new BaseCard()
                {
                    ExpiryMonth = "06",
                    ExpiryYear = "2022",
                    Name = RandomData.FullName,
                    BillingDetails = GetAddressModel()
                };
            }
            else
            {
                return new BaseCard()
                {
                    ExpiryYear = "2025",
                    ExpiryMonth = "06",
                    Name = RandomData.FullName,
                    BillingDetails = GetAddressModel()
                };
            }
        }

        public static CardCreate GetCardCreateModel(CardProvider cardProvider = CardProvider.Visa)
        {
            CardCreate card = new CardCreate();
            var baseCard = GetBaseCardModel(cardProvider);

            card.Name = baseCard.Name;
            card.ExpiryMonth = baseCard.ExpiryMonth;
            card.ExpiryYear = baseCard.ExpiryYear;
            card.BillingDetails = baseCard.BillingDetails;

            if (cardProvider == CardProvider.Visa)
            {
                card.Cvv = "100";
                card.Number = "4242424242424242";
            }
            else
            {
                card.Cvv = "257";
                card.Number = "5436031030606378";
            }

            return card;
        }

        public static CardUpdate GetCardUpdateModel(bool setDefaultCard = false)
        {
            CardUpdate card = new CardUpdate();
            var baseCard = GetBaseCardModel();

            card.Name = baseCard.Name;
            card.ExpiryMonth = baseCard.ExpiryMonth;
            card.ExpiryYear = baseCard.ExpiryYear;
            card.BillingDetails = baseCard.BillingDetails;
            card.DefaultCard = setDefaultCard;

            return card;
        }

        #endregion

        #region Customer Helpers

        public static CustomerCreate GetCustomerCreateModelWithCard(CardProvider cardProvider = CardProvider.Visa)
        {
            return new CustomerCreate()
            {
                Name = RandomData.FullName,
                Description = RandomData.String,
                Email = RandomData.Email,
                Phone = GetPhone(),
                Metadata = new Dictionary<string, string>() { { "channelInfo", RandomData.CompanyName } },
                Card = GetCardCreateModel(cardProvider)
            };
        }
        public static CustomerCreate GetCustomerCreateModelWithNoCard()
        {
            var customerCreateModel = GetCustomerCreateModelWithCard();
            customerCreateModel.Card = null;
            return customerCreateModel;
        }
        public static CustomerUpdate GetCustomerUpdateModel()
        {
            return new CustomerUpdate()
            {
                Name = RandomData.FullName,
                Description = RandomData.String,
                Email = RandomData.Email,
                Phone = GetPhone(),
                Metadata = new Dictionary<string, string>() { { "channelInfo", RandomData.CompanyName } }
            };
        }

        private static Phone GetPhone()
        {
            return new Phone()
            {
                CountryCode = "1",
                Number = "999 999 9999"
            };
        }

        public static DefaultCardCharge GetCustomerDefaultCardChargeCreateModel(string customerId)
        {
            DefaultCardCharge defaultCardCharge = new DefaultCardCharge
            {
                CustomerId = customerId,
                AutoCapture = "Y",
                AutoCapTime = 10,
                Currency = "Usd",
                TrackId = "TRK12345",
                TransactionIndicator = "1",
                CustomerIp = "82.23.168.254",
                Description = RandomData.String,
                Value = "100",
                Descriptor = new BillingDescriptor { Name = "Amigo ltd.", City = "London" },
                Products = GetProducts(),
                ShippingDetails = GetAddress(),
                Metadata = new Dictionary<string, string>() { { "extraInformation", RandomData.CompanyName } },
                Udf1 = RandomData.String,
                Udf2 = RandomData.String,
                Udf3 = RandomData.String,
                Udf4 = RandomData.String,
                Udf5 = RandomData.String
            };

            return defaultCardCharge;
        }
        #endregion

        #region Charge Helpers
        public static CardCharge GetCardChargeCreateModel(string customerEmail = null, string customerId = null)
        {
            return new CardCharge()
            {
                CustomerId = customerId,
                Email = customerEmail,
                AutoCapture = "N",
                AutoCapTime = 0,
                Currency = "Usd",
                TrackId = "TRK12345",
                TransactionIndicator = "1",
                CustomerIp = "82.23.168.254",
                Description = RandomData.String,
                Value = "100",
                Card = GetCardCreateModel(),
                Descriptor = new BillingDescriptor { Name = "Amigo ltd.", City = "London" },
                Products = GetProducts(),
                ShippingDetails = GetAddress(),
                Metadata = new Dictionary<string, string>() { { "extraInformation", RandomData.CompanyName } },
                Udf1 = RandomData.String,
                Udf2 = RandomData.String,
                Udf3 = RandomData.String,
                Udf4 = RandomData.String,
                Udf5 = RandomData.String,
                RiskCheck = true
            };
        }

        public static Address GetAddress()
        {

            return new Address
            {
                AddressLine1 = RandomData.StreetAddress,
                AddressLine2 = RandomData.String,
                City = RandomData.City,
                Country = RandomData.CountryISO2,
                Phone = GetPhone(),
                Postcode = RandomData.PostCode,
                State = RandomData.City
            };
        }

        public static List<Product> GetProducts(int numOfProducts = 1)
        {
            var productList = new List<Product>();
            Product product;

            for (int i = 0; i <= numOfProducts; i++)
            {
                product = new Product
                {
                    Description = RandomData.String,
                    Image = "http://www.imageurl.com/me.png",
                    Name = "test product" + i,
                    Price = RandomData.GetNumber(50, 500),
                    Quantity = RandomData.GetNumber(1, 100),
                    ShippingCost = RandomData.GetDecimalNumber(),
                    Sku = RandomData.UniqueString,
                    TrackingUrl = "http://www.track.com?ref=12345"
                };
                productList.Add(product);
            }

            return productList;
        }

        public static CardIdCharge GetCardIdChargeCreateModel(string cardId, string customerEmail = null, string customerId = null)
        {
            return new CardIdCharge()
            {
                CardId = cardId,
                Cvv = "100",
                CustomerId = customerId,
                Email = customerEmail,
                AutoCapture = "Y",
                AutoCapTime = 10,
                Currency = "Usd",
                Description = RandomData.String,
                Value = "100",
                Products = GetProducts(),
                ShippingDetails = GetAddress(),
                Metadata = new Dictionary<string, string>() { { "extraInformation", RandomData.CompanyName } },
                Descriptor = new BillingDescriptor { Name = "Amigo ltd.", City = "London" }
            };
        }

        public static CardTokenCharge GetCardTokenChargeCreateModel(string cardToken, string customerEmail = null, string customerId = null)
        {
            return new CardTokenCharge()
            {
                CardToken = cardToken,
                CustomerId = customerId,
                Email = customerEmail ?? RandomData.Email,
                AutoCapture = "Y",
                AutoCapTime = 10,
                Currency = "Usd",
                Description = RandomData.String,
                Value = "100",
                Descriptor = new BillingDescriptor { Name = "Amigo ltd.", City = "London" }
            };
        }

        public static BaseCharge GetBaseChargeModel(string customerEmail = null, string customerId = null)
        {
            return new BaseCharge()
            {
                CustomerId = customerId,
                Email = customerEmail,
                AutoCapture = "Y",
                AutoCapTime = 10,
                Currency = "Usd",
                Description = RandomData.String,
                Value = "100"
            };
        }

        public static ChargeRefund GetChargeRefundModel(string amount = null)
        {
            return new ChargeRefund()
            {
                Value = amount,
                TrackId = "TRK12345",
                Description = RandomData.String,
                Products = GetProducts(),
                Metadata = new Dictionary<string, string>() { { "extraInformation", RandomData.CompanyName } },
                Udf1 = RandomData.String,
                Udf2 = RandomData.String,
                Udf3 = RandomData.String,
                Udf4 = RandomData.String,
                Udf5 = RandomData.String
            };
        }

        public static ChargeUpdate GetChargeUpdateModel()
        {
            return new ChargeUpdate()
            {
                TrackId = "TRK12345",
                Description = RandomData.String,
                Metadata = new Dictionary<string, string>() { { "extraInformation", RandomData.CompanyName }, { "extraInformation2", RandomData.String } },
                Udf1 = RandomData.String,
                Udf2 = RandomData.String,
                Udf3 = RandomData.String,
                Udf4 = RandomData.String,
                Udf5 = RandomData.String
            };
        }

        public static PaymentTokenUpdate GetPaymentTokenUpdateModel()
        {
            return new PaymentTokenUpdate()
            {
                TrackId = "TRK12345",
                Description = RandomData.String,
                Metadata = new Dictionary<string, string>() { { "extraInformation", RandomData.CompanyName }, { "extraInformation2", RandomData.String } },
                Udf1 = RandomData.String,
                Udf2 = RandomData.String,
                Udf3 = RandomData.String,
                Udf4 = RandomData.String,
                Udf5 = RandomData.String
            };
        }

        public static ChargeCapture GetChargeCaptureModel(string amount = null)
        {
            return new ChargeCapture()
            {
                Value = amount,
                TrackId = "TRK12345",
                Description = RandomData.String,
                Products = GetProducts(),
                Metadata = new Dictionary<string, string>() { { "extraInformation", RandomData.CompanyName } },
                Udf1 = RandomData.String,
                Udf2 = RandomData.String,
                Udf3 = RandomData.String,
                Udf4 = RandomData.String,
                Udf5 = RandomData.String
            };
        }

        public static ChargeVoid GetChargeVoidModel()
        {
            return new ChargeVoid()
            {
                TrackId = "TRK12345",
                Description = RandomData.String,
                Products = GetProducts(),
                Metadata = new Dictionary<string, string>() { { "extraInformation", RandomData.CompanyName } },
                Udf1 = RandomData.String,
                Udf2 = RandomData.String,
                Udf3 = RandomData.String,
                Udf4 = RandomData.String,
                Udf5 = RandomData.String
            };
        }

        public static LocalPaymentCharge GetLocalPaymentChargeModel(string paymentToken, string lppId = "lpp_9", Dictionary<string, string> userData = null)
        {
            var localPayment = new LocalPaymentCharge
            {
                Email = RandomData.Email,
                LocalPayment = new LocalPaymentCreate
                {
                    LppId = lppId,
                    UserData = userData ?? new Dictionary<string, string> { { "issuerId", "INGBNL2A" } }
                },
                PaymentToken = paymentToken
            };
            return localPayment;
        }

        #endregion

        #region Reporting Helpers

        /// <summary>
        /// Creates a model for a dynamic reporting query
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="sortColumn"></param>
        /// <param name="sortOrder"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static QueryRequest GetQueryRequest(string searchValue = null, DateTime? fromDate = null, DateTime? toDate = null,
            SortColumn? sortColumn = null, SortOrder? sortOrder = null, int? pageSize = null, string pageNumber = null, List<Filter> filters = null)
        {
            return new QueryRequest()
            {
                FromDate = fromDate,
                ToDate = toDate,
                PageSize = pageSize,
                PageNumber = pageNumber,
                SortColumn = sortColumn,
                SortOrder = sortOrder,
                Search = searchValue,
                Filters = filters
            };
        }

        /// <summary>
        /// Creates a model for the transactions dynamic query using filters
        /// </summary>
        /// <param name="filters"></param>
        /// <returns></returns>
        public static QueryRequest GetQueryRequest(List<Filter> filters)
        {
            return GetQueryRequest(null, null, null, null, null, null, null, filters);
        }

        /// <summary>
        /// Creates an empty model for the transactions dynamic query
        /// </summary>
        /// <returns></returns>
        public static QueryRequest GetQueryRequest()
        {
            return GetQueryRequest(null);
        }

        #endregion

        #region Payouts Helpers

        /// <summary>
        ///     Creates a model for a BasePayout Request
        /// </summary>
        /// <param name="cardId">The destination of the Payout</param>
        /// <param name="firstName">The customer's first name</param>
        /// <param name="lastName">The customer's last name</param>
        /// <returns>
        ///     An instance of BasePayout with value USD 200.00
        /// </returns>
        public static BasePayout GetPayoutModel(string cardId, string cardholderFirstName, string cardholderLastName)
        {
            BasePayout basePayout = new BasePayout
            {
                Destination = cardId,
                Value = 200,
                Currency = "EUR",
                FirstName = cardholderFirstName,
                LastName = cardholderLastName
            };

            return basePayout;
        }
        #endregion


        /// <summary>
        /// Masks a card number to query transactions
        /// eg: 4242424242424242 -> 424242******4242
        /// </summary>
        /// <param name="cardNumber"></param>
        /// <returns></returns>
        public static string MaskCardNumber(string cardNumber)
        {
            return cardNumber.Replace(6, 6, '*');
        }
    }
}