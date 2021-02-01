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
            try
            {
                // requesting
                RestClient client = new RestClient("https://www.bancoprovincia.com.ar/Principal/Dolar");
                RestRequest request = new RestRequest(Method.GET);
                IRestResponse response = await client.ExecuteAsync(request);
                // parsing
                string[] items = JsonConvert.DeserializeObject<string[]>(response.Content);
                // returning
                CurrencyModel currency = new CurrencyModel(Convert.ToDecimal(items[0]), Convert.ToDecimal(items[1]), this.GetISO(), this.GetName());
                return currency;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override string GetISO()
        {
            return "USD";
        }

        public override string GetName()
        {
            return "Dollar";
        }

        public override decimal GetPurchaseLimit()
        {
            return 200;
        }
    }
}