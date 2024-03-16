//
// Order.cs
//
// Trevi Awater
// 27-01-2022
//
// © Bitvavo.API
//

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Bitvavo.API.Models;

public class Order
{
    [JsonProperty("market")]
    public string Market { get; set; }

    [JsonProperty("side")]
    [JsonConverter(typeof(StringEnumConverter))]
    public Side Side { get; set; }

    [JsonProperty("orderType")]
    [JsonConverter(typeof(StringEnumConverter))]
    public OrderType OrderType { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }

    [JsonProperty("amountQuote")]
    public decimal AmountQuote { get; set; }

    [JsonProperty("timeInForce")]
    public string TimeInForce { get; set; }

    [JsonProperty("selfTradePrevention")]
    public string SelfTradePrevention { get; set; }

    [JsonProperty("postOnly")]
    public bool PostOnly { get; set; }

    [JsonProperty("disableMarketProtection")]
    public bool DisableMarketProtection { get; set; }

    [JsonProperty("responseRequired")]
    public bool ResponseRequired { get; set; }

    public override string ToString()
    {
        return $"{{Order: {Side} {Amount} {Market} (Q={AmountQuote}) @ {Price} T={OrderType}}}";
    }
}
