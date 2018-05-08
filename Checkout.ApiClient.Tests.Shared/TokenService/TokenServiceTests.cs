using System.Net;
using Checkout.ApiServices.Tokens.RequestModels;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture(Category = "TokensApi")]
    public class TokenServiceTests : BaseServiceTests
    {
        [Test]
        public void CreatePaymentToken()
        {
            var paymentTokenCreateModel = TestHelper.GetPaymentTokenCreateModel(TestHelper.RandomData.Email);
            var response = CheckoutClient.TokenService.CreatePaymentToken(paymentTokenCreateModel);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().StartWith("pay_tok_");
        }

        [Test]
        public void UpdatePaymentToken()
        {
            var paymentTokenCreateModel = TestHelper.GetPaymentTokenCreateModel(TestHelper.RandomData.Email);
            var createPaymentTokenResponse = CheckoutClient.TokenService.CreatePaymentToken(paymentTokenCreateModel);

            var paymentTokenUpdateModel = TestHelper.GetPaymentTokenUpdateModel();
            var response = CheckoutClient.TokenService.UpdatePaymentToken(createPaymentTokenResponse.Model.Id, paymentTokenUpdateModel);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Message.ShouldBeEquivalentTo("ok");
        }

        [Test]
        public void CreateVisaCheckoutToken()
        {
            var visaCheckoutRequest = new VisaCheckoutTokenCreate { CallId = "3023957850660287501" };
            var response = CheckoutClient.TokenService.CreateVisaCheckoutCardToken(visaCheckoutRequest);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().StartWith("card_tok_");
            response.Model.BinData.Should().BeNull();
        }

        [Test]
        public void CreateVisaCheckoutToken_IncludeBinData()
        {
            var visaCheckoutRequest = new VisaCheckoutTokenCreate { CallId = "3023957850660287501", IncludeBinData = true };
            var response = CheckoutClient.TokenService.CreateVisaCheckoutCardToken(visaCheckoutRequest);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().StartWith("card_tok_");
            response.Model.BinData.Should().NotBeNull();
        }

        [Test]
        public void CreateVisaCheckoutToken_EmptyRequest_BadRequest()
        {
            var response = CheckoutClient.TokenService.CreateVisaCheckoutCardToken(new VisaCheckoutTokenCreate { CallId = "" });

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);

            response.HasError.Should().BeTrue();
            response.Model.Should().BeNull();
            response.Error.Message.Should().Be("Invalid value for 'token'");
        }
    }
}