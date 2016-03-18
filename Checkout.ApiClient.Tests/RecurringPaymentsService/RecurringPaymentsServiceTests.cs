using System;
using System.Linq;
using System.Net;
using Checkout.ApiServices.RecurringPayments.RequestModels;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.RecurringPaymentsService
{
    [TestFixture(Category = "RecurringPaymentsApi")]
    public class RecurringPaymentsServiceTests : BaseServiceTests
    {
        [Test]
        public void CancelCustomerPaymentPlan_ShouldReturnOk()
        {
            var cardCreateModel = TestHelper.GetCardChargeCreateModelWithNewPaymentPlan(TestHelper.RandomData.Email);
            var createResponse = CheckoutClient.ChargeService.ChargeWithCard(cardCreateModel);
            var cancelResponse =
                CheckoutClient.RecurringPaymentsService.CancelCustomerPaymentPlan(
                    createResponse.Model.CustomerPaymentPlans.Single().CustomerPlanId);

            cancelResponse.Should().NotBeNull();
            cancelResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            cancelResponse.Model.Message.Should().BeEquivalentTo("OK");
        }

        [Test]
        public void CancelPaymentPlan_ShouldReturnOk()
        {
            var paymentPlanModel = TestHelper.GetSinglePaymentPlanCreateModel();
            var createResponseModel =
                CheckoutClient.RecurringPaymentsService.CreatePaymentPlan(paymentPlanModel).Model.PaymentPlans.Single();
            var cancelResponse = CheckoutClient.RecurringPaymentsService.CancelPaymentPlan(createResponseModel.PlanId);

            cancelResponse.Should().NotBeNull();
            cancelResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            cancelResponse.Model.Message.Should().BeEquivalentTo("OK");
        }

        [Test]
        public void CreateFromExistingCustomerPaymentPlanWithCharge_ShouldReturnEquivalentPlan()
        {
            var paymentPlanModel = TestHelper.GetSinglePaymentPlanCreateModel();
            var createResponseModel =
                CheckoutClient.RecurringPaymentsService.CreatePaymentPlan(paymentPlanModel).Model.PaymentPlans.Single();
            var cardCreateModel = TestHelper.GetCardChargeCreateModelWithExistingPaymentPlan(
                createResponseModel.PlanId, null, TestHelper.RandomData.Email);
            var chargeResponse = CheckoutClient.ChargeService.ChargeWithCard(cardCreateModel);

            chargeResponse.Should().NotBeNull();
            chargeResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);

            var chargeResponseModel = chargeResponse.Model.CustomerPaymentPlans.Single();
            chargeResponseModel.ShouldBeEquivalentTo(createResponseModel,
                options =>
                    options.Excluding(o => o.CustomerPlanId)
                        .Excluding(o => o.CustomerId)
                        .Excluding(o => o.CardId)
                        .Excluding(o => o.RecurringCountLeft)
                        .Excluding(o => o.TotalCollectedCount)
                        .Excluding(o => o.TotalCollectedValue)
                        .Excluding(o => o.PreviousRecurringDate)
                        .Excluding(o => o.NextRecurringDate)
                        .Excluding(o => o.StartDate));
        }

        [Test]
        public void CreateNewCustomerPaymentPlanWithCharge_ShouldReturnEquivalentPlan()
        {
            var cardCreateModel = TestHelper.GetCardChargeCreateModelWithNewPaymentPlan(TestHelper.RandomData.Email);
            var response = CheckoutClient.ChargeService.ChargeWithCard(cardCreateModel);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);

            var customerPaymentPlanModel = cardCreateModel.PaymentPlans.Single();
            var responseModel = response.Model.CustomerPaymentPlans.Single();

            responseModel.PlanId.Should().NotBeNullOrEmpty();
            responseModel.Status.Should().NotBeNull();
            responseModel.Name.Should().Be(customerPaymentPlanModel.Name);
            responseModel.PlanTrackId.Should().Be(customerPaymentPlanModel.PlanTrackId);
            responseModel.RecurringCount.Should().Be(customerPaymentPlanModel.RecurringCount);
            responseModel.Value.Should().Be(customerPaymentPlanModel.Value);
            responseModel.AutoCapTime.Should().Be(customerPaymentPlanModel.AutoCapTime);
            responseModel.Cycle.Should().Be(customerPaymentPlanModel.Cycle);
        }

        [Test]
        public void CreatePaymentPlan_ShouldReturnEquivalentPlan()
        {
            var paymentPlanCreateModel = TestHelper.GetSinglePaymentPlanCreateModel();
            var response = CheckoutClient.RecurringPaymentsService.CreatePaymentPlan(paymentPlanCreateModel);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);

            var singlePlanModel = paymentPlanCreateModel.PaymentPlans.Single();
            var responseModel = response.Model.PaymentPlans.Single();

            responseModel.ShouldBeEquivalentTo(singlePlanModel,
                options => options.Excluding(o => o.PlanId).Excluding(o => o.Status));
            responseModel.PlanId.Should().NotBeNullOrEmpty();
            responseModel.Status.Should().NotBeNull();
        }

        [Test]
        public void GetCustomerPaymentPlan_ShouldReturnEquivalentPlan()
        {
            var cardCreateModel = TestHelper.GetCardChargeCreateModelWithNewPaymentPlan(TestHelper.RandomData.Email);
            var customerPlanModel =
                CheckoutClient.ChargeService.ChargeWithCard(cardCreateModel).Model.CustomerPaymentPlans.Single();
            var getResponse =
                CheckoutClient.RecurringPaymentsService.GetCustomerPaymentPlan(customerPlanModel.CustomerPlanId);

            getResponse.Should().NotBeNull();
            getResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            customerPlanModel.ShouldBeEquivalentTo(customerPlanModel,
                options => options.Excluding(o => o.CustomerId).Excluding(o => o.CardId));
        }

        [Test]
        public void GetPaymentPlan_ShouldReturnEquivalentPlan()
        {
            var paymentPlanModel = TestHelper.GetSinglePaymentPlanCreateModel();
            var createResponseModel =
                CheckoutClient.RecurringPaymentsService.CreatePaymentPlan(paymentPlanModel).Model.PaymentPlans.Single();
            var getResponse = CheckoutClient.RecurringPaymentsService.GetPaymentPlan(createResponseModel.PlanId);

            getResponse.Should().NotBeNull();
            getResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);

            var getResponseModel = getResponse.Model;
            getResponseModel.ShouldBeEquivalentTo(createResponseModel,
                options => options.Excluding(o => o.PlanId).Excluding(o => o.Status));
            getResponseModel.PlanId.Should().NotBeNullOrEmpty();
            getResponseModel.Status.Should().NotBeNull();
        }

        [Test]
        public void UpdateCustomerPaymentPlan_ShouldReturnOk()
        {
            var cardCreateModel = TestHelper.GetCardChargeCreateModelWithNewPaymentPlan(TestHelper.RandomData.Email);
            var createResponseModel =
                CheckoutClient.ChargeService.ChargeWithCard(cardCreateModel).Model.CustomerPaymentPlans.Single();
            var customerPlanUpdateModel = TestHelper.GetCustomerPaymentPlanUpdateModel(createResponseModel.CardId, RecurringPlanStatus.Suspended);
            var updateResponse =
                CheckoutClient.RecurringPaymentsService.UpdateCustomerPaymentPlan(createResponseModel.CustomerPlanId,
                    customerPlanUpdateModel);

            updateResponse.Should().NotBeNull();
            updateResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            updateResponse.Model.Message.Should().BeEquivalentTo("OK");
        }

        [Test]
        public void UpdatePaymentPlan_ShouldReturnOk()
        {
            var paymentPlanModel = TestHelper.GetSinglePaymentPlanCreateModel();
            var createResponseModel =
                CheckoutClient.RecurringPaymentsService.CreatePaymentPlan(paymentPlanModel).Model.PaymentPlans.Single();
            var updateResponse = CheckoutClient.RecurringPaymentsService.UpdatePaymentPlan(createResponseModel.PlanId,
                TestHelper.GetPaymentPlanUpdateModel());

            updateResponse.Should().NotBeNull();
            updateResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            updateResponse.Model.Message.Should().BeEquivalentTo("OK");
        }

        public void QueryPaymentPlan_ShouldAllowDateFiltering()
        {
            var fromDate = DateTime.Now;
            var paymentPlanResponse = CheckoutClient.RecurringPaymentsService.CreatePaymentPlan(TestHelper.GetSinglePaymentPlanCreateModel());

            var queryResponse1 = CheckoutClient.RecurringPaymentsService.QueryPaymentPlan(new QueryPaymentPlanRequest {FromDate = fromDate});
            queryResponse1.Should().NotBeNull();
            queryResponse1.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            queryResponse1.Model.TotalRows.Should().Be(1);

            var queryResponse2 = CheckoutClient.RecurringPaymentsService.QueryPaymentPlan(new QueryPaymentPlanRequest { ToDate = fromDate.AddDays(-1) });
            queryResponse2.Should().NotBeNull();
            queryResponse2.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            queryResponse2.Model.Data.Should().NotContain(x => x.PlanId == paymentPlanResponse.Model.PaymentPlans.Single().PlanId);
        }

        public void QueryCustomerPaymentPlan_ShouldAllowDateFiltering()
        {
            var fromDate = DateTime.Now;
            var cardCreateModel = TestHelper.GetCardChargeCreateModelWithNewPaymentPlan(TestHelper.RandomData.Email);
            var chargeResponse = CheckoutClient.ChargeService.ChargeWithCard(cardCreateModel);

            var queryResponse1 = CheckoutClient.RecurringPaymentsService.QueryPaymentPlan(new QueryPaymentPlanRequest { FromDate = fromDate });
            queryResponse1.Should().NotBeNull();
            queryResponse1.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            queryResponse1.Model.TotalRows.Should().Be(1);

            var queryResponse2 = CheckoutClient.RecurringPaymentsService.QueryPaymentPlan(new QueryPaymentPlanRequest { ToDate = fromDate.AddDays(-1) });
            queryResponse2.Should().NotBeNull();
            queryResponse2.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            queryResponse2.Model.Data.Should().NotContain(x => x.PlanId == chargeResponse.Model.CustomerPaymentPlans.Single().PlanId);
        }

        [TestCase("Offset", 2)]
        [TestCase("Count", 15)]
        public void QueryPaymentPlan_ShouldAllowPagination(string propertyName, int propertyValue)
        {
            var queryRequest = new QueryPaymentPlanRequest();
            ReflectionHelper.SetPropertyValue(queryRequest, propertyName, propertyValue);
            var queryResponse = CheckoutClient.RecurringPaymentsService.QueryPaymentPlan(queryRequest);

            queryResponse.Should().NotBeNull();
            queryResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            queryResponse.Model.TotalRows.Should().BeGreaterThan(0);
            ReflectionHelper.GetPropertyValue(queryResponse.Model, propertyName).ShouldBeEquivalentTo(propertyValue);
        }

        [TestCase("Offset", 2)]
        [TestCase("Count", 15)]
        public void QueryCustomerPaymentPlan_ShouldAllowPagination(string propertyName, int propertyValue)
        {
            var queryRequest = new QueryCustomerPaymentPlanRequest();
            ReflectionHelper.SetPropertyValue(queryRequest, propertyName, propertyValue);
            var queryResponse = CheckoutClient.RecurringPaymentsService.QueryCustomerPaymentPlan(queryRequest);

            queryResponse.Should().NotBeNull();
            queryResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            queryResponse.Model.TotalRows.Should().BeGreaterThan(0);
            ReflectionHelper.GetPropertyValue(queryResponse.Model, propertyName).ShouldBeEquivalentTo(propertyValue);
        }

        [TestCase("Name")]
        [TestCase("PlanTrackId")]
        [TestCase("AutoCapTime")]
        [TestCase("Currency")]
        [TestCase("Value")]
        [TestCase("Status")]
        public void QueryPaymentPlan_ShouldReturnEquivalentSearchObjects(string propertyName)
        {
            var createResponse = CheckoutClient.RecurringPaymentsService.CreatePaymentPlan(TestHelper.GetSinglePaymentPlanCreateModel());
            var paymentPlan = createResponse.Model.PaymentPlans.Single();
            var propertyValue = ReflectionHelper.GetPropertyValue(paymentPlan, propertyName);

            var queryRequest = TestHelper.GetCustomQueryPaymentPlanRequest(propertyName, propertyValue, paymentPlan.Currency);
            var queryResponse = CheckoutClient.RecurringPaymentsService.QueryPaymentPlan(queryRequest);
            queryResponse.Should().NotBeNull();
            queryResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            queryResponse.Model.TotalRows.Should().BeGreaterThan(0);

            foreach (var paymenPlan in queryResponse.Model.Data)
            {
                ReflectionHelper.GetPropertyValue(paymenPlan, propertyName).ShouldBeEquivalentTo(propertyValue);
            }
        }

        [TestCase("PlanId")]
        [TestCase("CardId")]
        [TestCase("CustomerId")]
        [TestCase("Name")]
        [TestCase("PlanTrackId")]
        [TestCase("AutoCapTime")]
        [TestCase("Currency")]
        [TestCase("Value")]
        [TestCase("Cycle")]
        [TestCase("StartDate")]
        [TestCase("Status")]
        public void QueryCustomerPaymentPlan_ShouldReturnEquivalentSearchObjects(string propertyName)
        {
            object propertyValue;
            var cardCreateModel = TestHelper.GetCardChargeCreateModelWithNewPaymentPlan(TestHelper.RandomData.Email);
            var chargeResponseModel = CheckoutClient.ChargeService.ChargeWithCard(cardCreateModel).Model;
            var customerPaymentPlan = chargeResponseModel.CustomerPaymentPlans.First();

            // Hack for CardId and CustomerId property match
            if (propertyName == "CardId")
            {
                propertyValue = ReflectionHelper.GetPropertyValue(chargeResponseModel, "Card.Id");
            }
            else if (propertyName == "CustomerId")
            {
                propertyValue = ReflectionHelper.GetPropertyValue(chargeResponseModel, "Card.CustomerId");
            }
            else
            {
                propertyValue = TestHelper.GetRecurringPlanPropertyValue(customerPaymentPlan, propertyName);
            }

            var queryRequest = TestHelper.GetCustomQueryCustomerPaymentPlanRequest(propertyName, propertyValue, cardCreateModel.Currency);
            var queryResponse = CheckoutClient.RecurringPaymentsService.QueryCustomerPaymentPlan(queryRequest);
            queryResponse.Should().NotBeNull();
            queryResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            queryResponse.Model.TotalRows.Should().BeGreaterThan(0);

            foreach (var customerPaymenPlan in queryResponse.Model.Data)
            {
                TestHelper.GetRecurringPlanPropertyValue(customerPaymenPlan, propertyName).ShouldBeEquivalentTo(propertyValue);
            }
        }
    }
}