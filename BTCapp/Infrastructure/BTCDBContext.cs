using Microsoft.EntityFrameworkCore;
using BTCapp.Domain;
namespace BTCapp.Infrastructure
{
    public class BTCDBContext : DbContext
    {
        public BTCDBContext(DbContextOptions<BTCDBContext> options)
            : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            var serverVersion = new MySqlServerVersion(new Version(10, 6, 4));
            var connectionString = "server=12.57.1.2;user=root;password='1234';database=BTC_DB";
            dbContextOptionsBuilder.UseMySql(connectionString, serverVersion);
        }
        public DbSet<Price> Prices { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the Price entity
            modelBuilder.Entity<Price>(entity =>
            {
                entity.ToTable("btcprice");
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.TimePoint).IsUnique(); 
                entity.Property(e => e.TimePoint).IsRequired();
                entity.Property(e => e.CloseAmount).IsRequired();
            }); 
        }
    }
    
}
