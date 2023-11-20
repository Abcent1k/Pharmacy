using Pharmacy.Classes.Products;
using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Classes
{
	internal class User: IUser
	{
		public int UserId { get; }
		public ICollection<Order>? Orders { get; }
		public ICollection<InventoryProduct> Products { get; }
		[MaxLength(30)]
		public string Name { get; private set; }
		[MaxLength(30)]
		public string Surname { get; private set; }

		public User(string name, string surname)
		{
			Name = name;
			Surname = surname;
			Products = new List<InventoryProduct>();
		}
		void AddProduct(InventoryProduct inv_product)
		{
			var index = (new List<InventoryProduct>(Products)).FindIndex(s => s.Product == inv_product.Product);

			if (index >= 0)
			{
				(new List<InventoryProduct>(Products))[index] = new InventoryProduct(inv_product.Product,
					(new List<InventoryProduct>(Products))[index].Quantity + inv_product.Quantity);
			}
			else
			{
				Products.Add(inv_product);
			}
		}
		void AddProduct(Product product)
		{
			var index = (new List<InventoryProduct>(Products)).FindIndex(s => s.Product == product);

			if (index >= 0)
			{
				(new List<InventoryProduct>(Products))[index] = new InventoryProduct(product, (new List<InventoryProduct>(Products))[index].Quantity + 1);
			}
			else
			{
				Products.Add(new InventoryProduct(product, 1));
			}
		}
		public void RemoveProduct(Product product)
		{
			var index = (new List<InventoryProduct>(Products)).FindIndex(s => s.Product == product);
			if (index >= 0)
			{
				if ((new List<InventoryProduct>(Products))[index].Quantity > 1)
				{
					(new List<InventoryProduct>(Products))[index] = new InventoryProduct(product, (new List<InventoryProduct>(Products))[index].Quantity - 1);
				}
				else
				{
					(new List<InventoryProduct>(Products)).RemoveAt(index);
				}
			}
			else
			{
				throw new InvalidOperationException($"{product.Name} are not in this cart");
			}
		}
		public void RemoveProduct(InventoryProduct inv_product)
		{
			var index = (new List<InventoryProduct>(Products)).FindIndex(s => s.Product == inv_product.Product);
			if (index >= 0)
			{
				if ((new List<InventoryProduct>(Products))[index].Quantity > inv_product.Quantity)
				{
					(new List<InventoryProduct>(Products))[index] = new InventoryProduct(inv_product.Product,
														   (new List<InventoryProduct>(Products))[index].Quantity - inv_product.Quantity);
				}
				else
				{
					(new List<InventoryProduct>(Products)).RemoveAt(index);
				}
			}
			else
			{
				throw new InvalidOperationException($"{inv_product.Product.Name} are not in this cart");
			}
		}
		public void ShowCart()
		{
			foreach (var item in Products)
			{
				Console.WriteLine($"Product Name - {item.Product.Name}; " +
								  $"Quantity - {item.Quantity}; Unit price - {item.Product.Price}");
			}
		}
		public void RemoveAll()
		{
			Products.Clear();
		}
	}
}
