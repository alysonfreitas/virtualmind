using System.Runtime.CompilerServices;
using System;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Repositories
{
    public abstract class CurrencyRepository
    {
        public abstract Task<CurrencyModel> GetCurrency();
        public abstract string GetISO();
        public abstract decimal GetPurchaseLimit();
        public static CurrencyRepository GetCurrencyRepositoryByISO(string iso)
        {
            CurrencyRepository currency = Assembly.GetExecutingAssembly().GetTypes()
                       .Where(a => a.GetConstructor(Type.EmptyTypes) != null)
                       .Select(Activator.CreateInstance).OfType<CurrencyRepository>()
                       .FirstOrDefault(currency => currency.GetISO().ToLower() == iso.ToLower());
            return currency;
        }
        public static bool HasCurrencyRepositoryByISO(string iso)
        {
            CurrencyRepository currency = CurrencyRepository.GetCurrencyRepositoryByISO(iso);
            return currency != null;
        }
    }
}
