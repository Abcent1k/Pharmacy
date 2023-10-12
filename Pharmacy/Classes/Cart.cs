using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Classes
{
	internal class Cart:IInventory
	{
		public List<IProduct> Products { get; }

		public Cart()
		{
			Products = new List<IProduct>();
		}
		public Cart(List<IProduct> products)
		{
			Products = products;
		}
		public void AddProduct(IProduct product)
		{
			Products.Add(product);
		}
		public void RemoveProduct(IProduct product)
		{
			if (Products.Contains(product))
			{
				Products.Remove(product);
			}
			else
			{
				throw new InvalidOperationException($"{product.Name} are not in this cart");
			}
		}
		public void RemoveProduct(List<IProduct> products)
		{
			foreach (IProduct product in products)
			{
				if (!Products.Contains(product))
				{
					throw new InvalidOperationException($"{product.Name} are not in this cart");
				}
			}
			foreach (IProduct product in products)
			{
				RemoveProduct(product);
			}
		}
		public void RemoveAll()
		{
			foreach (IProduct product in this.Products)
			{
				RemoveProduct(product);
			}
		}
	}
}
