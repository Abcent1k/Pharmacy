using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Classes
{
	internal class Cart: ICart
	{
		public List<InventoryProduct> Products { get; }

		public Cart()
		{
			Products = new List<InventoryProduct>();
		}
		public Cart(List<InventoryProduct> products)
		{
			Products = products;
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
	}
}
