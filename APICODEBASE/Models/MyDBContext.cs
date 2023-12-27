using Microsoft.EntityFrameworkCore;


namespace APICODEBASE.Models
{
    public class MyDBContext:DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {


        }

        public DbSet<Admin> Admin { get; set; }

        public DbSet<Brand> Brand { get; set; }

      
        public DbSet<Category> Category { get; set; }

        public DbSet<Customer> Customer { get; set; }

        public DbSet<Delivery> Delivery { get; set; }

        public DbSet<DeliveryPersonInfo> DeliveryPersonInfo { get; set; }

      

        public DbSet<OrderDetails> OrderDetails { get; set; }

        public DbSet<Payment> Payment { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Variant> Variant { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>().HasKey(d => d.AdminId);

            modelBuilder.Entity<Brand>().HasKey(d => d.BrandId);

            modelBuilder.Entity<DeliveryPersonInfo>().HasKey(d => d.DeliveryPersonId);

            modelBuilder.Entity<Category>().HasKey(d => d.CategoryId);

            modelBuilder.Entity<Customer>().HasKey(d => d.CustomerId);

            modelBuilder.Entity<Delivery>().HasKey(d => d.DeliveryId);

            modelBuilder.Entity<OrderDetails>().HasKey(d => d.OrderId);

            modelBuilder.Entity<Payment>().HasKey(d => d.PaymentId);

            modelBuilder.Entity<Product>().HasKey(d => d.ProductId);

            modelBuilder.Entity<Variant>().HasKey(d => d.VariantId);

        }


    }
}
