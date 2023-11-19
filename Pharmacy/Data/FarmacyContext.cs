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
		internal DbSet<Cart> Carts => Set<Cart>();
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
			
			//modelBuilder.Entity<Drugs>().ToTable("Drugs");
			//modelBuilder.Entity<Devices>().ToTable("Devices");
			//modelBuilder.Entity<Consumables>().ToTable("Consumables");
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

			modelBuilder.Entity<InventoryProduct>().HasKey(p => new { p.CartId, p.ProductUPC });

			//modelBuilder.Entity<Cart>()
			//	.HasMany(c => c.Products)
			//	.WithOne(ip => ip.Cart)
			//	.HasForeignKey(ip => ip.CartId);

			//// Настройка связи один-к-одному между Cart и Order
			//modelBuilder.Entity<Order>()
			//	.HasOne(o => o.Cart)
			//	.WithOne(c => c.Order)
			//	.HasForeignKey<Order>(o => o.CartId);

			//// Настройка связи один-к-одному или один-ко-многим между InventoryProduct и Product
			//modelBuilder.Entity<InventoryProduct>()
			//	.HasOne(ip => ip.Product)
			//	.WithMany() // Указание на наличие однонаправленной связи, если не требуется навигационное свойство в классе Product
			//	.HasForeignKey(ip => ip.Product.UPC);

			// Пример настройки связи один-к-одному между User и Cart
			modelBuilder.Entity<User>()
				.HasOne(u => u.Cart)
				.WithOne(c => c.User)
				.HasForeignKey<Cart>(c => c.UserId);

			// Пример настройки связи один-ко-многим между User и Order
			modelBuilder.Entity<User>()
				.HasMany(u => u.Orders)
				.WithOne(o => o.User)
				.HasForeignKey(o => o.UserId)
				.OnDelete(DeleteBehavior.Restrict); // Измените каскадное удаление на Restrict

			//Пример настройки связи многие-ко - многим между Cart и Product
			// Это потребует промежуточной сущности, например InventoryProduct
			modelBuilder.Entity<InventoryProduct>()
				.HasKey(ip => new { ip.CartId, ip.ProductUPC });

			modelBuilder.Entity<InventoryProduct>()
				.HasOne(ip => ip.Cart)
				.WithMany(c => c.Products)
				.HasForeignKey(ip => ip.CartId);

			modelBuilder.Entity<InventoryProduct>()
				.HasOne(ip => ip.Product)
				.WithMany(p => p.InventoryProducts)
				.HasForeignKey(ip => ip.ProductUPC);

		}
	}
}
