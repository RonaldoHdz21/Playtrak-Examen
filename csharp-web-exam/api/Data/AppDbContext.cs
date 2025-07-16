using api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;



namespace api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Balance> Balances { get; set; }
        public DbSet<BalanceHistory> BalancesHistory { get; set; }
        public DbSet<TypeClient> TypesClients { get; set; }


        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Client>()
                .HasOne(c => c.typeClient)
                .WithMany()
                .HasForeignKey(c => c.Type);

            modelBuilder.Entity<TypeClient>().HasData(
                new TypeClient { Id = 1, Description = "Regular" },
                new TypeClient { Id = 2, Description = "VIP" }
            );
        }   

        

    }
}
