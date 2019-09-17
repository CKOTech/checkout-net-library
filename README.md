[![Checkout.com](https://cdn.checkout.com/img/checkout-logo-online-payments.jpg)](https://checkout.com/)

>**:heavy_exclamation_mark: This library points to Checkout.com's classic API. Please refer to the [latest .NET SDK](https://github.com/checkout/checkout-sdk-net) instead to benefit from the updated API**

# Checkout .NET Standard Library
[![Build Status](https://travis-ci.org/philippe-leonhardt-cko/checkout-net-library.svg?branch=master)](https://travis-ci.org/philippe-leonhardt-cko/checkout-net-library)&nbsp;[![NuGet](https://img.shields.io/nuget/v/Checkout.ApiClient.svg)](https://www.nuget.org/packages/Checkout.ApiClient)

[Checkout.com](https://checkout.com/) is a software platform that has integrated 100% of the value chain to create payment infrastructures that truly make a difference.

<br />

## Target Frameworks

The library targets the following frameworks: 
- .NET Standard 1.3 or higher (that includes .NET Core)
- .NET Framework 4.5

<br />

## Installation
In order to use the Checkout.com .NET Standard Library you have two installation options:
>1. Either install the library through NuGet by searching for the NuGet package name [*Checkout.ApiClient*](https://www.nuget.org/packages/Checkout.APIClient/) and installing it;
>2. Or download the source code from our [master branch on GitHub](https://github.com/checkout/checkout-net-library/tree/master) and reference it in your solution.

<br />

## Initial Setup

### Pre Requirements
- [![PCI Logo](/img/pci_logo.png (PCI Security Standard Council \(R\)))](https://www.pcisecuritystandards.org/) It is a requirement that all Checkout.com merchants validate their *PCI DSS* (**P**ayment **C**ard **I**ndustry **D**ata **S**ecurity **S**tandard) compliance annually. Details about your required level of compliance depend on the Checkout.com solution you want to use. [Read on in our Docs](https://docs.checkout.com/docs/pci-compliance#section-validate-your-pci-compliance) for the details. The [*PCI Security Standard Council*](https://www.pcisecuritystandards.org/) maintains, evolves and promotes the *PCI DSS*.
- Before using the *Checkout.ApiClient* SDK, you require your *Secret Key* that you can get [here](https://docs.checkout.com/docs/business-level-administration#section-view-api-keys) from your Merchant Account a.k.a *The Hub*;
  > If you do not have access to *The Hub* yet, simply go to https://checkout.com and click the "Get Sandbox" button to create a Sandbox Account.
- You have the *Checkout.ApiClient* SDK installed either from [NuGet](https://www.nuget.org/packages/Checkout.APIClient/) or from our [master branch on GitHub](https://github.com/checkout/checkout-net-library/tree/master) and you have referenced it in your project.

### Adding the SDK to your Solution

Add the library namespace `Checkout` to your solution like this:   
```csharp
using Checkout;
```

If you get class name conflicts please add a namespace alias as shown below:
```csharp
using CheckoutEnvironment = Checkout.Helpers.Environment;
```

<br />

## Configuration

Once you are done with the [Initial Setup](#initial-setup), you are only a few lines of code away from tapping into the power of Checkout.com.
1. Create a `CheckoutConfiguration` object with your **Secret Key** obtained from *The Hub*
2. Initialize the `ApiClient` by passing it the `CheckoutConfiguration` object

![Initialize the ApiClient](/img/init_ApiClient.gif)

<br />

Here's the code for the demo from above:

```csharp
// Adding the Checkout namespace
using Checkout;

namespace Tests
{
	public class BaseServiceTests
	{
		protected ApiClient CheckoutClient;

		public void Init()
		{
			// Creating an instance of CheckoutConfiguration with configurations for Sandbox
			CheckoutConfiguration configuration = new CheckoutConfiguration()
			{
				SecretKey = "sk_test_{your_secret_key}",
				DebugMode = true
			};
			// Initializing the ApiClient using the Sandbox configuration
			CheckoutClient = new ApiClient(configuration);
		}
	}
}
```

<br />

> Congratulations for installing and configuring the *Checkout .NET Standard Library!* :tada:  
From here on you may explore all **Services** and their **Methods** in our [Wiki](https://github.com/philippe-leonhardt-cko/checkout-net-library/wiki).

<br />

## How to use the SDK

After initializing the ApiClient, you can make any API Calls by simply writing `{ApiClient_instance}.{Service}.{Method}`.

e.g.:

```csharp
...
// Making an API Call to find details about the bank with Bank Identification Number (BIN) 465945
string bin = "465945";
var response = CheckoutClient.LookupService.GetBinLookup(bin);
...
```

> The full example is available on our [How to use the SDK Wiki entry](https://github.com/philippe-leonhardt-cko/checkout-net-library/wiki/Endpoints#how-to-use-the-sdk).

<br />

## Going Live

Once you are set with your integration on *Sandbox* you are ready to switch it to *Live*.

1. Contact your **Account Manager** to kick-off the switch from *Sandbox* to *Live*.
    - If you are not already in contact with an **Account Manager**, you may contact our [Sales Team](mailto:sales@checkout.com).
2. Provide the necessary **Documentation** for your onboarding. That includes the [**PCI SAQ**](https://docs.checkout.com/docs/pci-compliance#section-validate-your-pci-compliance) (**P**ayment **C**ard **I**ndustry **S**elf-**A**ssessment **Q**uestionnaire).
3. You will be provided with access to [*The Hub*](https://hub.checkout.com/login) where you can find your [**Live Secret Key**](https://docs.checkout.com/docs/business-level-administration#section-view-api-keys). Place this **Live Secret Key** in your solution and you are ready to go live!
    > Remember to ensure that `DebugMode` is no longer set to `true` when going live.

<br />

## Reference 

You can find our complete Documentation [here](http://docs.checkout.com/ "here").

### Useful URIs
- [Codes](https://docs.checkout.com/docs/codes)
- [Test Credit Card Numbers](https://docs.checkout.com/docs/testing#section-test-card-numbers)

---

If you would like to get an account manager, please contact us at sales@checkout.com  
For help during the integration process you can contact us at integration@checkout.com  
For support, you can contact us at support@checkout.com

*Checkout.com is authorised and regulated as a Payment institution by the UK Financial Conduct Authority.*
