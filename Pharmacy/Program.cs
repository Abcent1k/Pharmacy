using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Pharmacy
{
	internal class Program
	{
		private static List<string> names = new List<string> { "Emily", "Michael", "Sofia", "Jacob", "Olivia"/*, "Ethan", "Ava", "Daniel", "Isabella", "Matthew", "Emma", "Lucas", "Mia", "Aiden", "Charlotte" */};
		private static List<string> surnames = new List<string> { "Smith", "Johnson", "Williams", "Brown", "Jones"/*, "Garcia", "Miller", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson" */};
		private static int currentIndex = 0;
		private static Random random = new Random();
		private static Mutex mutex = new Mutex();

		private static List<User> users = new List<User>();
		private static List<Thread> threads = new List<Thread>();

		private static PharmacyContextFactory contextFactory = new PharmacyContextFactory();

		public static void Main(string[] args)
		{
			GenerateAndSaveUsers();

			var stop = Console.ReadKey();
		}

		private static void GenerateAndSaveUsers()
		{
			var tasks = new List<Task>();
			for (int i = 0; i < 20; i++)
			{
				tasks.Add(Task.Run(async () =>
				{
					var user = GenerateUser();
					if (user != null)
					{
						await AddUserToDatabaseAsync(user);
						lock (users)
						{
							users.Add(user);
						}

						await Console.Out.WriteLineAsync($"Time - {DateTime.Now}; Name - {user.Name}; Surname - {user.Surname}");
						//await Task.Delay(5000);
					}
				}));
			}

			Task.WhenAll(tasks).Wait();
		}

		private static User GenerateUser()
		{
			mutex.WaitOne();
			if (currentIndex < names.Count * surnames.Count)
			{
				int nameIndex = currentIndex / surnames.Count;
				int surnameIndex = currentIndex % surnames.Count;
				currentIndex++;
				mutex.ReleaseMutex();

				return new User(names[nameIndex], surnames[surnameIndex]);
			}
			else
			{
				mutex.ReleaseMutex();
				return null; // Усі комбінації вже використані
			}
		}

		private static async Task AddUserToDatabaseAsync(User user)
		{			
			using (var context = contextFactory.CreateDbContext(new string[] { }))
			{
				context.Users.Add(user);
				await context.SaveChangesAsync();
			}
		}

		//static void Main(string[] args)
		//{
		//	var u = new User("Саша", "Дядя");

		//	var contextFactory = new PharmacyContextFactory();

		//	////Додавання
		//	//using (var context = contextFactory.CreateDbContext(new string[] { }))
		//	//{
		//	//	context.Users.Add(u);
		//	//	context.SaveChanges();
		//	//}

		//	////Зміна
		//	//using (var context = contextFactory.CreateDbContext(new string[] { }))
		//	//{
		//	//	var userToUpdate = context.Users.Find(1);
		//	//	if (userToUpdate != null)
		//	//	{
		//	//		userToUpdate.Email = "yana@gmail.com";
		//	//		context.SaveChanges();
		//	//	}
		//	//}

		//	////Видалення
		//	//using (var context = contextFactory.CreateDbContext(new string[] { }))
		//	//{
		//	//	var userToDelete = context.Users.Find(6);
		//	//	if (userToDelete != null)
		//	//	{
		//	//		context.Users.Remove(userToDelete);
		//	//		context.SaveChanges();
		//	//	}
		//	//}

		//	////Додавання
		//	//using (var context = contextFactory.CreateDbContext(new string[] { }))
		//	//{
		//	//	var newProduct = new Drugs(23896531, "Ніпіксин", 204.5m, 76548231, DrugType.Drops, false);
		//	//	context.Products.Add(newProduct);


		//	//	var user = context.Users.Find(1);
		//	//	var newOrder = new Order
		//	//	{
		//	//		User = user,
		//	//		UserId = 1,
		//	//	};

		//	//	var newInventoryProduct = new InventoryProduct(user, newProduct, 5, newOrder);
		//	//	context.InventoryProducts.Add(newInventoryProduct);

		//	//	newOrder.PlaceOrder();
		//	//	context.Orders.Add(newOrder);

		//	//	context.SaveChanges();
		//	//}

		//	////Зміна
		//	//using (var context = contextFactory.CreateDbContext(new string[] { }))
		//	//{
		//	//	var productToUpdate = context.Products.Find(23896531u);
		//	//	if (productToUpdate != null)
		//	//	{
		//	//		productToUpdate.Name = "Ніксельпін";
		//	//	}

		//	//	var inventoryToUpdate = context.InventoryProducts.Find(2, 1, 23896531u);
		//	//	if (inventoryToUpdate != null)
		//	//	{
		//	//		inventoryToUpdate.Quantity = 20;
		//	//	}

		//	//	context.SaveChanges();
		//	//}

		//	//using (var context = contextFactory.CreateDbContext(new string[] { }))
		//	//{
		//	//	//Union
		//	//	var drugsAndDevices = context.Drugs.Select(d => d.Name)
		//	//		.Union(context.Devices.Select(d => d.Name));

		//	//	foreach (var d in drugsAndDevices)
		//	//		Console.WriteLine(d);
		//	//	Console.WriteLine();

		//	//	//Except
		//	//	var drugsNotInDevices = context.Drugs.Select(d => d.Name)
		//	//		.Except(context.Devices.Select(d => d.Name));

		//	//	foreach (var d in drugsNotInDevices)
		//	//		Console.WriteLine(d);
		//	//	Console.WriteLine();

		//	//	//Intersect
		//	//	var commonInDrugsAndDevices = context.Drugs.Select(d => d.Price)
		//	//		.Intersect(context.Consumables.Select(d => d.Price));

		//	//	foreach (var d in commonInDrugsAndDevices)
		//	//		Console.WriteLine(d);
		//	//	Console.WriteLine();

		//	//	//Join
		//	//	var orderDetails = context.Orders
		//	//		.Join(context.Users,
		//	//			order => order.UserId,
		//	//			user => user.UserId,
		//	//			(order, user) => new { user.Name, order.OrderDate });

		//	//	foreach (var d in orderDetails)
		//	//		Console.WriteLine(d);
		//	//	Console.WriteLine();

		//	//	//Distinct
		//	//	var uniquePrices = context.Products.Select(d => d.Price).Distinct();

		//	//	foreach (var d in uniquePrices)
		//	//		Console.WriteLine(d);
		//	//	Console.WriteLine();

		//	//	//Group by + Count
		//	//	var productCountsByPrice = context.Products
		//	//		.GroupBy(p => p.Price)
		//	//		.Select(g => new { Price = g.Key, Count = g.Count() });

		//	//	foreach (var d in productCountsByPrice)
		//	//		Console.WriteLine(d);
		//	//	Console.WriteLine();

		//	//	//NoTracking
		//	//	var userAsNoTracking = context.Users.AsNoTracking().First();
		//	//	Console.WriteLine(userAsNoTracking.Email);
		//	//	Console.WriteLine(context.Entry(userAsNoTracking).State);

		//	//	userAsNoTracking.Email = "yanocka@gmail.com";
		//	//	context.Entry(userAsNoTracking).State = EntityState.Modified;
		//	//	Console.WriteLine(context.Entry(userAsNoTracking).State);

		//	//	context.SaveChanges();
		//	//	Console.WriteLine(context.Entry(userAsNoTracking).State);


		//	//	//Збережена процедура
		//	//	var orderId = 1;
		//	//	var orderDetails_ = context.Orders
		//	//		.FromSqlRaw("EXEC GetOrderDetails @orderId", new SqlParameter("@orderId", orderId))
		//	//		.ToList();
		//	//	foreach (var d in orderDetails_)
		//	//		Console.WriteLine(d);
		//	//	Console.WriteLine();

		//	//	//Збережена функція
		//	//	int customerId = 1; // Припустимо, це ID клієнта
		//	//	var orders = context.Orders
		//	//		.FromSqlRaw("SELECT * FROM dbo.GetOrdersByUserId({0})", customerId)
		//	//		.ToList();
		//	//	foreach (var d in orders)
		//	//		Console.WriteLine(d);
		//	//	Console.WriteLine();

		//	//}
		//	//using (var context = contextFactory.CreateDbContext(new string[] { }))
		//	//{
		//	//	//Eager Loading
		//	//	var ordersWithUsers = context.Orders.Include(o => o.User).ToList();
		//	//	foreach (var d in ordersWithUsers)
		//	//	{
		//	//		Console.WriteLine(d);
		//	//		Console.WriteLine(d.User);
		//	//	}
		//	//	Console.WriteLine();
		//	//}
		//	//using (var context = contextFactory.CreateDbContext(new string[] { }))
		//	//{
		//	//	//Explicit Loading
		//	//	var user = context.Users.First();
		//	//	context.Entry(user).Collection(u => u.Orders).Load();
		//	//	Console.WriteLine(user);
		//	//	foreach (var d in user.Orders)
		//	//		Console.WriteLine(d.Id);
		//	//	Console.WriteLine();
		//	//}
		//	//using (var context = contextFactory.CreateDbContext(new string[] { }))
		//	//{
		//	//	//Lazy Loading
		//	//	var users = context.Users.ToList();
		//	//	foreach (var us in users)
		//	//	{
		//	//		Console.WriteLine(us.Name);
		//	//		foreach (var o in us.Orders)
		//	//			Console.WriteLine($"\t{o.Id}");
		//	//	}
		//	//	Console.WriteLine();
		//	//}



		//	using (var context = contextFactory.CreateDbContext(new string[] { }))
		//	{

		//	}

		//		var stop = Console.ReadKey();
		//}
	}
}