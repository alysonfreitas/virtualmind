namespace API.Models
{
    public class CurrencyModel
    {
        public decimal SalePrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public string ISO { get; set; }
        public string Name { get; set; }

        public CurrencyModel(decimal salePrice, decimal purchasePrice, string iso, string name)
        {
            this.SalePrice = salePrice;
            this.PurchasePrice = purchasePrice;
            this.ISO = iso;
            this.Name = name;
        }
    }
}