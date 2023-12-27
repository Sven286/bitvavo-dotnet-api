//
// Trade.cs
//
// Trevi Awater
// 27-01-2022
//
// © Bitvavo.API
//

using System.Collections.Generic;
using System.Globalization;
//using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bitvavo.API.Models;
/*
public class CandleCollection
{
    [JsonConverter(typeof(CandleItemConverter))]
    public List<string> items { get; set; }


    public override string ToString()
    {
        return $"{{CandleCollection:}}";
    }
}

[JsonConverter(typeof(CandleItemConverter))]
*/
public class Candle
{
    public Candle() { }
    public Candle(List<string> data)
    {
        timestamp = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(data[0], CultureInfo.InvariantCulture));
        open = data[1];
        high = data[2];
        low = data[3];
        close = data[4];
        volume = Convert.ToDecimal(data[5], CultureInfo.InvariantCulture);
    }

    public DateTimeOffset timestamp { get; set; }
    public string open { get; set; }
    public string high { get; set; }
    public string low { get; set; }
    public string close { get; set; }
    public decimal volume { get; set; }

    public override string ToString()
    {
        return $"{{Candle:}}";
    }
}

/*
public class CandleItemConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
            return null;
        var jArray = JArray.Load(reader);
        var result = new List<Candle>();
        foreach (var arrayItem in jArray)
        {
            var innerJArray = arrayItem as JArray;
            if (innerJArray?.Count != 6)
                continue;
            result.Add(new Candle
            {
                timestamp = (string)innerJArray[0],
                open = (string)innerJArray[1],
                high = (string)innerJArray[2],
                low = (string)innerJArray[3],
                close = (string)innerJArray[4],
                volume = (string)innerJArray[5],
            });
        }
        return result;
    }
    public override bool CanConvert(Type objectType)
    {
        throw new NotImplementedException();
    }
}
*/