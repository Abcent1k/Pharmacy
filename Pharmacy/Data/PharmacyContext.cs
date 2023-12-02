using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Pharmacy.Classes;
using Pharmacy.Classes.Products;
using Pharmacy.Interfaces;

namespace Pharmacy.Data
{
	public class PharmacyContext : DbContext
	{
		internal DbSet<User> Users => Set<User>();
		internal DbSet<Order> Orders => Set<Order>();
		internal DbSet<Product> Products => Set<Product>();
		internal DbSet<Drugs> Drugs => Set<Drugs>();
		internal DbSet<Devices> Devices => Set<Devices>();
		internal DbSet<Consumables> Consumables => Set<Consumables>();
		internal DbSet<InventoryProduct> InventoryProducts => Set<InventoryProduct>();

		public PharmacyContext(DbContextOptions<PharmacyContext> options): base(options) 
		{
			//Database.EnsureDeleted();
			//Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.EnableSensitiveDataLogging();
			optionsBuilder.LogTo(Console.WriteLine, new[] { RelationalEventId.CommandExecuted });
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
				.HasForeignKey(o => o.UserId)
				.OnDelete(DeleteBehavior.NoAction); // Disable cascade delete

			// Configure one-to-many relationship between Order and InventoryProduct
			modelBuilder.Entity<Order>()
				.HasMany(o => o.InventoryProducts)
				.WithOne(ip => ip.Order)
				.HasForeignKey(ip => new { ip.OrderId, ip.UserId })
				.OnDelete(DeleteBehavior.NoAction); // Disable cascade delete

			// Configure one-to-many relationship between Product and InventoryProduct and
			modelBuilder.Entity<InventoryProduct>()
				.HasOne(ip => ip.Product)
				.WithMany(p => p.InventoryProducts)
				.HasForeignKey(ip => ip.ProductUPC);

			var user1 = new User("Яна", "Алексюк", 1);
			modelBuilder.Entity<User>().HasData(
				user1,
				new User("Євгеній", "Жидик", 2),
				new User("Ясос", "Біба", 5)
				);

			var consumable1 = new Consumables(1234567890, "Mediprop", 250.5m, 88888888, ConsumableType.Syringe);
			var consumable2 = new Consumables(1973209712, "Medipipa", 204.5m, 44444444, ConsumableType.Needle);
			var device1 = new Devices(986235712, "Інгалятор", 850.25m, 11111111, DeviceType.Inhaler);

			modelBuilder.Entity<Consumables>().HasData(consumable1);
			modelBuilder.Entity<Consumables>().HasData(consumable2);
			modelBuilder.Entity<Devices>().HasData(device1);

			var order1 = new Order { Id = 1, UserId = user1.UserId };
			modelBuilder.Entity<Order>().HasData(order1);

			modelBuilder.Entity<InventoryProduct>().HasData(
				 new InventoryProduct(2)
				 {
					 UserId = user1.UserId,
					 ProductUPC = consumable1.UPC,
					 OrderId = order1.Id
				 }
				);
		}
	}
}
