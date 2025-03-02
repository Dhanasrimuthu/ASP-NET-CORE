using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using storedetail.model.domain;
using storedetail.Model.Domain;


namespace storedetail.Data
{
    public class storedetailDbContext : IdentityDbContext<ApplicationUser>
    {
        public storedetailDbContext(DbContextOptions<storedetailDbContext> options) : base(options)
        {
        }

        public DbSet<Store> Store { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Cart> Cart { get; set; }
        public DbSet<CartItem> CartItem { get; set; }

        /*  protected override void OnModelCreating(ModelBuilder modelBuilder)
          {
              base.OnModelCreating(modelBuilder);

              var store = new List<Store>() {

              new Store()
              {
                  Id = Guid.Parse("59f860be-b440-4453-b040-71bc6c43d85e"),
                  Name="store-A",
                  Description="newstore",
                  userId = Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153")                  
              }
            };
              modelBuilder.Entity<Store>().HasData(store); 
          }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var userRoleId = "ed1d6857-0f21-4b0c-87d5-c97474786df5";
            var adminRoleId = "e0961324-3177-4e44-b48d-78d4a27c04f8";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = userRoleId,
                    ConcurrencyStamp=userRoleId,
                    Name="User",
                    NormalizedName = "User".ToUpper()
                },
                new IdentityRole
                {
                    Id = adminRoleId,
                    ConcurrencyStamp=adminRoleId,
                    Name="Admin",
                    NormalizedName = "Admin".ToUpper()
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
