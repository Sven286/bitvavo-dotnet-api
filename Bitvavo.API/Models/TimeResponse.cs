//
// TimeResponse.cs
//
// Trevi Awater
// 27-01-2022
//
// © Bitvavo.API
//

using Newtonsoft.Json;

namespace Bitvavo.API.Models;

public class TimeResponse
{
    [JsonProperty("time")]
    public long Time { get; set; }
}
