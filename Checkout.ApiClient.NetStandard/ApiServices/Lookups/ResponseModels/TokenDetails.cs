using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.ApiServices.Lookups.ResponseModels
{
    public class TokenDetails
    {
        public string Token { get; set; }

        public string Bin { get; set; }

        public string Issuer { get; set; }

        public string IssuerCountry { get; set; }

        public string IssuerCountryIso2 { get; set; }

        public string Scheme { get; set; }

        public string Type { get; set; }

        public string Category { get; set; }

        public string ProductId { get; set; }

        public string ProductType { get; set; }

        public string Last4 { get; set; }

        public Dictionary<string, Dictionary<string, string>> Providers { get; set; }
    }
}
