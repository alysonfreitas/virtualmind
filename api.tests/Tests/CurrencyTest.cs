using Microsoft.VisualStudio.TestTools.UnitTesting;
using API.Repositories;
using API.Models;
using System.Threading.Tasks;

namespace API.Tests
{
    [TestClass]
    public class CurrencyTest
    {
        [DataTestMethod]
        [DataRow("USD")]
        [DataRow("BRL")]
        [DataRow("CAD")]
        public void TestCurrencyRepository(string iso)
        {
            CurrencyRepository currency = CurrencyRepository.GetCurrencyRepositoryByISO(iso);
            if (!CurrencyRepository.HasCurrencyRepositoryByISO(iso))
            {
                Assert.IsNull(currency, "currency is null due not implemented CurrencyRepository");
                return;
            }
            string value = currency.GetISO();
            Assert.AreEqual(value, iso, $"currency.GetISO() is returning: {value} and expected value is: {iso}");
        }

        [TestMethod]
        public void TestNotExistingCurrency()
        {
            string iso = "USDT";
            CurrencyRepository currency = CurrencyRepository.GetCurrencyRepositoryByISO(iso);
            Assert.IsNull(currency, "currency should be null due not implemented repositories");
        }

        [TestMethod]
        public void TestExistingCurrency()
        {
            string iso = "USD";
            CurrencyRepository currency = CurrencyRepository.GetCurrencyRepositoryByISO(iso);
            Assert.IsNotNull(currency, "currency should not be null due implemented repositories");
        }

        [TestMethod]
        public async Task TestGetValueUSD()
        {
            string iso = "USD";
            CurrencyRepository currency = CurrencyRepository.GetCurrencyRepositoryByISO(iso);
            CurrencyModel model = await currency.GetCurrency();
            Assert.IsNotNull(model, "model should not be null");
            Assert.AreEqual(typeof(CurrencyModel), model.GetType(), "model must to be type of CurrencyModel");
            Assert.AreEqual(typeof(decimal), model.PurchasePrice.GetType(), "PurchasePrice must to be type of decimal");
            Assert.AreEqual(typeof(decimal), model.SalePrice.GetType(), "SalePrice must to be type of decimal");
            Assert.IsNotNull(model.PurchasePrice, "currency should not be null due implemented repositories");
        }
    }
}