using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Classes.Products;

namespace Pharmacy.Classes
{
	internal class Cart : ICart
	{
		[Key]
		public int UserId { get; private set; }
		public User? User { get; private set; }
		public Order? Order { get; set; } // Навигационное свойство для Order
		public ICollection<InventoryProduct> Products { get; }

		public Cart()
		{
			Products = new List<InventoryProduct>();
		}
		//public Cart(User user, List<InventoryProduct> products)
		//{
		//	Products = products;
		//	this.User = user;
		//}

		public void SetUser(User user)
		{
			User = user;
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
