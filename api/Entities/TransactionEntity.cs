using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace API.Entities
{
    [Table("Transaction")]
    public class TransactionEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal AmountPurchased { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public virtual UserEntity User { get; set; }
    }
}