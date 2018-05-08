# Checkout .NET Standard Library

## Target Frameworks

The library targets the following frameworks: 
* .NET Standard 1.3 or higher
* .NET Framework 4.5
* .NET Framework 4.0

---
## How to use the library

### 1. Installation
In order to use the Checkout .Net Standard Library you have two options:
>1. Either install the library through NuGet by searching for the NuGet package name *Checkout.APIClient* and installing it 
>2. Or download the source code from our [master branch on GitHub](https://github.com/checkout/checkout-net-library/tree/master) and reference it in your solution.

After that add the library namespace `Checkout` in your code like this:   
```csharp
using Checkout;
```

If you get class name conflicts please add a namespace alias as shown below:
```csharp
using CheckoutEnvironment = Checkout.Helpers.Environment;
```

### 2. Configuration
Before initializing a new instance of the `ApiClient(AppSettings settings)` class, you will be required to **specify the settings**.
The `AppSettings` object consists of the following settable parameters:

Parameter|Description|Required|Type|Default Value
:---|---|:---:|---|---
SecretKey|Your SecretKey is provided to you in The Hub - see [where to find it](https://docs.checkout.com/docs/business-level-administration#section-view-api-keys).|true|`string`|`null`
PublicKey|Your PublicKey is provided to you in The Hub - see [where to find it](https://docs.checkout.com/docs/business-level-administration#section-view-api-keys).|true|`string`|`null`
Environment|You may set this to either `Checkout.Helpers.Environment.Sandbox` or `Checkout.Helpers.Environment.Live`.<br /><br />Default is `Checkout.Helpers.Environment.Sandbox`|false|`enum`|`null`
MaxResponseContentBufferSize|Sets the maximum number of bytes to buffer when reading the response.<br /><br />Default is `10240`.|false|`int`|`10240`
RequestTimeout|Set your default number of seconds to wait before the request times out on the ApiHttpClient.<br /><br />Default is `60`.|false|`int`| `60`
DebugMode|If set to `true`, the HttpRequests and HttpResponses will be logged to console. Set this option to `false` when going Live.<br /><br />Default is `false`.|false|`bool`|`false`

E.g. specifying your settings for Sandbox could look like this:
```csharp
AppSettings settings = new AppSettings()
{
	SecretKey = "sk_test_{your_secret_key}",
	PublicKey = "pk_test_{your_public_key}",
	DebugMode = true
};
```

Then, you can pass the settings to the constructor of the `ApiClient` class ...
```csharp
APIClient CheckoutClient = new APIClient(settings);
```
... and you are set up to be using our various API endpoints.

>Congratulations for installing and configuring the Checkout .NET Standard Library! If you want to learn what's next, continue reading about our API endpoints.

---
## Endpoints 
There are various API endpoints that the `APIClient` interacts with.
>Make sure you visit our [Docs](https://docs.checkout.com/docs/api-quickstart) if you want to learn in more detail about the endpoints and how they interact. There is even full [examples for transaction lifecycles](https://docs.checkout.com/docs/integration-options) and more details about our fully proprietory solutions that are worth checking out.

Supporting Docs:
- [Codes](https://docs.checkout.com/docs/codes)

---
## API Methods

Each Endpoint has its own Service and each Service respectively contains methods.

Endpoint|Service
---|---
Cards|`CardService`
Charges|`ChargeService`
Customers|`CustomerService`
Lookups|`LookupsService`
PaymentTokens|`TokenService`
RecurringPayments|`RecurringPaymentsService`
Reporting|`ReportingService`

> After initializing the `APIClient`, you can make any API Calls by simply writing `{APIClient_instance}.{Service}.{Method}`. Every API Call is a RESTful HttpRequest that returns an HttpResponse<T> with a corresponding Generic Type Object. The properties of the Generic Type Object are accessible through the HttpResponse<T\>.Model property.
>
> Have a look at this example:
>```csharp
>// Adding the Checkout namespace
>using Checkout;
>
>// Creating an instance of AppSettings with configurations for Sandbox
>AppSettings sandbox_settings = new AppSettings()
>{
>	SecretKey = "sk_test_{your_secret_key}",
>	PublicKey = "pk_test_{your_public_key}",
>	DebugMode = true
>};
>
>try
>{
>	// Initializing the APIClient using the Sandbox settings
>	APIClient CheckoutClient = new APIClient(sandbox_settings);
>
>	// Making an API Call to find details about the bank with Bank Identification Number (BIN) 465945
>	string bin = "465945";
>	HttpResponse<CountryInfo> response = CheckoutClient.LookupService.GetBinLookup(bin);
>
>	// Any HttpResponse has a public "Model" property that gives access to the underlying properties of the returned Generic Type - here: CountryInfo
>	Console.WriteLine(response.Model.Bin);
>	Console.WriteLine(response.Model.IssuerCountryISO2);
>	Console.WriteLine(response.Model.CountryName);
>	Console.WriteLine(response.Model.BankName);
>	Console.WriteLine(response.Model.CardScheme);
>	Console.WriteLine(response.Model.CardType);
>	Console.WriteLine(response.Model.CardCategory);
>
>	/* Output to the console:
>	"465945"
>	"GB"
>	"United Kingdom"
>	"HSBC BANK PLC"
>	"Visa"
>	"Debit"
>	"Consumer"
>	*/
>}
>catch (Exception e)
>{
>	// ... Handle exception
>}
>```

### Available methods and their respective return types:

### CardService
Method|HttpResponse<{__GenericTypeParameter__}>
---|---:
`CreateCard(string customerId, CardCreate requestModel)`|`Card`
`GetCard(string customerId, string cardId)`|`Card`
`UpdateCard(string customerId, string cardId, CardUpdate requestModel)`|`OkResponse`
`DeleteCard(string customerId, string cardId)`|`OkResponse`
`GetCardList(string customerId)`|`CardList`

<br />
<br />

### ChargeService
Method|HttpResponse<{__GenericTypeParameter__}>
---|---:
`ChargeWithCard(CardCharge requestModel)`|`Charge`
`ChargeWithCardId(CardIdCharge requestModel)`|`Charge`
`ChargeWithCardToken(CardTokenCharge requestModel)`|`Charge`
`ChargeWithDefaultCustomerCard(DefaultCardCharge requestModel)`|`Charge`
`ChargeWithLocalPayment(LocalPaymentCharge requestModel)`|`Charge`
`VoidCharge(string chargeId, ChargeVoid requestModel)`|`Void`
`RefundCharge(string chargeId, ChargeRefund requestModel)`|`Refund`
`CaptureCharge(string chargeId, ChargeCapture requestModel)`|`Capture`
`UpdateCharge(string chargeId, ChargeUpdate requestModel)`|`OkResponse`
`GetCharge(string chargeId)`|`Charge`
`GetChargeHistory(string chargeId)`|`ChargeHistory`
`VerifyCharge(string paymentToken)`|`Charge`

<br />
<br />

### CustomerService
Method|HttpResponse<{__GenericTypeParameter__}>
---|---:
`CreateCustomer(CustomerCreate requestModel)`|`Customer`
`UpdateCustomer(string customerId, CustomerUpdate requestModel)`|`OkResponse`
`DeleteCustomer(string customerId)`|`OkResponse`
`GetCustomer(string identifier)` - identifier being either customerId or customerEmail|`Customer`
`GetCustomerList(CustomerGetList request)`|`CustomerList`

<br />
<br />

### LookupsService
Method|HttpResponse<{__GenericTypeParameter__}>
---|---:
`GetBinLookup(string bin)`|`CountryInfo`
`GetLocalPaymentIssuerIds(string lppId)`|`LocalPaymentData`

<br />
<br />

### PayoutsService
Method|HttpResponse<{__GenericTypeParameter__}>
---|---:
`MakePayout(BasePayout requestModel)`|`Payout`

<br />
<br />

### RecurringPaymentsService
Method|HttpResponse<{__GenericTypeParameter__}>
---|---:
`CreatePaymentPlan(SinglePaymentPlanCreateRequest requestModel)`|`SinglePaymentPlanCreateResponse`
`UpdatePaymentPlan(string planId, PaymentPlanUpdate requestModel)`|`OkResponse`
`CancelPaymentPlan(string planId)`|`OkResponse`
`GetPaymentPlan(string planId)`|`PaymentPlan`
`QueryPaymentPlan(QueryPaymentPlanRequest requestModel)`|`QueryPaymentPlanResponse`
`QueryCustomerPaymentPlan(QueryCustomerPaymentPlanRequest requestModel)`|`QueryCustomerPaymentPlanResponse`
`GetCustomerPaymentPlan(string customerPlanId)`|`CustomerPaymentPlan`
`CancelCustomerPaymentPlan(string customerPlanId)`|`OkResponse`
`UpdateCustomerPaymentPlan(string customerPlanId, CustomerPaymentPlanUpdate requestModel)`|`OkResponse`

<br />
<br />

### ReportingService
Method|HttpResponse<{__GenericTypeParameter__}>
---|---:
`QueryTransaction(QueryRequest requestModel)`|`QueryTransactionResponse`
`QueryChargeback(QueryRequest requestModel)`|`QueryChargebackResponse`

<br />
<br />

### TokenService
Method|HttpResponse<{__GenericTypeParameter__}>
---|---:
`CreatePaymentToken(PaymentTokenCreate requestModel)`|`PaymentToken`
`UpdatePaymentToken(string paymentToken, PaymentTokenUpdate requestModel)`|`OkResponse`
`CreateVisaCheckoutCardToken(VisaCheckoutTokenCreate requestModel)`|`CardTokenResponse`
`GetCardToken(TokenCard requestModel)`|`CardTokenCreate`

<br />
<br />

---

## Examples

Have a look at a few more examples. More detailed information on various Models to be passed in the API Methods are available in our [Merchant API Reference](https://docs.checkout.com/docs/additional-apis). It is assumed that you already have instantiated your `APIClient`.

### Charges

#### Charge with full Card Details (PCI compliant merchants only)
- API Method: `ChargeWithCard(CardCharge requestModel)`
- requires: `CardCharge requestModel`

```csharp
// Create CardCharge requestModel
var cardChargeRequestModel = new CardCharge()
{
	Email = "myEmail@hotmail.com",
	AutoCapture = "Y",
	AutoCapTime = 0,
	Currency = "Usd",
	TrackId = "TRK12345",
	TransactionIndicator = "1",
	CustomerIp = "82.23.168.254",
	Description = "Ipad for Ebs travel",
	Value = "100",
	Card = new CardCreate()
	{
		ExpiryMonth = "06",
		ExpiryYear = "2018",
		Cvv = "100",
		Number = "4242424242424242",
		Name = "Mehmet Ali",
		BillingDetails = new Address()
		{
			AddressLine1 = "Flat 1",
			AddressLine2 = "Glading Fields",
			Postcode = "N16 2BR",
			City = "London",
			State = "Hackney",
			Country = "GB",
			Phone = new Phone()
			{
				CountryCode = "44",
				Number = "203 583 44 55"
			}
		}
	},
	Products = new List<Product>(){
		new Product{ 
			Name = "iPad 3", 
			Price = 100, 
			Quantity = 1, 
			ShippingCost = 10.5, 
			Description = "Gold edition", 
			Image = "http://goofle.com/?id=12345", 
			Sku = "TR12345",
			TrackingUrl = "http://tracket.com?id=123456"
		}
	},
	ShippingDetails = new Address()
	{
		AddressLine1 = "Flat 1",
		AddressLine2 = "Glading Fields",
		Postcode = "N16 2BR",
		City = "London",
		State = "Hackney",
		Country = "GB",
		Phone = new Phone()
		{
			CountryCode = "44",
			Number = "203 583 44 55"
		}
	},
	Metadata = new Dictionary<string, string>()
	{
		{ "extraInformation", "EBS travel" }
	},
	Udf1 = "udf1 string",
	Udf2 = "udf2 string",
	Udf3 = "udf3 string",
	Udf4 = "udf4 string",
	Udf5 = "udf5 string"
};

try
{
	// Submit your request and receive a response from the API Endpoint
	HttpResponse<Charge> response = CheckoutClient.ChargeService.ChargeWithCard(cardChargeRequestModel);

	if (!response.HasError)
	{
		// Access the response object retrieved from the API
		var charge = response.Model;
		// ... Do stuff
	}
	else
	{
		// API has returned an error object. You can access the details in the error property of the response.
		// response.error
		// ... Handle error
	}
}
catch (Exception e)
{
	// ... Handle exception
}
```

### Customers

#### Create customer with card example

```csharp
// Create payload
var customerCreateRequest = new CustomerCreate()
{
	Name = "Miss Matt Quigley",
	Description = "New customer created",
	Email = "myEmail1@hotmail.com",
	Phone =  new Phone()
			{
				CountryCode = "44",
				Number = "203 583 44 55"
			},
	Metadata = new Dictionary<string, string>() { { "Category", "UK customer" } },
	Card =  new CardCreate()
	{
		ExpiryMonth = "06",
		ExpiryYear = "2018",
		Cvv = "100",
		Number = "4242424242424242",
		Name = "Miss Matt Quigley",
		BillingDetails = new Address()
		{
			AddressLine1 = "Flat 1",
			AddressLine2 = "Glading Fields",
			Postcode = "N16 2BR",
			City = "London",
			State = "Hackney",
			Country = "GB",
			Phone = new Phone()
			{
				CountryCode = "44",
				Number = "203 583 44 55"
			}
		}
	}
};

try
{
	// Create APIClient instance with your secret key
	APIClient ckoAPIClient = new APIClient("sk_test_32b9cb39-1cd6-4f86-b750-7069a133667d", Checkout.APIClient.Helpers.Environment.Sandbox);

	// Submit your request and receive an apiResponse
	HttpResponse<Customer> apiResponse = ckoAPIClient.CustomerService.CreateCustomer(customerCreateRequest);

	if (!apiResponse.HasError)
	{
		// Access the response object retrieved from the api
		var customer = apiResponse.Model;
	}
	else
	{
		// Api has returned an error object. You can access the details in the error property of the apiResponse.
		// apiResponse.error
	}
}
catch (Exception e)
{
	//... Handle exception
}
```

#### Cards
##### Create card
```csharp
// Create payload
var cardCreateRequest = new CardCreate()
{
	ExpiryMonth = "06",
	ExpiryYear = "2018",
	Cvv = "100",
	Number = "4242424242424242",
	Name = "Miss Matt Quigley",
	BillingDetails = new Address()
	{
		AddressLine1 = "Flat 1",
		AddressLine2 = "Glading Fields",
		Postcode = "N16 2BR",
		City = "London",
		State = "Hackney",
		Country = "GB",
		Phone = new Phone()
		{
			CountryCode = "44",
			Number = "203 583 44 55"
		}
	}, 
	DefaultCard=true
};

try
{
	// Create APIClient instance with your secret key
	APIClient ckoAPIClient = new APIClient("sk_test_32b9cb39-1cd6-4f86-b750-7069a133667d", Checkout.APIClient.Helpers.Environment.Sandbox);

	// Submit your request and receive an apiResponse
	HttpResponse<Card> apiResponse = ckoAPIClient.CardService.CreateCard("cust_9DECF6A8-DBF7-46F3-927D-BA6C3CE1F501", cardCreateRequest);

	if (!apiResponse.HasError)
	{
		// Access the response object retrieved from the api
		var card = apiResponse.Model;
	}
	else
	{
		// Api has returned an error object. You can access the details in the error property of the apiResponse.
		// apiResponse.error
	}
}
catch (Exception e)
{
	//... Handle exception
}
```

#### Tokens
##### Create payment token example

```csharp
// Create payload
var paymentTokenRequest = new PaymentTokenCreate()
  {
	  Currency = "usd",
	  Value = "100",
	  AutoCapTime = 1,
	  AutoCapture = "N",
	  ChargeMode = 1,
	  Email = "myEmail1@hotmail.com",
	  CustomerIp = "82.23.168.254",
	  TrackId = "TRK12345", 
	  Description = "new payment token",
	  Products = new List<Product>(){
		new Product{ 
			Name="ipad 3", 
			Price=100, 
			Quantity=1, 
			ShippingCost=10.5M, 
			Description="Gold edition", 
			Image="http://goofle.com/?id=12345", 
			Sku="TR12345", TrackingUrl="http://tracket.com?id=123456"
		}
	},
	  ShippingDetails = new Address()
	  {
		  AddressLine1 = "Flat 1",
		  AddressLine2 = "Glading Fields",
		  Postcode = "N16 2BR",
		  City = "London",
		  State = "Hackney",
		  Country = "GB",
		  Phone = new Phone()
		  {
			  CountryCode = "44",
			  Number = "203 583 44 55"
		  }
	  },
	  Metadata = new Dictionary<string, string>() { { "extraInformation", "EBS travel" } },
	  Udf1 = "udf1 string",
	  Udf2 = "udf2 string",
	  Udf3 = "udf3 string",
	  Udf4 = "udf4 string",
	  Udf5 = "udf5 string"
  };

try
{
	// Create APIClient instance with your secret key
	APIClient ckoAPIClient = new APIClient("sk_test_32b9cb39-1cd6-4f86-b750-7069a133667d", Checkout.APIClient.Helpers.Environment.Sandbox);

	// Submit your request and receive an apiResponse
	HttpResponse<PaymentToken> apiResponse = ckoAPIClient.TokenService.CreatePaymentToken(paymentTokenRequest);

	if (!apiResponse.HasError)
	{
		// Access the response object retrieved from the api
		var paymentToken = apiResponse.Model;
	}
	else
	{
		// Api has returned an error object. You can access the details in the error property of the apiResponse.
		// apiResponse.error
	}
}
catch (Exception e)
{
	//... Handle exception
}
```

##### Verify charge example

```csharp
// Create payload
string paymentToken = "pay_tok_e6ef69d3-11b2-473d-bdc0-6b03c8713454";

try
{
	// Create APIClient instance with your secret key
	APIClient ckoAPIClient = new APIClient("sk_test_32b9cb39-1cd6-4f86-b750-7069a133667d", Checkout.APIClient.Helpers.Environment.Sandbox);

	// Submit your request and receive an apiResponse
	HttpResponse<Charge> apiResponse = ckoAPIClient.ChargeService.VerifyCharge(paymentToken);

	if (!apiResponse.HasError)
	{
		// Access the response object retrieved from the api
		var charge = apiResponse.Model;
	}
	else
	{
		// Api has returned an error object. You can access the details in the error property of the apiResponse.
		// apiResponse.error
	}

}
catch (Exception e)
{
	//... Handle exception
}
```
---
## Debug Mode

If you enable the debug mode the HttpRequests and HttpResponses will be logged to console. Set this option to `false` when going Live. Default is `false`.

---
## Build

To build the library from source, .NET Framework 4.6.1 or later is required. 

---
## Tests

All the tests are written with NUnit and reside in the `*.Tests` projects.
