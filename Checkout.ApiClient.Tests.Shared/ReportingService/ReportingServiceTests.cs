using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Checkout.ApiServices.Charges.ResponseModels;
using Checkout.ApiServices.Charges.RequestModels;
using Checkout.ApiServices.Reporting.ResponseModels;
using Checkout.ApiServices.Tokens.RequestModels;
using Checkout.ApiServices.Tokens.ResponseModels;
using Checkout.ApiServices.SharedModels;
using FluentAssertions;
using NUnit.Framework;
using FilterAction = Checkout.ApiServices.SharedModels.Action;

namespace Tests
{
    [TestFixture(Category = "ReportingApi")]
    public class ReportingServiceTests : BaseServiceTests
    {
        private readonly StringComparison _ignoreCase = StringComparison.OrdinalIgnoreCase;

        [TestCase(null, HttpStatusCode.OK, false)]
        [TestCase(9, HttpStatusCode.BadRequest, true)]
        [TestCase(15, HttpStatusCode.OK, false)]
        [TestCase(251, HttpStatusCode.BadRequest, true)]
        public void QueryTransactions_PageSizeShouldBeWithinLimits(int? pageSize, HttpStatusCode responseStatus,
            bool hasError)
        {
            var request = TestHelper.GetQueryRequest(string.Empty, null, null, null, null, pageSize);
            var response = CheckoutClient.ReportingService.QueryTransaction(request);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(responseStatus);
            response.HasError.Should().Be(hasError);
        }

        [TestCase(null)]
        [TestCase(SortColumn.Amount)]
        [TestCase(SortColumn.BusinessName)]
        [TestCase(SortColumn.ChannelName)]
        [TestCase(SortColumn.Currency)]
        [TestCase(SortColumn.Date)]
        [TestCase(SortColumn.Email)]
        [TestCase(SortColumn.Id)]
        //[TestCase(SortColumn.LiveMode)]
        [TestCase(SortColumn.Name)]
        [TestCase(SortColumn.OriginId)]
        [TestCase(SortColumn.ResponseCode)]
        [TestCase(SortColumn.Scheme)]
        [TestCase(SortColumn.Status)]
        [TestCase(SortColumn.TrackId)]
        [TestCase(SortColumn.Type)]
        public void QueryTransactions_ShouldAllowColumnSortingBy(SortColumn? sortColumn)
        {
            var request = TestHelper.GetQueryRequest(string.Empty, null, null, sortColumn);
            var response = CheckoutClient.ReportingService.QueryTransaction(request);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);

            if (!sortColumn.HasValue || sortColumn == SortColumn.Date)
            {
                response.Model.Data.Should().BeInDescendingOrder(x => x.Date);
            }
            else if (sortColumn == SortColumn.Name || sortColumn == SortColumn.Email)
            {
                var expression = ReflectionHelper.CreateExpression<Transaction>("Customer." + sortColumn);
                response.Model.Data.Should().BeInAscendingOrder(expression);
            }
            else
            {
                var expression = ReflectionHelper.CreateExpression<Transaction>(sortColumn);
                response.Model.Data.Should().BeInAscendingOrder(expression);
            }
        }

        [TestCase(null)]
        [TestCase(SortOrder.Asc)]
        [TestCase(SortOrder.Desc)]
        public void QueryTransactions_ShouldAllowSortingOrder(SortOrder? sortOrder)
        {
            var request = TestHelper.GetQueryRequest(string.Empty, null, null, SortColumn.Date, sortOrder);
            var response = CheckoutClient.ReportingService.QueryTransaction(request);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);

            if (request.SortOrder == SortOrder.Asc)
            {
                response.Model.Data.Should().BeInAscendingOrder(x => x.Date);
            }
            else
            {
                response.Model.Data.Should().BeInDescendingOrder(x => x.Date);
            }
        }

        [TestCase(null)]
        [TestCase("asd")]
        [TestCase("-1")]
        [TestCase("2")]
        [TestCase("9999")]
        public void QueryTransactions_ShouldAllowPagination(string pageNumber)
        {
            var request = TestHelper.GetQueryRequest(string.Empty, null, null, null, null, null, pageNumber);
            var response = CheckoutClient.ReportingService.QueryTransaction(request);

            response.Should().NotBeNull();

            if (string.IsNullOrEmpty(request.PageNumber))
            {
                // default page number
                response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
                response.Model.PageNumber.Should().Be(1);
            }
            else
            {
                if (int.TryParse(request.PageNumber, out int value) && value > 0)
                {
                    if (response.Model.PageNumber == 9999)
                    {
                        // Result set empty if greater than the number of pages. 
                        response.Model.TotalRecords.Should().Be(0);
                    }

                    response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
                    response.Model.PageNumber.ShouldBeEquivalentTo(request.PageNumber);
                }
                else
                {
                    // invalid number
                    response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
                    response.HasError.Should().BeTrue();
                }
            }
        }

        [TestCase("test")]
        [TestCase("captured")]
        [TestCase("TRK12345")]
        public void QueryTransactions_ShouldAllowFilteringBySearchString(string searchValue)
        {
            var request = TestHelper.GetQueryRequest(searchValue);
            var response = CheckoutClient.ReportingService.QueryTransaction(request);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.TotalRecords.Should().BeGreaterThan(0);

            // the following fields will be checked if they contain the input search value:
            var assertions = new List<bool>
            {
                response.Model.Data.Any(d => d.Id.Contains(request.Search, _ignoreCase)),
                response.Model.Data.Any(d => d.OriginId.Contains(request.Search, _ignoreCase)),
                response.Model.Data.Any(d => d.TrackId.Contains(request.Search, _ignoreCase)),
                response.Model.Data.Any(d => d.Status.Contains(request.Search, _ignoreCase)),
                response.Model.Data.Any(d => d.Customer.Email.Contains(request.Search, _ignoreCase))
            };

            // at least one of the fields contains the search value
            assertions.Should().Contain(assert => assert);
        }

        [TestCase(null, "test")]
        [TestCase(FilterAction.Exclude, "test")]
        [TestCase(FilterAction.Include, "test")]
        public void QueryTransactions_ShouldAllowFilteringWithAction(FilterAction? action, string value)
        {
            var filter = new Filter {Action = action, Value = value, Field = Field.Email, Operator = Operator.Contains};
            var request = TestHelper.GetQueryRequest(new List<Filter> {filter});
            var response = CheckoutClient.ReportingService.QueryTransaction(request);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);

            if (action == FilterAction.Exclude)
            {
                response.Model.Data.Should().NotContain(t => t.Customer.Email.Contains(value));
            }
            else
            {
                response.Model.Data.Should().OnlyContain(t => t.Customer.Email.Contains(value));
            }
        }

        [TestCase(Field.Email)]
        [TestCase(Field.ChargeId)]
        [TestCase(Field.CardNumber)]
        [TestCase(Field.TrackId)]
        [TestCase(Field.Status)]
        public void QueryTransactions_ShouldAllowFilteringByField(Field? field)
        {
            var charge = CreateChargeWithNewTrackId(out string cardNumber);
            var filter = new Filter {Field = field, Value = GetChargePropertyValueFromField(charge, field, cardNumber)};

            var request = TestHelper.GetQueryRequest(new List<Filter> {filter});
            var response = CheckoutClient.ReportingService.QueryTransaction(request);

            response.Should().NotBeNull();

            if (field.HasValue)
            {
                response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
                response.Model.TotalRecords.Should().BeGreaterThan(0);

                if (field == Field.Status)
                {
                    response.Model.Data.Should().OnlyContain(t => t.Status.Equals(filter.Value, _ignoreCase));
                }
                else if (field == Field.Email)
                {
                    response.Model.Data.Should().OnlyContain(t => t.Customer.Email.Equals(filter.Value, _ignoreCase));
                }
                else
                {
                    response.Model.Data.Should().Contain(t => t.TrackId == charge.TrackId);
                }
            }
            else
            {
                response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
                response.HasError.Should().BeTrue();
            }
        }

        [TestCase(null)]
        [TestCase(Operator.Equals)]
        public void QueryTransactions_ShouldAllowFilteringWithOperator_ForEmailWithTransactionHistory(Operator? op)
        {
            TokenCard cardCreateModel = TestHelper.GetTokenCardModel();
            HttpResponse<CardTokenCreate> cardTokenResponse = CheckoutClient.TokenService.GetCardToken(cardCreateModel);
            CardTokenCharge cardTokenChargeModel = TestHelper.GetCardTokenChargeCreateModel(cardTokenResponse.Model.Id, TestHelper.RandomData.Email);
            HttpResponse<Charge> chargeResponse = CheckoutClient.ChargeService.ChargeWithCardToken(cardTokenChargeModel);
            QueryTransactions_ShouldAllowFilteringWithOperator(chargeResponse.Model.Email, op);
        }

        [TestCase("test", Operator.Begins)]
        [TestCase("test", Operator.Contains)]
        [TestCase("@checkouttest.co.uk", Operator.Ends)]
        public void QueryTransactions_ShouldAllowFilteringWithOperator(string value, Operator? op)
        {
            var filter = new Filter {Value = value, Field = Field.Email, Operator = op};
            var request = TestHelper.GetQueryRequest(new List<Filter> {filter});
            var response = CheckoutClient.ReportingService.QueryTransaction(request);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);

            if (op == Operator.Begins)
            {
                response.Model.Data.Should().OnlyContain(t => t.Customer.Email.StartsWith(value, _ignoreCase));
            }
            else if (op == Operator.Contains)
            {
                response.Model.Data.Should().OnlyContain(t => t.Customer.Email.Contains(value, _ignoreCase));
            }
            else if (op == Operator.Ends)
            {
                response.Model.Data.Should().OnlyContain(t => t.Customer.Email.EndsWith(value, _ignoreCase));
            }
            else
            {
                response.Model.Data.Should().OnlyContain(t => t.Customer.Email.Equals(value, _ignoreCase));
            }
        }

        [TestCase("test")]
        [TestCase(null)]
        public void QueryTransactions_ShouldAllowFilteringWithValue(string value)
        {
            var filter = new Filter {Value = value, Field = Field.Email, Operator = Operator.Contains};
            var request = TestHelper.GetQueryRequest(new List<Filter> {filter});
            var response = CheckoutClient.ReportingService.QueryTransaction(request);

            response.Should().NotBeNull();
            if (string.IsNullOrEmpty(value))
            {
                response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
                response.HasError.Should().BeTrue();
            }
            else
            {
                response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
                response.Model.Data.Should().OnlyContain(t => t.Customer.Email.Contains(value));
            }
        }

        [TestCase(Field.Email)]
        [TestCase(Field.ChargeId)]
        // [TestCase(Field.CardNumber)] // Querying by CardNumber is not working atm due to hard coded TrackId in TestHelper
        [TestCase(Field.TrackId)]
        [TestCase(Field.Status)]
        public void QueryTransactions_CreateChargeAndCapture_BothTransactionsFoundBy(Field? field)
        {
            var charge = CreateChargeWithNewTrackId(out string cardNumber);
            var filter = new Filter {Field = field, Value = GetChargePropertyValueFromField(charge, field, cardNumber)};

            var request = TestHelper.GetQueryRequest(new List<Filter> {filter});
            var firstQueryResponse = CheckoutClient.ReportingService.QueryTransaction(request);

#region Assert First Query Response

            firstQueryResponse.Should().NotBeNull();

            if (field.HasValue)
            {
                firstQueryResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
                firstQueryResponse.Model.TotalRecords.Should().BeGreaterThan(0);

                if (field == Field.Status)
                {
                    firstQueryResponse.Model.Data.Should().OnlyContain(t => t.Status.Equals(filter.Value, _ignoreCase));
                }
                else if (field == Field.Email)
                {
                    firstQueryResponse.Model.Data.Should().OnlyContain(t => t.Customer.Email.Equals(filter.Value, _ignoreCase));
                }
                else
                {
                    firstQueryResponse.Model.Data.Should().OnlyContain(t => t.TrackId.Equals(charge.TrackId));
                }
            }
            else
            {
                firstQueryResponse.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
                firstQueryResponse.HasError.Should().BeTrue();
            }

#endregion Assert First Query Response

            // capture charge and query 2nd time
            var chargeCapture = TestHelper.GetChargeCaptureModel(charge.Value);
            chargeCapture.TrackId = charge.TrackId;
            var captureChargeResponse = CheckoutClient.ChargeService.CaptureCharge(charge.Id, chargeCapture);

            captureChargeResponse.Should().NotBeNull();
            captureChargeResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);

            var secondQueryResponse = CheckoutClient.ReportingService.QueryTransaction(request);

#region Assert Second Query Response

            secondQueryResponse.Should().NotBeNull();

            if (field.HasValue)
            {
                secondQueryResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
                secondQueryResponse.Model.TotalRecords.Should().BeGreaterThan(0);

                if (field == Field.Status)
                {
                    secondQueryResponse.Model.Data.Should().OnlyContain(t => t.Status.Equals(filter.Value, _ignoreCase));
                }
                else if (field == Field.Email)
                {
                    secondQueryResponse.Model.Data.Should().OnlyContain(t => t.Customer.Email.Equals(filter.Value, _ignoreCase));
                }
                else if (field == Field.CardNumber || field == Field.ChargeId)
                {
                    secondQueryResponse.Model.Data.Should()
                        .OnlyContain(t => t.TrackId == charge.TrackId)
                        .And.HaveCount(1);
                }
                else if (field == Field.TrackId)
                {
                    secondQueryResponse.Model.Data.Should()
                        .OnlyContain(t => t.TrackId == charge.TrackId)
                        .And.HaveCount(2);
                }
            }
            else
            {
                secondQueryResponse.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
                secondQueryResponse.HasError.Should().BeTrue();
            }

#endregion Assert Second Query Response
        }

        private static string GetChargePropertyValueFromField(Charge charge, Field? field, string cardNumber = null)
        {
            if (!field.HasValue) return null;

            // card number has to be masked for querying
            if (field == Field.CardNumber)
            {
                return TestHelper.MaskCardNumber(cardNumber);
            }

            // Charge object does not contain ChargeId property
            var propertyName = field.ToString();
            if (field == Field.ChargeId)
            {
                propertyName = "Id";
            }

            return ReflectionHelper.GetPropertyValue(charge, propertyName) as string;
        }

        [Test]
        public void QueryTransactions_FromDateAfterTransactionCreated_NoTransactionsFound()
        {
            // create new charge
            var charge = CreateChargeWithNewTrackId();

            // query transactions starting from charge created date
            var chargeCreatedDate = DateTime.SpecifyKind(DateTime.Parse(charge.Created), DateTimeKind.Utc);
            var request = TestHelper.GetQueryRequest(charge.Email, chargeCreatedDate.AddHours(1));
            var response = CheckoutClient.ReportingService.QueryTransaction(request);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.TotalRecords.Should().Be(0);
        }

        [Test]
        public void QueryTransactions_FromDateBeforeTransactionCreated_OneTransactionFound()
        {
            // create new charge
            DateTime fromDate = DateTime.Now.AddDays(-7);
            var chargeResponse = CreateChargeWithNewTrackId();

            // query transactions starting from input date
            var request = TestHelper.GetQueryRequest(chargeResponse.Email, fromDate);
            var response = CheckoutClient.ReportingService.QueryTransaction(request);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Data.Should().OnlyContain(d => fromDate < d.Date);
            response.Model.TotalRecords.Should().Be(1);
        }

        [Test]
        public void QueryTransactions_FromDateIsNull_OneTransactionFound()
        {
            // create new charge
            var charge = CreateChargeWithNewTrackId();

            // query transactions starting from input date
            var request = TestHelper.GetQueryRequest(charge.Email);
            var response = CheckoutClient.ReportingService.QueryTransaction(request);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.TotalRecords.Should().Be(1);
        }

        [Test]
        public void QueryTransactions_MultipleFilters()
        {
            var filters = new List<Filter>
            {
                new Filter {Value = "test", Field = Field.Email, Operator = Operator.Contains},
                new Filter {Value = "captured", Field = Field.Status, Operator = Operator.Equals}
            };

            var request = TestHelper.GetQueryRequest(filters);
            var response = CheckoutClient.ReportingService.QueryTransaction(request);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.TotalRecords.Should().BeGreaterThan(0);
            response.Model.Data.Should().OnlyContain(t => t.Customer.Email.Contains("test"));
            response.Model.Data.Should().OnlyContain(t => t.Status == "Captured");
        }

        [Test]
        public void QueryTransactions_OppositeFilters_NoResults()
        {
            var filters = new List<Filter>
            {
                new Filter {Value = "test", Field = Field.Email, Operator = Operator.Contains},
                new Filter
                {
                    Action = FilterAction.Exclude,
                    Value = "test",
                    Field = Field.Email,
                    Operator = Operator.Contains
                }
            };
            var request = TestHelper.GetQueryRequest(filters);
            var response = CheckoutClient.ReportingService.QueryTransaction(request);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.TotalRecords.Should().Be(0);
        }

        [Test]
        public void QueryTransactions_ShouldAllowFilteringBySearchWithCardNumber()
        {
            var charge = CreateChargeWithNewTrackId(out string cardNumber);

            // query transactions containing the generated card number
            var request = TestHelper.GetQueryRequest(TestHelper.MaskCardNumber(cardNumber), null, null,
                SortColumn.Date);
            var response = CheckoutClient.ReportingService.QueryTransaction(request);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.TotalRecords.Should().BeGreaterThan(0);
            response.Model.Data.Should().Contain(t => t.TrackId == charge.TrackId);
        }

        [Test]
        public void QueryTransactions_ToDateAfterTransactionCreated_OneTransactionsFound()
        {
            // create new charge
            var charge = CreateChargeWithNewTrackId();

            // query transactions starting from charge created date
            var chargeCreatedDate = DateTime.SpecifyKind(DateTime.Parse(charge.Created), DateTimeKind.Utc);
            var request = TestHelper.GetQueryRequest(charge.Email, null, chargeCreatedDate.AddHours(1));
            var response = CheckoutClient.ReportingService.QueryTransaction(request);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Data.Should().OnlyContain(t => request.ToDate > t.Date);
            response.Model.TotalRecords.Should().Be(1);
        }

        [Test]
        public void QueryTransactions_ToDateBeforeTransactionCreated_NoTransactionsFound()
        {
            var charge = CreateChargeWithNewTrackId(); // create new charge
            var toDate = Convert.ToDateTime(charge.Created).AddMilliseconds(-1); // ensure toDate is always before charge.Created

            var request = TestHelper.GetQueryRequest(charge.Email, null, toDate); // query transactions starting from input date
            var response = CheckoutClient.ReportingService.QueryTransaction(request);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.TotalRecords.Should().Be(0);
        }

        [Test]
        public void QueryTransactions_ToDateIsNull_OneTransactionFound()
        {
            // create new charge
            var charge = CreateChargeWithNewTrackId();

            // query transactions starting from input date
            var request = TestHelper.GetQueryRequest(charge.Email);
            var response = CheckoutClient.ReportingService.QueryTransaction(request);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.TotalRecords.Should().Be(1);
        }

        [Test]
        public void QueryChargebacks()
        {
            DateTime fromDate = DateTime.Now.AddDays(-7); // Looking 7 days in the past
            var request = TestHelper.GetQueryRequest(fromDate: fromDate);
            var response = CheckoutClient.ReportingService.QueryChargeback(request);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            //response.Model.TotalRecords.Should().BeGreaterThan(1);
        }
    }
}