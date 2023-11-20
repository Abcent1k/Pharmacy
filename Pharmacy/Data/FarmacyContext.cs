using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pharmacy.Classes;
using Pharmacy.Classes.Products;
using Pharmacy.Interfaces;

namespace Pharmacy.Data
{
	public class FarmacyContext : DbContext
	{
		internal DbSet<User> Users => Set<User>();
		internal DbSet<Order> Orders => Set<Order>();
		internal DbSet<Product> Products => Set<Product>();
		internal DbSet<Drugs> Drugs => Set<Drugs>();
		internal DbSet<Devices> Devices => Set<Devices>();
		internal DbSet<Consumables> Consumables => Set<Consumables>();
		internal DbSet<InventoryProduct> InventoryProducts => Set<InventoryProduct>();

		public FarmacyContext()
		{
			Database.EnsureDeleted();
			Database.EnsureCreated();
		}


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			var builder = new ConfigurationBuilder();
			builder.SetBasePath(Directory.GetCurrentDirectory());
			builder.AddJsonFile("appsettings.json");
			var config = builder.Build();
			string connectionString = config.GetConnectionString("DefaultConnection");
			optionsBuilder.UseSqlServer(connectionString);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>().HasKey(u => u.UserId);

			modelBuilder.Entity<Product>().Property(p => p.Price).HasPrecision(18, 2);
			modelBuilder.Entity<Product>().ToTable(t => t.HasCheckConstraint("EDRPOU", "LEN(EDRPOU) = 8"));
			
			modelBuilder.Entity<Consumables>()
				.Property(c => c.ExpirationDate)
				.HasDefaultValue(DateTime.Now.AddYears(5));
			modelBuilder.Entity<Drugs>()
				.Property(c => c.ExpirationDate)
				.HasDefaultValue(DateTime.Now.AddYears(1));
			modelBuilder.Entity<Drugs>()
				.Property(c => c.NeedRecipe)
				.HasDefaultValue(false);

			modelBuilder.Entity<Drugs>().Property(p => p.DrugType).HasConversion<string>();
			modelBuilder.Entity<Consumables>().Property(p => p.ConsumableType).HasConversion<string>();
			modelBuilder.Entity<Devices>().Property(p => p.DeviceType).HasConversion<string>();

			modelBuilder.Entity<Order>().HasKey(o => new { o.Id, o.UserId });
			modelBuilder.Entity<Order>().Property(o => o.TotalPrice).HasPrecision(18, 2);

			modelBuilder.Entity<InventoryProduct>().HasKey(p => new { p.OrderId, p.UserId, p.ProductUPC });

			// Configure TPH inheritance for Product and its subclasses
			modelBuilder.Entity<Product>()
				.HasDiscriminator<string>("ProductType")
				.HasValue<Consumables>("Consumables")
				.HasValue<Devices>("Devices")
				.HasValue<Drugs>("Drugs");

			// Configure one-to-many relationship between User and Order
			modelBuilder.Entity<User>()
				.HasMany(u => u.Orders)
				.WithOne(o => o.User)
				.HasForeignKey(o => o.UserId) // Assuming Order has a UserId foreign key
				.OnDelete(DeleteBehavior.NoAction); // Disable cascade delete

			// Configure one-to-many relationship between Order and InventoryProduct
			modelBuilder.Entity<Order>()
				.HasMany(o => o.InventoryProducts) // Assuming Order has a collection of InventoryProduct
				.WithOne(ip => ip.Order) // Assuming InventoryProduct has a navigation property to Order
				.HasForeignKey(ip => new { ip.OrderId, ip.UserId }) // Assuming InventoryProduct has an OrderId foreign key
				.OnDelete(DeleteBehavior.NoAction); // Disable cascade delete

			// Configure many-to-one relationship between InventoryProduct and Product
			modelBuilder.Entity<InventoryProduct>()
				.HasOne(ip => ip.Product)
				.WithMany(p => p.InventoryProducts)
				.HasForeignKey(ip => ip.ProductUPC); // Assuming InventoryProduct has a ProductId foreign key

		}
	}
}
