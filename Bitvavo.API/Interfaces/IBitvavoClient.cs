namespace Bitvavo.API.Interfaces;

using RestSharp;
using System.Collections.Generic;
using Bitvavo.API.Models;

public interface IBitvavoClient
{
    Task<IRestResponse<TickerPrice>> GetTickerPrice(string market);

    Task<IRestResponse<List<Trade>>> GetTrades(string market);

    Task<IRestResponse<List<Balance>>> GetBalances();

    Task<IRestResponse<Order>> PlaceOrder(Order order);
}
