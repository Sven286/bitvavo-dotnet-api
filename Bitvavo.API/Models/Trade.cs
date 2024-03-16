//
// Trade.cs
//
// Trevi Awater
// 27-01-2022
//
// © Bitvavo.API
//

using Newtonsoft.Json;

namespace Bitvavo.API.Models;

public class Trade
{
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("orderId")]
    public string OrderId { get; set; }

    [JsonProperty("timestamp")]
    public long Timestamp { get; set; }

    [JsonProperty("market")]
    public string Market { get; set; }

    [JsonProperty("side")]
    public Side Side { get; set; }

    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }

    [JsonProperty("taker")]
    public bool Taker { get; set; }

    [JsonProperty("fee")]
    public decimal Fee { get; set; }

    [JsonProperty("feeCurrency")]
    public string FeeCurrency { get; set; }

    [JsonProperty("settled")]
    public bool Settled { get; set; }

    public override string ToString()
    {
        var t = DateTimeOffset.FromUnixTimeMilliseconds(Timestamp);
        return $"{{Trade: {t} {Side} {Amount} {Market} @ {Price} ({Fee} {FeeCurrency} Fee, Taker={Taker}, Settled={Settled}, Id={Id})}}";
    }
}
