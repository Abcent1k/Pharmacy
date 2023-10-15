using Pharmacy.Classes;
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
			var date1 = new DateTime(2023, 7, 20);

			var spray = new Drugs(123456789, "Tantum verde", 112.25m, 1236747675, date1, DrugType.Spray, false);
			var syrop = new Drugs(903456721, "Пектолван", 210.50m, 2106747675, date1, DrugType.Syrop, false);
			var patch = new Consumables(903456721, "ECOplast", 51.50m, 2106747675, date1, ConsumableType.Patch);


			spray.Show();

			Console.WriteLine();

			((IProduct)spray).Show();

			Console.WriteLine();

			((IProductFormat)spray).Show();
			
			Console.WriteLine();

			////////////

			var cart = new Cart(new List<InventoryProduct>() {new InventoryProduct(spray, 2),
															  new InventoryProduct(syrop, 1),
															  new InventoryProduct(patch, 3)
															 });

			var user = new User("Ясос", "Біба", cart);
			var order = new Order(user);

			order.Notify += DisplayMessage;

			order.PlaceOrder(user);

			void DisplayMessage(string message) => Console.WriteLine(message);


			Console.ReadLine();
		}
	}
}
