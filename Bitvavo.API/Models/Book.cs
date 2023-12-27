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

public class Book
{
    [JsonProperty("market")]
    public string Market { get; set; }

    [JsonProperty("nonce")]
    public string Nonce { get; set; }

    [JsonProperty("bids")]
    public List<List<string>> Bids { get; set; }
    [JsonProperty("asks")]
    public List<List<string>> Asks { get; set; }


    public override string ToString()
    {
        return $"{{Book: {Market} {Nonce} (#{Bids.Count} Bids, #{Asks.Count} Bids)}}";
    }
}
