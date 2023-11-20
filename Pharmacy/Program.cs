using Microsoft.Extensions.Configuration;
using Pharmacy.Classes;
using Pharmacy.Classes.Products;
using Pharmacy.Data;
using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var u = new User("Саша", "Дядя");

			var contextFactory = new PharmacyContextFactory();

			//Додавання
			using (var context = contextFactory.CreateDbContext(new string[] { }))
			{
				context.Users.Add(u);
				context.SaveChanges();
			}

			//Зміна
			using (var context = contextFactory.CreateDbContext(new string[] { }))
			{
				var userToUpdate = context.Users.Find(1);
				if (userToUpdate != null)
				{
					userToUpdate.Email = "yana@gmail.com";
					context.SaveChanges();
				}
			}

			//Видалення
			using (var context = contextFactory.CreateDbContext(new string[] { }))
			{
				var userToDelete = context.Users.Find(6);
				if (userToDelete != null)
				{
					context.Users.Remove(userToDelete);
					context.SaveChanges();
				}
			}

			//Додавання
			using (var context = contextFactory.CreateDbContext(new string[] { }))
			{
				var newProduct = new Drugs(23896531,"Ніпіксин", 204.5m, 76548231, DrugType.Drops, false);
				context.Products.Add(newProduct);


				var user = context.Users.Find(1);
				var newOrder = new Order
				{
					User = user,
					UserId = 1,
				};

				var newInventoryProduct = new InventoryProduct(user, newProduct, 5, newOrder);
				context.InventoryProducts.Add(newInventoryProduct);

				newOrder.PlaceOrder();
				context.Orders.Add(newOrder);

				context.SaveChanges();
			}

			//Зміна
			using (var context = contextFactory.CreateDbContext(new string[] { }))
			{
				var productToUpdate = context.Products.Find(23896531u);
				if (productToUpdate != null)
				{
					productToUpdate.Name = "Ніксельпін";
				}

				var inventoryToUpdate = context.InventoryProducts.Find(2, 1, 23896531u);
				if (inventoryToUpdate != null)
				{
					inventoryToUpdate.Quantity = 20;
				}

				context.SaveChanges();
			}

			//var stop = Console.ReadKey();
		}
	}
}