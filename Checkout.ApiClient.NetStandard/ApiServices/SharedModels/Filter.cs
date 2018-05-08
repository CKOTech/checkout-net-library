using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Checkout.ApiServices.SharedModels
{
    public class Filter
    {
        public Action? Action { get; set; }

        public Field? Field { get; set; }

        public Operator? Operator { get; set; }

        public string Value { get; set; }
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Action
    {
        Include,
        Exclude
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Field
    {
        Status,
        Email,
        CardNumber,
        ChargeId,
        TrackId
    }

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Operator
    {
        Equals,
        Contains,
        Begins,
        Ends
    }
}