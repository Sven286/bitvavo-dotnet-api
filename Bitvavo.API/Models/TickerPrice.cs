//
// TickerPrice.cs
//
// Trevi Awater
// 27-01-2022
//
// © Bitvavo.API
//

using Newtonsoft.Json;

namespace Bitvavo.API.Models;

public class TickerPrice
{
    [JsonProperty("market")]
    public string Market { get; set; }

    [JsonProperty("price")]
    public decimal Price { get; set; }

    public override string ToString()
    {
        return $"{{TickerPrice: {Market} {Price}}}";
    }
}
