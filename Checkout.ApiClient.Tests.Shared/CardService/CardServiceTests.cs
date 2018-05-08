using System.Linq;
using System.Net;
using FluentAssertions;
using NUnit.Framework;
using Tests.Utils;

namespace Tests
{
    [TestFixture(Category = "CardsApi")]
    public class CardServiceTests : BaseServiceTests
    {
        [Test]
        public void CreateCard()
        {
            var customer = CheckoutClient.CustomerService.CreateCustomer(TestHelper.GetCustomerCreateModelWithNoCard()).Model;
            var cardCreateModel = TestHelper.GetCardCreateModel();
            var response = CheckoutClient.CardService.CreateCard(customer.Id, cardCreateModel);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().StartWith("card_");
            response.Model.CustomerId.Should().BeEquivalentTo(customer.Id);
            response.Model.Name.Should().Be(cardCreateModel.Name);
            response.Model.ExpiryMonth.Should().Be(cardCreateModel.ExpiryMonth);
            response.Model.ExpiryYear.Should().Be(cardCreateModel.ExpiryYear);
            response.Model.Bin.Should().Be(cardCreateModel.Number.Substring(0, 6));
            cardCreateModel.Number.Should().EndWith(response.Model.Last4);
            cardCreateModel.BillingDetails.ShouldBeEquivalentTo(response.Model.BillingDetails);
        }

        [Test]
        public void DeleteOnlyCard()
        {
            var customer = CheckoutClient.CustomerService.CreateCustomer(TestHelper.GetCustomerCreateModelWithCard()).Model;
            var customerOnlyCardId = customer.Cards.Data.First().Id;

            var response = CheckoutClient.CardService.DeleteCard(customer.Id, customerOnlyCardId);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);
            response.Error.ErrorCode.Should().BeEquivalentTo("83041");
        }

        [Test]
        public void DeleteSecondaryCard()
        {
            var customer = CheckoutClient.CustomerService.CreateCustomer(TestHelper.GetCustomerCreateModelWithCard()).Model;
            var customerPrimaryCardId = customer.Cards.Data.First().Id;
            var customerSecondaryCardId = CheckoutClient.CardService.CreateCard(customer.Id, TestHelper.GetCardCreateModel(CardProvider.Mastercard)).Model.Id;

            var response = CheckoutClient.CardService.DeleteCard(customer.Id, customerSecondaryCardId);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Message.Should().BeEquivalentTo("Ok");
        }

        [Test]
        public void GetCard()
        {
            var customerCreateModel = TestHelper.GetCustomerCreateModelWithCard();
            var customer = CheckoutClient.CustomerService.CreateCustomer(customerCreateModel).Model;
            var customerCard = customer.Cards.Data.First();

            var response = CheckoutClient.CardService.GetCard(customer.Id, customerCard.Id);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().Be(customerCard.Id);
            response.Model.CustomerId.Should().BeEquivalentTo(customer.Id);
            response.Model.Name.Should().Be(customerCard.Name);
            response.Model.ExpiryMonth.Should().Be(customerCard.ExpiryMonth);
            response.Model.ExpiryYear.Should().Be(customerCard.ExpiryYear);
            response.Model.Bin.Should().Be(customerCard.Bin);
            customerCreateModel.Card.Number.Should().EndWith(response.Model.Last4);
            customerCard.BillingDetails.ShouldBeEquivalentTo(response.Model.BillingDetails);
        }

        [Test]
        public void GetCardList()
        {
            var customer = CheckoutClient.CustomerService.CreateCustomer(TestHelper.GetCustomerCreateModelWithNoCard()).Model;
            var customerCard1 = CheckoutClient.CardService.CreateCard(customer.Id, TestHelper.GetCardCreateModel()).Model;
            var customerCard2 = CheckoutClient.CardService.CreateCard(customer.Id, TestHelper.GetCardCreateModel(CardProvider.Mastercard)).Model;

            var response = CheckoutClient.CardService.GetCardList(customer.Id);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Count.Should().Be(2);
        }

        [Test]
        public void UpdateCard()
        {
            var customer = CheckoutClient.CustomerService.CreateCustomer(TestHelper.GetCustomerCreateModelWithCard()).Model;
            var customerCardId = customer.Cards.Data.First().Id;

            var response = CheckoutClient.CardService.UpdateCard(customer.Id, customerCardId,
                TestHelper.GetCardUpdateModel());

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Message.Should().BeEquivalentTo("Ok");
        }
    }
}