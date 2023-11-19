using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Classes
{
	internal class Cart : ICart
	{
		[Key]
		public int UserId { get; private set; }
		public User? User { get; private set; }
		public List<InventoryProduct> Products { get; }

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
		public void RemoveProduct(IProduct product)
		{
			var index = Products.FindIndex(s => s.Product == product);
			if (index >= 0)
			{
				if (Products[index].Quantity > 1)
				{
					Products[index] = new InventoryProduct(product, Products[index].Quantity - 1);
				}
				else
				{
					Products.RemoveAt(index);
				}
			}
			else
			{
				throw new InvalidOperationException($"{product.Name} are not in this cart");
			}
		}
		public void RemoveProduct(InventoryProduct inv_product)
		{
			var index = Products.FindIndex(s => s.Product == inv_product.Product);
			if (index >= 0)
			{
				if (Products[index].Quantity > inv_product.Quantity)
				{
					Products[index] = new InventoryProduct(inv_product.Product,
														   Products[index].Quantity - inv_product.Quantity);
				}
				else
				{
					Products.RemoveAt(index);
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
