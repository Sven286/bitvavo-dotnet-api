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

public class MarketItem
{
    //[JsonProperty("market")]
    public string Market { get; set; }

    //[JsonProperty("status")]
    public MarketStatus Status { get; set; }

    [JsonProperty("base")]
    public string Base { get; set; }

    [JsonProperty("quote")]
    public string Quote { get; set; }

    [JsonProperty("pricePrecision")]
    public string PricePrecision { get; set; }

    [JsonProperty("minOrderInQuoteAsset")]
    public string MinOrderInQuoteAsset { get; set; }

    [JsonProperty("minOrderInBaseAsset")]
    public string MinOrderInBaseAsset { get; set; }

    [JsonProperty("orderTypes")]
    public List<string> OrderTypes { get; set; }

    public virtual Asset Asset { get; set; }

    public override string ToString()
    {
        return $"{{Market: {Market} ({Status}) Asset={Asset?.ToString() ?? "<NULL>"}}}";
    }
}
