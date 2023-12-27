//
// BitvavoClient.cs
//
// Trevi Awater
// 27-01-2022
//
// © Bitvavo.API
//

using System.Net;
using System.Security.Cryptography;
using System.Text;
using Bitvavo.API.Interfaces;
using Bitvavo.API.Models;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Extensions;

namespace Bitvavo.API;

public class BitvavoClient : IBitvavoClient
{
    private readonly string _apiKey;
    private readonly string _appSecret;
    private readonly int _window;

    private readonly RestClient _restClient;

    private const string BitvavoApiV2Url = "https://api.bitvavo.com/v2/";

    public BitvavoClient(string apiKey, string appSecret, int window = 60000)
    {
        _apiKey = apiKey;
        _appSecret = appSecret;
        _window = window;

        _restClient = new RestClient(BitvavoApiV2Url);
    }

    private static byte[] HashHmac(byte[] key, byte[] message)
    {
        var hash = new HMACSHA256(key);
        return hash.ComputeHash(message);
    }

    private static byte[] StringEncode(string text)
    {
        var encoding = new ASCIIEncoding();
        return encoding.GetBytes(text);
    }

    private static string HashEncode(byte[] hash)
    {
        return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
    }

    private string CreateSignature(long time, Method method, string url, string body)
    {
        var payload = time + method.ToString() + url;

        if (!string.IsNullOrEmpty(body))
            payload += body;

        var key = StringEncode(_appSecret);

        var byteArray = StringEncode(payload);
        var hash = HashHmac(key, byteArray);

        return HashEncode(hash);
    }

    private async Task<RestRequest> AddSignatureToRequest(RestRequest request, string body = null)
    {
        var timeResponse = await GetTime();

        long epoch;

        if (timeResponse.StatusCode != HttpStatusCode.OK)
        {
            // Unable to retrieve time via API, using system.
            epoch = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        else
            epoch = timeResponse.Data.Time;

        var url = _restClient.BuildUri(request);

        var signature = CreateSignature(epoch, request.Method, url.PathAndQuery, body);

        request.AddHeader("Bitvavo-Access-Key", _apiKey);
        request.AddHeader("Bitvavo-Access-Signature", signature);
        request.AddHeader("Bitvavo-Access-Timestamp", epoch.ToString());
        request.AddHeader("Bitvavo-Access-Window", _window.ToString());

        return request;
    }



    private async Task<IRestResponse<TimeResponse>> GetTime()
    {
        var request = new RestRequest("time", Method.GET);

        return await _restClient.ExecuteAsync<TimeResponse>(request);
    }

    public async Task<IRestResponse<TickerPrice>> GetTickerPrice(string market)
    {
        var request = new RestRequest("ticker/price", Method.GET);

        request.AddQueryParameter(nameof(market), market);

        return await _restClient.ExecuteAsync<TickerPrice>(request);
    }

    /// <summary>
    /// Retrieve the list of bids and asks for market. 
    /// That is, the buy and sell orders made by all Bitvavo users in a specific market. 
    /// The orders in the return parameters are sorted by price.
    /// </summary>
    /// <param name="market"></param>
    /// <returns></returns>
    public async Task<IRestResponse<Candle>> GetBook(string market)
    {
        var request = new RestRequest($"{market}/book", Method.GET);

        //request.AddQueryParameter(nameof(market), market);
        request = await AddSignatureToRequest(request);

        return await _restClient.ExecuteAsync<Candle>(request);
    }

    /// <summary>
    /// Personal Trades for your account
    /// </summary>
    /// <param name="market"></param>
    /// <returns></returns>
    public async Task<IRestResponse<List<Trade>>> GetTrades(string market)
    {
        var request = new RestRequest("trades", Method.GET);

        request.AddQueryParameter(nameof(market), market);
        request = await AddSignatureToRequest(request);

        return await _restClient.ExecuteAsync<List<Trade>>(request);
    }

    public async Task<IRestResponse<List<Trade>>> GetPublicTrades(string market, DateTimeOffset? start = null)
    {
        var request = new RestRequest($"{market}/trades", Method.GET);

        //request.AddQueryParameter(nameof(market), market);

        if (start.HasValue)
        {
            request.AddQueryParameter("start", start.Value.ToUnixTimeMilliseconds().ToString());
        }

        request = await AddSignatureToRequest(request);

        return await _restClient.ExecuteAsync<List<Trade>>(request);
    }

    public async Task<IRestResponse<List<Candle>>> GetCandles(string market, CandleInterval interval, DateTimeOffset? start = null, DateTimeOffset? end = null)
    {
        var request = new RestRequest($"{market}/candles", Method.GET);

        var ci = interval.ToString().Substring(1);
        request.AddQueryParameter("interval", ci);

        if (start.HasValue)
        {
            request.AddQueryParameter("start", start.Value.ToUnixTimeMilliseconds().ToString());
        }

        if (end.HasValue)
        {
            request.AddQueryParameter("end", end.Value.ToUnixTimeMilliseconds().ToString());
        }

        request = await AddSignatureToRequest(request);

        var parts = await _restClient.ExecuteAsync<List<List<string>>>(request);

        var result = new RestResponse<List<Candle>>();
        result.Content = parts.Content;
        result.ContentLength = parts.ContentLength;
        result.ContentEncoding = parts.ContentEncoding;
        result.ContentType = parts.ContentType;
        //result.Cookies = parts.Cookies
        result.ErrorException = parts.ErrorException;
        result.ErrorMessage = parts.ErrorMessage;
        //result.IsSuccessful = parts.IsSuccessful;
        result.ProtocolVersion = parts.ProtocolVersion;
        result.RawBytes = parts.RawBytes;
        result.Request = parts.Request;
        result.ResponseStatus = parts.ResponseStatus;
        result.Server = parts.Server;
        result.StatusCode = parts.StatusCode;
        result.StatusDescription = parts.StatusDescription;

        result.Data = new List<Candle>();
        foreach (var item in parts.Data)
        {
            var candle = new Candle(item);
            result.Data.Add(candle);
        }

        return result;
    }


    public async Task<IRestResponse<List<Order>>> GetOpenOrders()
    {
        var request = new RestRequest("ordersOpen", Method.GET);

        request = await AddSignatureToRequest(request);

        return await _restClient.ExecuteAsync<List<Order>>(request);
    }

    public async Task<IRestResponse<List<Balance>>> GetBalances()
    {
        var request = new RestRequest("balance", Method.GET);

        request = await AddSignatureToRequest(request);

        return await _restClient.ExecuteAsync<List<Balance>>(request);
    }

    public async Task<IRestResponse<List<MarketItem>>> GetMarkets(string? market = null)
    {
        var request = new RestRequest("markets", Method.GET);

        if (market != null)
        {
            request.AddQueryParameter("market", market);
        }

        request = await AddSignatureToRequest(request);

        return await _restClient.ExecuteAsync<List<MarketItem>>(request);
    }

    public async Task<IRestResponse<List<Asset>>> GetAssets()
    {
        var request = new RestRequest("assets", Method.GET);

        request = await AddSignatureToRequest(request);

        return await _restClient.ExecuteAsync<List<Asset>>(request);
    }

    public async Task<IRestResponse<Order>> PlaceOrder(Order order)
    {
        var request = new RestRequest("order", Method.POST);

        dynamic payload = new
        {
            market = order.Market,
            side = order.Side,
            orderType = order.OrderType,
            amount = order.Amount
        };

        var body = JsonConvert.SerializeObject(payload);

        request.AddParameter("application/json", body, ParameterType.RequestBody);

        request = await AddSignatureToRequest(request, body);

        return await _restClient.ExecuteAsync<Order>(request);
    }
}
