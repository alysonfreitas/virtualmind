using System;
using API.Models;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;

namespace API.Repositories
{
    public class DollarRepository : CurrencyRepository
    {
        public override async Task<CurrencyModel> GetCurrency()
        {
            // requesting
            RestClient client = new RestClient("https://www.bancoprovincia.com.ar/Principal/Dolar");
            RestRequest request = new RestRequest(Method.GET);
            IRestResponse response = await client.ExecuteAsync(request);
            // parsing
            string[] items = JsonConvert.DeserializeObject<string[]>(response.Content);
            // returning
            CurrencyModel currency = new CurrencyModel(Convert.ToDecimal(items[0]), Convert.ToDecimal(items[1]), this.GetISO());
            return currency;
        }

        public override string GetISO()
        {
            return "USD";
        }

        public override decimal GetPurchaseLimit()
        {
            return 200;
        }
    }
}