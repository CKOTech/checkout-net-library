using System.Collections.Generic;
using System.Net;
using System.Threading;
using Checkout.ApiServices.Charges.RequestModels;
using Checkout.ApiServices.SharedModels;
using Checkout.ApiServices.Tokens.ResponseModels;
using FluentAssertions;
using NUnit.Framework;
using Tests.Utils;

namespace Tests
{
    [TestFixture(Category = "ChargesApi")]
    public class ChargeService : BaseServiceTests
    {
        [Test]
        public void CaptureCharge()
        {
            var cardCreateModel = TestHelper.GetCardChargeCreateModel(TestHelper.RandomData.Email);

            var charge = CheckoutClient.ChargeService.ChargeWithCard(cardCreateModel).Model;

            var chargeCaptureModel = TestHelper.GetChargeCaptureModel(charge.Value);

            var response = CheckoutClient.ChargeService.CaptureCharge(charge.Id, chargeCaptureModel);

            ////Check if charge details match
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.OriginalId.Should().BeEquivalentTo(charge.Id);
        }

        [Test]
        public void CreateChargeWithCard()
        {
            var cardCreateModel = TestHelper.GetCardChargeCreateModel(TestHelper.RandomData.Email);
            var response = CheckoutClient.ChargeService.ChargeWithCard(cardCreateModel);

            //Check if charge details match
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().StartWith("charge_");

            response.Model.AutoCapTime.Should().Be(cardCreateModel.AutoCapTime);
            response.Model.AutoCapture.Should().BeEquivalentTo(cardCreateModel.AutoCapture);
            response.Model.Email.Should().BeEquivalentTo(cardCreateModel.Email);
            response.Model.Currency.Should().BeEquivalentTo(cardCreateModel.Currency);
            response.Model.Description.Should().BeEquivalentTo(cardCreateModel.Description);
            response.Model.Value.Should().Be(cardCreateModel.Value);
            response.Model.Status.Should().NotBeNullOrEmpty();
            response.Model.AuthCode.Should().NotBeNullOrEmpty();
            response.Model.ResponseCode.Should().NotBeNullOrEmpty();

            //Check if card details match
            response.Model.Card.Name.Should().Be(cardCreateModel.Card.Name);
            response.Model.Card.ExpiryMonth.Should().Be(cardCreateModel.Card.ExpiryMonth);
            response.Model.Card.ExpiryYear.Should().Be(cardCreateModel.Card.ExpiryYear);
            response.Model.Card.Bin.Should().Be(cardCreateModel.Card.Number.Substring(0, 6));
            cardCreateModel.Card.Number.Should().EndWith(response.Model.Card.Last4);
            response.Model.Card.BillingDetails.ShouldBeEquivalentTo(cardCreateModel.Card.BillingDetails);

            //Check if shipping details match
            response.Model.Products.ShouldBeEquivalentTo(cardCreateModel.Products);

            //Check if metadatadetails match
            response.Model.Metadata.ShouldBeEquivalentTo(cardCreateModel.Metadata);
        }

        [Test]
        public void CreateChargeWithCard_3DChargeMode()
        {
            var cardCreateModel = TestHelper.GetCardChargeCreateModel(TestHelper.RandomData.Email);
            cardCreateModel.ChargeMode = 2;

            var response = CheckoutClient.ChargeService.ChargeWithCard(cardCreateModel);

            //Check if charge details match
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().StartWith("pay_tok");

            response.Model.ChargeMode.Should().Be(2);
            response.Model.RedirectUrl.Should().StartWith("http");
            response.Model.ResponseCode.Should().NotBeNullOrEmpty();
            response.Model.TrackId.ShouldBeEquivalentTo(cardCreateModel.TrackId);
        }

        [Test]
        public void CreateChargeWithCard_AttemptN3DIsNotSet_CardIsNotEligible()
        {
            var cardCreateModel = TestHelper.GetCardChargeCreateModel(TestHelper.RandomData.Email);
            cardCreateModel.ChargeMode = 2;
            cardCreateModel.AttemptN3D = false;
            cardCreateModel.Value = "6900";

            var response = CheckoutClient.ChargeService.ChargeWithCard(cardCreateModel);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().StartWith("pay_tok");
            response.Model.ResponseCode.Should().Be("20150"); //card not 3DS enabled
        }

        [Test]
        public void CreateChargeWithCard_AttemptN3DIsNotSet_AuthenticationAvailable()
        {
            var cardCreateModel = TestHelper.GetCardChargeCreateModel(TestHelper.RandomData.Email);
            cardCreateModel.ChargeMode = 2;
            cardCreateModel.AttemptN3D = false;

            var response = CheckoutClient.ChargeService.ChargeWithCard(cardCreateModel);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().StartWith("pay_tok");
            response.Model.ResponseCode.Should().NotBeNullOrEmpty();
            response.Model.ChargeMode.Should().Be(2); //processed as 3DS
        }

        [Test]
        public void CreateChargeWithCard_AttemptN3DIsSet_CardIsNotEligible()
        {
            var cardCreateModel = TestHelper.GetCardChargeCreateModel(TestHelper.RandomData.Email);
            cardCreateModel.ChargeMode = 2;
            cardCreateModel.AttemptN3D = true;
            cardCreateModel.Value = "6900";

            var response = CheckoutClient.ChargeService.ChargeWithCard(cardCreateModel);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().StartWith("pay_tok");
            response.Model.ResponseCode.Should().NotBeNullOrEmpty();
            response.Model.ChargeMode.Should().Be(1); //downgraded to N3D
        }


        [Test]
        public void CreateChargeWithCardId_AttemptN3DIsSet_CardIsNotEligible()
        {
            var customer = CheckoutClient.CustomerService.CreateCustomer(TestHelper.GetCustomerCreateModelWithCard()).Model;

            var cardIdChargeCreateModel = TestHelper.GetCardIdChargeCreateModel(customer.Cards.Data[0].Id, customer.Email);
            cardIdChargeCreateModel.ChargeMode = 2;
            cardIdChargeCreateModel.AttemptN3D = true;
            cardIdChargeCreateModel.Value = "6900";

            var response = CheckoutClient.ChargeService.ChargeWithCardId(cardIdChargeCreateModel);

            //Check if charge details match
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().StartWith("pay_tok");

            response.Model.ChargeMode.Should().Be(1); //downgraded to N3D
            response.Model.RedirectUrl.Should().StartWith("http");
            response.Model.ResponseCode.Should().NotBeNullOrEmpty();
            response.Model.TrackId.ShouldBeEquivalentTo(cardIdChargeCreateModel.TrackId);
        }

        [Test]
        public void CreateChargeWithCardId_3DChargeMode()
        {
            var customer =
              CheckoutClient.CustomerService.CreateCustomer(TestHelper.GetCustomerCreateModelWithCard()).Model;

            var cardIdChargeCreateModel = TestHelper.GetCardIdChargeCreateModel(customer.Cards.Data[0].Id,
                customer.Email);
            cardIdChargeCreateModel.ChargeMode = 2;

            var response = CheckoutClient.ChargeService.ChargeWithCardId(cardIdChargeCreateModel);

            //Check if charge details match
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().StartWith("pay_tok");

            response.Model.ChargeMode.Should().Be(2);
            response.Model.RedirectUrl.Should().StartWith("http");
            response.Model.ResponseCode.Should().NotBeNullOrEmpty();
            response.Model.TrackId.ShouldBeEquivalentTo(cardIdChargeCreateModel.TrackId);
        }

        [Test]
        public void CreateChargeWithCardId()
        {
            var customer =
                CheckoutClient.CustomerService.CreateCustomer(TestHelper.GetCustomerCreateModelWithCard()).Model;

            var cardIdChargeCreateModel = TestHelper.GetCardIdChargeCreateModel(customer.Cards.Data[0].Id,
                customer.Email);

            var response = CheckoutClient.ChargeService.ChargeWithCardId(cardIdChargeCreateModel);

            ////Check if charge details match
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().StartWith("charge_");

            response.Model.AutoCapTime.Should().Be(cardIdChargeCreateModel.AutoCapTime);
            response.Model.AutoCapture.Should().BeEquivalentTo(cardIdChargeCreateModel.AutoCapture);
            response.Model.Email.Should().BeEquivalentTo(cardIdChargeCreateModel.Email);
            response.Model.Currency.Should().BeEquivalentTo(cardIdChargeCreateModel.Currency);
            response.Model.Description.Should().BeEquivalentTo(cardIdChargeCreateModel.Description);
            response.Model.Value.Should().Be(cardIdChargeCreateModel.Value);
            response.Model.Card.Id.Should().Be(cardIdChargeCreateModel.CardId);
            response.Model.Status.Should().NotBeNullOrEmpty();
            response.Model.AuthCode.Should().NotBeNullOrEmpty();
            response.Model.ResponseCode.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void CreateChargeWithCardToken()
        {
            var cardToken = "card_tok_34FF74EC-5E8A-41CD-A7FF-8992F54DAA9F"; // card token for the JS charge

            var cardTokenChargeModel = TestHelper.GetCardTokenChargeCreateModel(cardToken, TestHelper.RandomData.Email);

            var response = CheckoutClient.ChargeService.ChargeWithCardToken(cardTokenChargeModel);

            ////Check if charge details match
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().StartWith("charge_");

            response.Model.AutoCapTime.Should().Be(cardTokenChargeModel.AutoCapTime);
            response.Model.AutoCapture.Should().BeEquivalentTo(cardTokenChargeModel.AutoCapture);
            response.Model.Email.Should().BeEquivalentTo(cardTokenChargeModel.Email);
            response.Model.Currency.Should().BeEquivalentTo(cardTokenChargeModel.Currency);
            response.Model.Description.Should().BeEquivalentTo(cardTokenChargeModel.Description);
            response.Model.Value.Should().Be(cardTokenChargeModel.Value);
            response.Model.Status.Should().NotBeNullOrEmpty();
            response.Model.AuthCode.Should().NotBeNullOrEmpty();
            response.Model.ResponseCode.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void CreateChargeWithCustomerDefaultCard()
        {
            var customer =
                CheckoutClient.CustomerService.CreateCustomer(
                    TestHelper.GetCustomerCreateModelWithCard(CardProvider.Mastercard)).Model;

            var baseChargeModel = TestHelper.GetCustomerDefaultCardChargeCreateModel(customer.Id);
            var response = CheckoutClient.ChargeService.ChargeWithDefaultCustomerCard(baseChargeModel);

            ////Check if charge details match
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().StartWith("charge_");

            response.Model.AutoCapTime.Should().Be(baseChargeModel.AutoCapTime);
            response.Model.AutoCapture.Should().BeEquivalentTo(baseChargeModel.AutoCapture);
            response.Model.Currency.Should().BeEquivalentTo(baseChargeModel.Currency);
            response.Model.Description.Should().BeEquivalentTo(baseChargeModel.Description);
            response.Model.Value.Should().Be(baseChargeModel.Value);
            response.Model.Email.Should().NotBeNullOrEmpty();
            response.Model.Status.Should().NotBeNullOrEmpty();
            response.Model.AuthCode.Should().NotBeNullOrEmpty();
            response.Model.ResponseCode.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void CreateChargeWithLocalPayment()
        {
            var paymentTokenCreateModel = TestHelper.GetPaymentTokenCreateModel(TestHelper.RandomData.Email, 3, "EUR");
            var paymentToken = CheckoutClient.TokenService.CreatePaymentToken(paymentTokenCreateModel);

            var localPaymentCharge = TestHelper.GetLocalPaymentChargeModel(paymentToken.Model.Id);
            var chargeResponse = CheckoutClient.ChargeService.ChargeWithLocalPayment(localPaymentCharge);

            chargeResponse.Should().NotBeNull();
            chargeResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            chargeResponse.Model.Id.Should().StartWith("pay_tok_");
            chargeResponse.Model.ResponseCode.Should().Be("10000");
            chargeResponse.Model.ChargeMode.Should().Be(3);
            chargeResponse.Model.LocalPayment.PaymentUrl.Should().NotBeNullOrWhiteSpace();
        }

        [Test]
        public void GetCharge()
        {
            var cardCreateModel = TestHelper.GetCardChargeCreateModel(TestHelper.RandomData.Email);

            var chargeResponse = CheckoutClient.ChargeService.ChargeWithCard(cardCreateModel);

            var response = CheckoutClient.ChargeService.GetCharge(chargeResponse.Model.Id);

            //Check if charge details match
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().StartWith("charge_");

            chargeResponse.Model.Id.Should().Be(response.Model.Id);
        }

        [Test]
        public void GetChargeHistory()
        {
            #region setup charge history

            var cardCreateModel = TestHelper.GetCardChargeCreateModel(TestHelper.RandomData.Email);

            var chargeResponse = CheckoutClient.ChargeService.ChargeWithCard(cardCreateModel);

            var chargeVoidModel = TestHelper.GetChargeVoidModel();

            var voidResponse = CheckoutClient.ChargeService.VoidCharge(chargeResponse.Model.Id, chargeVoidModel);

            #endregion

            var response = CheckoutClient.ChargeService.GetChargeHistory(voidResponse.Model.Id);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Charges.Should().HaveCount(2);

            response.Model.Charges[0].Id.Should().Be(voidResponse.Model.Id);
            response.Model.Charges[1].Id.Should().Be(chargeResponse.Model.Id);
        }

        [Test]
        public void GetChargeWithMultipleHistory()
        {
            // charge
            var cardCreateModel = TestHelper.GetCardChargeCreateModel(TestHelper.RandomData.Email);
            var chargeResponse = CheckoutClient.ChargeService.ChargeWithCard(cardCreateModel);

            // capture
            var chargeCaptureModel = TestHelper.GetChargeCaptureModel(chargeResponse.Model.Value);
            var captureResponse = CheckoutClient.ChargeService.CaptureCharge(chargeResponse.Model.Id, chargeCaptureModel);

            // refund
            var chargeRefundModel = TestHelper.GetChargeRefundModel(chargeResponse.Model.Value);
            var refundResponse = CheckoutClient.ChargeService.RefundCharge(captureResponse.Model.Id, chargeRefundModel);

            var response = CheckoutClient.ChargeService.GetChargeHistory(chargeResponse.Model.Id);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Charges.Should().HaveCount(3);

            response.Model.Charges[0].Id.Should().Be(refundResponse.Model.Id);
            response.Model.Charges[1].Id.Should().Be(captureResponse.Model.Id);
            response.Model.Charges[2].Id.Should().Be(chargeResponse.Model.Id);

            chargeResponse.Model.Id.Should().Be(captureResponse.Model.OriginalId);
            refundResponse.Model.OriginalId.Should().Be(captureResponse.Model.Id);
        }

        [Test]
        public void RefundCharge()
        {
            var cardCreateModel = TestHelper.GetCardChargeCreateModel(TestHelper.RandomData.Email);

            var charge = CheckoutClient.ChargeService.ChargeWithCard(cardCreateModel).Model;

            var chargeCaptureModel = TestHelper.GetChargeCaptureModel(charge.Value);

            var captureResponse = CheckoutClient.ChargeService.CaptureCharge(charge.Id, chargeCaptureModel);

            var chargeRefundModel = TestHelper.GetChargeRefundModel(charge.Value);
            var response = CheckoutClient.ChargeService.RefundCharge(captureResponse.Model.Id, chargeRefundModel);

            //Check if charge details match
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.OriginalId.Should().Be(captureResponse.Model.Id);
        }

        [Test]
        public void UpdateCharge()
        {
            var cardCreateModel = TestHelper.GetCardChargeCreateModel(TestHelper.RandomData.Email);
            var charge = CheckoutClient.ChargeService.ChargeWithCard(cardCreateModel).Model;

            var chargeUpdateModel = TestHelper.GetChargeUpdateModel();

            var response = CheckoutClient.ChargeService.UpdateCharge(charge.Id, chargeUpdateModel);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Message.Should().BeEquivalentTo("Ok");
        }

        [Test]
        public void VerifyChargeByPaymentToken()
        {
            var paymentToken = "pay_tok_cacdc3d0-f912-4ebb-9f84-8fde65e05fbd"; // payment token for the JS charge

            var response = CheckoutClient.ChargeService.VerifyCharge(paymentToken);

            //Check if charge details match
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().StartWith("charge_");
        }

        [Test]
        public void VoidCharge()
        {
            var cardCreateModel = TestHelper.GetCardChargeCreateModel(TestHelper.RandomData.Email);

            var charge = CheckoutClient.ChargeService.ChargeWithCard(cardCreateModel).Model;

            var chargeVoidModel = TestHelper.GetChargeVoidModel();

            var response = CheckoutClient.ChargeService.VoidCharge(charge.Id, chargeVoidModel);

            //Check if charge details match
            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.OriginalId.Should().Be(charge.Id);
        }
    }
}