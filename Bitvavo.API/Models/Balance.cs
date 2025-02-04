//
// Balance.cs
//
// Trevi Awater
// 27-01-2022
//
// © Bitvavo.API
//

using Newtonsoft.Json;

namespace Bitvavo.API.Models;

public class Balance
{
    [JsonProperty("symbol")]
    public string Symbol { get; set; }

    [JsonProperty("available")]
    public decimal Available { get; set; }

    [JsonProperty("inOrder")]
    public decimal InOrder { get; set; }

    public override string ToString()
    {
        return $"{{Balance: {Available} {Symbol} (InOrders={InOrder})}}";
    }
}
