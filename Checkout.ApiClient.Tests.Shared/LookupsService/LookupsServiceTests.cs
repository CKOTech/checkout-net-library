using System.Net;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture(Category = "LookupsApi")]
    public class LookupsServiceTests : BaseServiceTests
    {
        [TestCase("123456", HttpStatusCode.NotFound)]
        [TestCase("44", HttpStatusCode.BadRequest)]
        [TestCase(null, HttpStatusCode.NotFound)]
        [TestCase("465945", HttpStatusCode.OK)]
        public void BinLookup_AssertErrorResponseStatus(string bin, HttpStatusCode code)
        {
            var response = CheckoutClient.LookupsService.GetBinLookup(bin);

            if(response != null)
            {
                response.HttpStatusCode.Should().Be(code);
            }
        }

        [Test]
        public void BinLookup_ReturnsFullBinData()
        {
            var bin = "424242";
            var response = CheckoutClient.LookupsService.GetBinLookup(bin);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Should().NotBeNull();
            response.Model.Bin.Should().Be(bin);
        }

        [Test]
        public void LocalPaymentIsserIdLookup_ReturnsInformationForIdeal()
        {
            var response = CheckoutClient.LookupsService.GetLocalPaymentIssuerIds("lpp_9");

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Should().NotBeNull();
            response.Model.LookupDetails.Should().NotBeEmpty();
        }
    }
}