using Microsoft.EntityFrameworkCore;

namespace API.Entities
{
    public class EntityContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }
        public EntityContext(DbContextOptions<EntityContext> options) : base(options)
        {
        }
    }
}