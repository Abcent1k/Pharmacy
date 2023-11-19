using Pharmacy.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Interfaces
{
	internal struct InventoryProduct
	{
		public IProduct Product { get; set; }
		public uint Quantity { get; set; }
		public InventoryProduct (IProduct product, uint quantity)
		{
			Product = product;
			Quantity = quantity;
		}
	}
	internal interface ICart
	{
		List<InventoryProduct> Products { get; }
		void AddProduct(InventoryProduct inv_product)
		{
			var index = Products.FindIndex(s => s.Product == inv_product.Product);

			if (index >= 0)
			{
				Products[index] = new InventoryProduct(inv_product.Product, 
													   Products[index].Quantity + inv_product.Quantity);
			}
			else
			{
				Products.Add(inv_product);
			}
		}
		void AddProduct(IProduct product)
		{
			var index = Products.FindIndex(s => s.Product == product);

			if (index >= 0)
			{
				Products[index] = new InventoryProduct(product, Products[index].Quantity + 1);
			}
			else
			{
				Products.Add(new InventoryProduct(product, 1));
			}
		}
		void RemoveProduct(IProduct product);
		void RemoveProduct(InventoryProduct inv_product);
		void RemoveProduct(List<InventoryProduct> products)
		{
			foreach (var inv_product in products)
			{
				RemoveProduct(inv_product);
			}
		}
		void RemoveAll();
		void ShowCart();
	}
}
