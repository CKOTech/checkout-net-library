using System.Collections.Generic;

namespace Checkout.ApiServices.Lookups.ResponseModels
{
    public class LocalPaymentData
    {
        public List<LookupDetails> LookupDetails { get; set; }
    }

    public class LookupDetails
    {
        public string TagName { get; set; }

        public List<TagValue> Values { get; set; }
    }

    public class TagValue
    {
        public string Key { get; set; }

        public string Value { get; set; }
    }
}