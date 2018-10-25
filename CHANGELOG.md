[![Checkout.com](https://cdn.checkout.com/img/checkout-logo-online-payments.jpg)](https://checkout.com/)

# CHANGELOG
Starting with version 2.0.0 this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

<br />

---

<br />

## [Unreleased](#) (yyyy-mm-dd)

<br />

## 2.2.5 (2018-10-25)

### Bug Fixes
- **added** `ConfigureAwait(false)` to prevent a potential deadlock on the async thread

<br />

## 2.2.4 (2018-08-16)

### Features
- **added** method `GetTokenDetails()` / `GetTokenDetailsAsync()` to the Lookups Service

<br />

## 2.2.3 (2018-07-03)

### Features
- **added:** `RecipientDetails` attribute to satisfy [*requirements for financial institutions* regulations by Visa and Mastercard](https://docs.checkout.com/docs/requirements-for-financial-institutions)

<br />

## [2.2.2](https://github.com/checkout/checkout-net-library/tree/2.2.2) (2018-06-27)

### Changes
- **changed:** structure of some class inheritance

<br />

## 2.2.1 (2018-06-21)

### Features
- **added:** attributes to satisfy [*stored card details* regulations by Visa and Mastercard](https://docs.checkout.com/docs/stored-card-details)
  - `cardOnFile`
  - `previousChargeId`
  - `transactionIndicator`

<br />

## 2.2.0 (2018-06-14)

### BREAKING CHANGES
- **removed:** targeting framework **.NET 4.0** for it ...
  - [... only supporting early TLS 1.0](https://blog.pcisecuritystandards.org/are-you-ready-for-30-june-2018-sayin-goodbye-to-ssl-early-tls)
  - [... not supporting asynchronous programming with async and await](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/)

### Features
- **added:** ability to [asynchronously program with **async and await**](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/async/) through additional classes and methods
  - synchronous methods remain available via `ApiClient()`
  - asynchronous methods are added via new Class `ApiClientAsync()`

<br />

## 2.1.0 (2018-06-08)

### Features
- **added:** method to create a Card from a `cardToken`
    ```csharp
    CreateCard(string customerId, string cardToken)
    ```

<br />

## 2.0.2 (2018-05-24)

### BREAKING CHANGES
- **changed:** naming conventions for more consistency
  - ~~`APIClient()`~~ → **`ApiClient()`**
  - ~~`AppSettings()`~~ → **`CheckoutConfiguration()`**

### Changes
- **added:** Interfaces for `ApiClient()`, `ApiHttpClient()` and API Service Classes
  - `IApiClient()`
  - `IApiHttpClient()`
  - `ICardService()`
  - `IChargeService()`
  - `ICustomerService()`
  - `ILookupsService()`
  - `IPayoutsService()`
  - `IRecurringPaymentsService()`
  - `IReportingService()`
  - `ITokenService()`

<br />

## 2.0.1 (2018-05-05)

<br />

## 2.0.0 (2018-05-04)

This release is a port to **.NET Standard 1.3** and closes [#23](https://github.com/checkout/checkout-net-library/issues/23)

### BREAKING CHANGES
- **deprecated:** <a name="security_1"></a>.NET 4.0 for only supporting early TLS 1.0
    > The Payment Card Industry Security Standards Council (PCI SSC) is extending the migration completion date to 30 June 2018 for transitioning from SSL and TLS 1.0 to a secure version of TLS (currently v1.1 or higher)... [more](https://blog.pcisecuritystandards.org/migrating-from-ssl-and-early-tls)

- **added:** requirement for passing `AppSettings` to the `APIClient(AppSettings settings)` constructor instead of configuring `AppSettings` via custom App Configuration files `App.Debug.config` and `App.Release.config`
    ```csharp
    AppSettings settings = new AppSettings()
    {
        SecretKey = "sk_test_{your_secret_key}",
        PublicKey = "pk_test_{your_public_key}",
        MaxResponseContentBufferSize = 10240,
        RequestTimeout = 60,
        DebugMode = true,
        Environment = Checkout.Helpers.Environment.Sandbox
    };

    APIClient CheckoutClient = new APIClient(settings);
    ```

### Features
- **added:** availability of the **Payouts** API Endpoint through the SDK

### Changes
- **removed:** setting `ServicePointManager.SecurityProtocol`

    Framework|TLS 1.2 is default|TLS 1.2 can be added via
    ---|:---:|---
    .NET 4.6+|:heavy_check_mark:|-
    .NET 4.5|:x:|opt-in: `ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;`
    .NET 4.0|:x:|opt-in with .NET 4.5 (or higher) installed: `ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;`
    .NET 3.5|:x:|Microsoft has released a patch for .NET 3.5 that enables support for system-default SSL and TLS versions
    [more](https://mavenlink.zendesk.com/hc/en-us/articles/115007653028-Transport-Layer-Security-TLS)

### Bug Fixes

- **added:** logic to `CustomerService.UpdateCustomer(string identifier, CustomerUpdate requestModel)` to support identifier being an email address

- **added:** ResponseModels in the `ApiServices.RecurringPayments` namespace to handle API Responses returning stringified floats in the value field
  - `ApiServices.RecurringPayments.ResponseModels.BaseResponseRecurringPlan`
  - `ApiServices.RecurringPayments.ResponseModels.ResponsePaymentPlan`
  - `ApiServices.RecurringPayments.ResponseModels.ResponsePaymentPlanCreate`
  - `ApiServices.RecurringPayments.ResponseModels.ResponsePaymentPlanUpdate`

<br />

## [1.3.0.9](https://github.com/checkout/checkout-net-library/tree/1.3.0.9) (2018-01-22)

### Features
- **added:** support for dynamic redirect URLs in Charge Requests

<br />

## [1.3.0.7](https://github.com/checkout/checkout-net-library/tree/1.3.0.7) (2017-10-19)

### Bug Fixes
- **added:** support for Billing Details in Charge with Card Token, closes [#24](https://github.com/checkout/checkout-net-library/issues/24)

### Features
- **added:** support for Attempt Non 3D in Charge Request

<br />

## [1.3.0.6](https://github.com/checkout/checkout-net-library/tree/1.3.0.6) (2016-10-26)

### Features
- **added:** *Bin* property to `ApiServices.Cards.ResponseModels.Card`