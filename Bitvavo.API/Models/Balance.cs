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
    public double Available { get; set; }

    [JsonProperty("inOrder")]
    public double InOrder { get; set; }
}
