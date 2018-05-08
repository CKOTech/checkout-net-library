using System;
using System.Net;
using Checkout.ApiServices.Customers.RequestModels;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    [TestFixture(Category = "CustomersApi")]
    public class CustomersApiTests : BaseServiceTests
    {
        [Test]
        public void CreateCustomerWithCard()
        {
            var customerCreateModel = TestHelper.GetCustomerCreateModelWithCard();
            var response = CheckoutClient.CustomerService.CreateCustomer(customerCreateModel);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().StartWith("cust_");
            customerCreateModel.ShouldBeEquivalentTo(response.Model, options => options.Excluding(x => x.Card));
            customerCreateModel.Card.ShouldBeEquivalentTo(response.Model.Cards.Data[0],
                options => options.Excluding(c => c.Number).Excluding(c => c.Cvv).Excluding(c => c.DefaultCard));
        }

        [Test]
        public void CreateCustomerWithNoCard()
        {
            var customerCreateModel = TestHelper.GetCustomerCreateModelWithNoCard();
            var response = CheckoutClient.CustomerService.CreateCustomer(customerCreateModel);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().StartWith("cust_");
            customerCreateModel.ShouldBeEquivalentTo(response.Model, options => options.Excluding(x => x.Card));
        }

        [Test]
        public void DeleteCustomer()
        {
            var customer =
                CheckoutClient.CustomerService.CreateCustomer(TestHelper.GetCustomerCreateModelWithCard()).Model;

            var response = CheckoutClient.CustomerService.DeleteCustomer(customer.Id);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Message.Should().BeEquivalentTo("Ok");
        }

        [TestCase("Id")]
        [TestCase("Email")]
        public void GetCustomer(string method)
        {
            var customerCreateModel = TestHelper.GetCustomerCreateModelWithCard();
            var customer = CheckoutClient.CustomerService.CreateCustomer(customerCreateModel).Model;
            string identifier = "";

            switch(method)
            {
                case "Id":
                    identifier = customer.Id;
                    break;
                case "Email":
                    identifier = customer.Email;
                    break;
                default:
                    throw new InvalidOperationException("Unknown method for Unit Test GetCustomer(string method). method must be either 'Id' or 'Email'.");
            }

            var response = CheckoutClient.CustomerService.GetCustomer(identifier);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().Be(customer.Id);
            response.Model.Id.Should().StartWith("cust_");
        }

        [Test]
        public void GetCustomerList()
        {
            var startTime = DateTime.UtcNow.AddMinutes(-5); // records for the past 5 minutes

            var customer1 = CheckoutClient.CustomerService.CreateCustomer(TestHelper.GetCustomerCreateModelWithCard());
            var customer2 = CheckoutClient.CustomerService.CreateCustomer(TestHelper.GetCustomerCreateModelWithCard());
            var customer3 = CheckoutClient.CustomerService.CreateCustomer(TestHelper.GetCustomerCreateModelWithCard());
            var customer4 = CheckoutClient.CustomerService.CreateCustomer(TestHelper.GetCustomerCreateModelWithCard());

            var custGetListRequest = new CustomerGetList
            {
                FromDate = startTime,
                ToDate = DateTime.UtcNow
            };

            // Get all customers created
            var response = CheckoutClient.CustomerService.GetCustomerList(custGetListRequest);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Count.Should().BeGreaterOrEqualTo(4);

            // Verify if customers are contained in the customer list regardless of parallel customer creation and sorting
            response.Model.Data.ToString().Should().Contain(customer1.Model.ToString());
            response.Model.Data.ToString().Should().Contain(customer2.Model.ToString());
            response.Model.Data.ToString().Should().Contain(customer3.Model.ToString());
            response.Model.Data.ToString().Should().Contain(customer4.Model.ToString());
        }

        [Test]
        public void UpdateCustomer()
        {
            var customer = CheckoutClient.CustomerService.CreateCustomer(TestHelper.GetCustomerCreateModelWithCard()).Model;
            var customerUpdateModel = TestHelper.GetCustomerUpdateModel();
            var response = CheckoutClient.CustomerService.UpdateCustomer(customer.Id, customerUpdateModel);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Message.Should().BeEquivalentTo("Ok");
        }
    }
}