using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Checkout.ApiServices.SharedModels
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SortColumn
    {
        Date,
        Id,
        OriginId,
        Name,
        Email,
        Status,
        Type,
        Amount,
        Scheme,
        ResponseCode,
        Currency,
        LiveMode,
        BusinessName,
        ChannelName,
        TrackId
    }
}
