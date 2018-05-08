using System.Linq;
using System.Net;
using FluentAssertions;
using NUnit.Framework;
using Tests.Utils;

namespace Tests
{
    [TestFixture(Category = "PayoutsApi")]
    public class PayoutsService : BaseServiceTests
    {
        [Test]
        public void MakePayout()
        {
            // Create Customer with Card
            var customerCreateModel = TestHelper.GetCustomerCreateModelWithCard(CardProvider.Mastercard);
            var customer = CheckoutClient.CustomerService.CreateCustomer(customerCreateModel).Model;
            var customerId = customer.Id;
            var cardId = customer.Cards.Data[0].Id;
            var cardholderName = customer.Cards.Data[0].Name;
            var cardholderFirstName = cardholderName.Split(' ').First();
            var cardholderLastName = cardholderName.Split(' ').Last();

            // Make Payout
            var payoutsCreateModel = TestHelper.GetPayoutModel(cardId, cardholderFirstName, cardholderLastName);
            var payoutResponse = CheckoutClient.PayoutsService.MakePayout(payoutsCreateModel);
            var payout = payoutResponse.Model;

            payoutResponse.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            payout.ResponseCode.Should().Be("10000");
            payout.Status.Should().Be("Authorised");
            payout.ResponseSummary.Should().Be("Approved");
            payout.ResponseDetails.Should().Be("Approved");


        }
    }
}
