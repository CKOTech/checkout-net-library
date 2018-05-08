# CHANGELOG
Starting with version 1.4.0 this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

# [Unreleased]

# [2.0.0](https://github.com/checkout/checkout-net-library/tree/2.0.0) (2018-04-xx)

This release is a port to **.NET Standard 1.3** and closes [#23](https://github.com/checkout/checkout-net-library/issues/23)

## BREAKING CHANGES
- **deprecated:** <a name="security_1"></a>.NET 4.0 for only supporting early TLS 1.0
    > The Payment Card Industry Security Standards Council (PCI SSC) is extending the migration completion date to 30 June 2018 for transitioning from SSL and TLS 1.0 to a secure version of TLS (currently v1.1 or higher)... [more](https://blog.pcisecuritystandards.org/migrating-from-ssl-and-early-tls)

## Features
- **added:** availability of the **Payouts** API Endpoint through the SDK

## Changes
- **added:** support for passing `AppSettings` to the `APIClient(AppSettings settings)` constructor instead of configuring `AppSettings` via custom App Configuration files `App.Debug.config` and `App.Release.config`
    ```csharp
    AppSettings settings = new AppSettings()
    {
        SecretKey = "sk_test_32b9cb39-1cd6-4f86-b750-7069a133667d",
        PublicKey = "pk_test_2997d616-471e-48a5-ba86-c775ed3ac38a",
        MaxResponseContentBufferSize = 10240,
        RequestTimeout = 60,
        DebugMode = true,
        Environment = Checkout.Helpers.Environment.Sandbox
    };

    CheckoutClient = new APIClient(settings);
    ```
- **removed:** setting `ServicePointManager.SecurityProtocol`

    Framework|TLS 1.2 is default|TLS 1.2 can be added via
    ---|:---:|---
    .NET 4.6+|:heavy_check_mark:|-
    .NET 4.5|:x:|opt-in: `ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;`
    .NET 4.0|:x:|opt-in with .NET 4.5 (or higher) installed: `ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;`
    .NET 3.5|:x:|Microsoft has released a patch for .NET 3.5 that enables support for system-default SSL and TLS versions
    [more](https://mavenlink.zendesk.com/hc/en-us/articles/115007653028-Transport-Layer-Security-TLS)

## Bug Fixes

- **added:** logic to `CustomerService.UpdateCustomer(string identifier, CustomerUpdate requestModel)` to support identifier being an email address

- **added:** ResponseModels in the `ApiServices.RecurringPayments` namespace to handle API Responses returning stringified floats in the value field
  - `ApiServices.RecurringPayments.ResponseModels.BaseResponseRecurringPlan`
  - `ApiServices.RecurringPayments.ResponseModels.ResponsePaymentPlan`
  - `ApiServices.RecurringPayments.ResponseModels.ResponsePaymentPlanCreate`
  - `ApiServices.RecurringPayments.ResponseModels.ResponsePaymentPlanUpdate`


# [1.3.0.9](https://github.com/checkout/checkout-net-library/tree/1.3.0.9) (2018-01-22)

## Features
- **added:** support for dynamic redirect URLs in Charge Requests

# [1.3.0.7](https://github.com/checkout/checkout-net-library/tree/1.3.0.7) (2017-10-19)

## Bug Fixes
- **added:** support for Billing Details in Charge with Card Token, closes [#24](https://github.com/checkout/checkout-net-library/issues/24)

## Features
- **added:** support for Attempt Non 3D in Charge Request

# [1.3.0.6](https://github.com/checkout/checkout-net-library/tree/1.3.0.6) (2016-10-26)

## Features
- **added:** *Bin* property to `ApiServices.Cards.ResponseModels.Card`