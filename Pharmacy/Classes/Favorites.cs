using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Classes
{
	internal class Favorites:IInventory
	{
		public List<IProduct> Products { get; }

		public Favorites()
		{
			Products = new List<IProduct>();
		}
		public Favorites(List<IProduct> products)
		{
			Products = products;
		}

		public void RemoveProduct(IProduct product)
		{
			if (Products.Contains(product))
			{
				Products.Remove(product);
			}
			else
			{
				throw new InvalidOperationException($"{product.Name} are not in favorites");
			}
		}
		public void RemoveProduct(List<IProduct> products)
		{
			foreach (IProduct product in products)
			{
				if (!Products.Contains(product))
				{
					throw new InvalidOperationException($"{product.Name} are not in favorites");
				}
			}
			foreach (IProduct product in products)
			{
				RemoveProduct(product);
			}
		}
	}
}
