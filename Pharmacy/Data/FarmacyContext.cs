using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pharmacy.Classes;
using Pharmacy.Classes.Products;

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

			modelBuilder.Entity<Order>()
				.HasOne(o => o.User)
				.WithMany(u => u.Orders)
				.HasForeignKey(o => o.UserId);

			modelBuilder.Entity<Cart>()
				.HasOne(c => c.User)
				.WithOne(u => u.Cart)
				.HasForeignKey<Cart>(c => c.UserId);
		}
	}
}
