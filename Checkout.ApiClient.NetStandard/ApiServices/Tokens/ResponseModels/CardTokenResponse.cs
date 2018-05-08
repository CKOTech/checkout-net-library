using System;
using Checkout.ApiServices.Lookups.ResponseModels;

namespace Checkout.ApiServices.Tokens.ResponseModels
{
    public class CardTokenResponse
    {
        public string Id { get; set; }

        public bool LiveMode { get; set; }

        public DateTime Created { get; set; }

        public bool Used { get; set; }

        public CountryInfo BinData { get; set; }
    }
}
