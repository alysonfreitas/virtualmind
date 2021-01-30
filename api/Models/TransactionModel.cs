namespace API.Models
{
    public class TransactionModel
    {
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public int UserId { get; set; }
    }
}