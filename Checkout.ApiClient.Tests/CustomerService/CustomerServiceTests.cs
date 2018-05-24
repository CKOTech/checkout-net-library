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

        [Test]
        public void GetCustomerById()
        {
            var customerCreateModel = TestHelper.GetCustomerCreateModelWithCard();
            var customer = CheckoutClient.CustomerService.CreateCustomer(customerCreateModel).Model;

            var response = CheckoutClient.CustomerService.GetCustomerById(customer.Id);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().Be(customer.Id);
            response.Model.Id.Should().StartWith("cust_");
            customer.ShouldBeEquivalentTo(response.Model);
        }

        [Test]
        public void GetCustomerByEmail()
        {
            var customerCreateModel = TestHelper.GetCustomerCreateModelWithCard();
            var customer = CheckoutClient.CustomerService.CreateCustomer(customerCreateModel).Model;

            var response = CheckoutClient.CustomerService.GetCustomerByEmail(customer.Email);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Id.Should().Be(customer.Id);
            response.Model.Id.Should().StartWith("cust_");
            customer.ShouldBeEquivalentTo(response.Model);
        }

        [Test]
        public void GetCustomerList()
        {
            var startTime = DateTime.UtcNow.AddHours(-1); // records for the past hour

            var customer1 = CheckoutClient.CustomerService.CreateCustomer(TestHelper.GetCustomerCreateModelWithCard());
            var customer2 = CheckoutClient.CustomerService.CreateCustomer(TestHelper.GetCustomerCreateModelWithCard());
            var customer3 = CheckoutClient.CustomerService.CreateCustomer(TestHelper.GetCustomerCreateModelWithCard());
            var customer4 = CheckoutClient.CustomerService.CreateCustomer(TestHelper.GetCustomerCreateModelWithCard());

            var custGetListRequest = new CustomerGetList
            {
                FromDate = startTime,
                ToDate = DateTime.UtcNow
            };

            //Get all customers created
            var response = CheckoutClient.CustomerService.GetCustomerList(custGetListRequest);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Count.Should().BeGreaterOrEqualTo(4);

            response.Model.Data[0].Id.Should().Be(customer4.Model.Id);
            response.Model.Data[1].Id.Should().Be(customer3.Model.Id);
            response.Model.Data[2].Id.Should().Be(customer2.Model.Id);
            response.Model.Data[3].Id.Should().Be(customer1.Model.Id);
        }

        [Test]
        public void UpdateCustomer()
        {
            var customer =
                CheckoutClient.CustomerService.CreateCustomer(TestHelper.GetCustomerCreateModelWithCard()).Model;

            var customerUpdateModel = TestHelper.GetCustomerUpdateModel();
            var response = CheckoutClient.CustomerService.UpdateCustomer(customer.Id, customerUpdateModel);

            response.Should().NotBeNull();
            response.HttpStatusCode.Should().Be(HttpStatusCode.OK);
            response.Model.Message.Should().BeEquivalentTo("Ok");
        }
    }
}