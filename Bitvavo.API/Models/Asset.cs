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

public class Asset
{
    //[JsonProperty("market")]
    public string Symbol { get; set; }

    //[JsonProperty("status")]
    public string Name { get; set; }

    public int Decimals { get; set; }

    public AssetStatus DepositStatus { get; set; }

    //[JsonProperty("pricePrecision")]
    //public string PricePrecision { get; set; }

    //[JsonProperty("minOrderInQuoteAsset")]
    //public string MinOrderInQuoteAsset { get; set; }

    //[JsonProperty("minOrderInBaseAsset")]
    //public string MinOrderInBaseAsset { get; set; }

    //[JsonProperty("orderTypes")]
    //public List<string> OrderTypes { get; set; }



    public override string ToString()
    {
        return $"{{Asset:{Symbol.PadRight(5)}: {Name} ({Decimals}, {DepositStatus})}}";
    }
}
