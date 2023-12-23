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
		private static List<string> names = new List<string> { "Emily", "Michael", "Sofia", "Jacob", "Olivia", "Ethan", "Ava", "Daniel", "Isabella", "Matthew", "Emma", "Lucas", "Mia", "Aiden", "Charlotte"};
		private static List<string> surnames = new List<string> { "Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez", "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson"};
		private static int currentIndex = 0;
		private static Random random = new Random();
		private static Mutex mutex = new Mutex();

		private static List<string> productNames = new List<string>
		{
			"Acetaminophen", "Ibuprofen", "Amoxicillin", "Cephalexin",
			"Clindamycin", "Ciprofloxacin", "Metformin", "Atorvastatin",
			"Simvastatin", "Lisinopril", "Losartan", "Amlodipine",
			"Metoprolol", "Levothyroxine", "Omeprazole", "Lorazepam",
			"Prednisone", "Meloxicam", "Gabapentin", "Sertraline"
		};
		private static object lockObject = new object();

		private static PharmacyContextFactory contextFactory = new PharmacyContextFactory();

		public static void Main(string[] args)
		{
			using (var context = contextFactory.CreateDbContext(new string[] { }))
			{
				context.Database.EnsureDeleted();
				context.Database.EnsureCreated();
			}

			List<Thread> threads = new List<Thread>();

			threads.Add(new Thread(() => GenerateAndSaveUsers()));

			threads.Add(new Thread(() => CreateAndSaveDrugs()));
			threads.Add(new Thread(() => CreateAndSaveConsumables()));
			threads.Add(new Thread(() => CreateAndSaveDevices()));

			foreach (var thread in threads)
				thread.Start();

			foreach (var thread in threads)
				thread.Join();

			var readTasks = new List<Task>
			{
				Task.Run(() => ReadAndDisplayUsers()),
				Task.Run(() => ReadAndDisplayDrugs()),
				Task.Run(() => ReadAndDisplayDevices()),
				Task.Run(() => ReadAndDisplayConsumables())
			};

			Task.WhenAll(readTasks).Wait();
		
			var stop = Console.ReadKey();
		}

		private static void GenerateAndSaveUsers()
		{
			var tasks = new List<Task>();
			for (int i = 0; i < 50; i++)
			{
				tasks.Add(Task.Run(async () =>
				{
					var user = GenerateUser();
					if (user != null)
					{
						await AddUserToDatabaseAsync(user);

						await Console.Out.WriteLineAsync($"Add User: Name - {user.Name}; Surname - {user.Surname}; Time - {DateTime.Now}");
						await Task.Delay(5000);
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

		private static void CreateAndSaveDrugs()
		{
			for (int i = 0; i < 5; i++)
			{
				var drug = new Drugs(
					GenerateRandomUPC(),
					GetUniqueProductName(),
					GenerateRandomPrice(),
					GenerateRandomUPC(),
					GenerateRandomDrugType(),
					false);

				Console.WriteLine($"Add Drug - {drug.Name}; {drug.Price}; Time - {DateTime.Now}");
				Thread.Sleep(500);
				SaveProduct(drug);
			}
		}

		private static void CreateAndSaveDevices()
		{
			for (int i = 0; i < 5; i++)
			{
				var device = new Devices(
					GenerateRandomUPC(),
					GetUniqueProductName(),
					GenerateRandomPrice(),
					GenerateRandomUPC(),
					GenerateRandomDeviceType());

				Console.WriteLine($"Add Device - {device.Name}; {device.Price}; Time - {DateTime.Now}");
				Thread.Sleep(500);
				SaveProduct(device);
			}
		}

		private static void CreateAndSaveConsumables()
		{
			for (int i = 0; i < 5; i++)
			{
				var cons = new Consumables(
					GenerateRandomUPC(),
					GetUniqueProductName(),
					GenerateRandomPrice(),
					GenerateRandomUPC(),
					GenerateRandomConsumableType());

                Console.WriteLine($"Add Consumable - {cons.Name}; {cons.Price}; Time - {DateTime.Now}");
                Thread.Sleep(1000);
				SaveProduct(cons);
			}
		}

		private static string GetUniqueProductName()
		{
			lock (lockObject)
			{
				if (productNames.Count > 0)
				{
					int index = random.Next(productNames.Count);
					string name = productNames[index];
					productNames.RemoveAt(index);
					return name;
				}
				else
				{
					return null;
				}
			}
		}

		private static DrugType GenerateRandomDrugType()
		{
			var values = Enum.GetValues(typeof(DrugType));
			return (DrugType)values.GetValue(random.Next(values.Length));
		}

		private static DeviceType GenerateRandomDeviceType()
		{
			var values = Enum.GetValues(typeof(DeviceType));
			return (DeviceType)values.GetValue(random.Next(values.Length));
		}

		private static ConsumableType GenerateRandomConsumableType()
		{
			var values = Enum.GetValues(typeof(ConsumableType));
			return (ConsumableType)values.GetValue(random.Next(values.Length));
		}

		private static uint GenerateRandomUPC()
		{
			return (uint)random.Next(10000000, 99999999);
		}
		private static decimal GenerateRandomPrice()
		{
			return (decimal)random.Next(50, 800);
		}

		private static void SaveProduct(IProduct product)
		{
			using (var context = contextFactory.CreateDbContext(new string[] { }))
			{
				if (product is Drugs drug)
				{
					context.Drugs.Add(drug);
				}
				else if (product is Consumables consumable)
				{
					context.Consumables.Add(consumable);
				}
				else if (product is Devices device)
				{
					context.Devices.Add(device);
				}

				context.SaveChanges();
			}
		}

		private static async Task ReadAndDisplayUsers()
		{
			using (var context = contextFactory.CreateDbContext(new string[] { }))
			{
				var users = await context.Users.ToListAsync();
				foreach (var user in users)
				{
					await Console.Out.WriteLineAsync($"User: {user.Name} {user.Surname}");
					await Task.Delay(10);
				}
			}
		}

		private static async Task ReadAndDisplayDrugs()
		{
			using (var context = contextFactory.CreateDbContext(new string[] { }))
			{
				var drugs = await context.Drugs.ToListAsync();
				foreach (var drug in drugs)
				{
					await Console.Out.WriteLineAsync($"Drug: {drug.Name} {drug.Price}");
					await Task.Delay(10);
				}
			}
		}

		private static async Task ReadAndDisplayDevices()
		{
			using (var context = contextFactory.CreateDbContext(new string[] { }))
			{
				var devices = await context.Devices.ToListAsync();
				foreach (var device in devices)
				{
					await Console.Out.WriteLineAsync($"Device: {device.Name} {device.Price}");
					await Task.Delay(10);
				}
			}
		}

		private static async Task ReadAndDisplayConsumables()
		{
			using (var context = contextFactory.CreateDbContext(new string[] { }))
			{
				var consumables = await context.Consumables.ToListAsync();
				foreach (var consumable in consumables)
				{
					await Console.Out.WriteLineAsync($"Consumable: {consumable.Name} {consumable.Price}");
					await Task.Delay(10);
				}
			}
		}
	}
}