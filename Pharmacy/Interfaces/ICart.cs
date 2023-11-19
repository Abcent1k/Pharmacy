using Pharmacy.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Classes.Products;

namespace Pharmacy.Interfaces
{
	internal class InventoryProduct
	{
		public int CartId { get; set; }
		public Cart Cart { get; private set; }
		public Product Product { get; set; }
		public uint ProductUPC { get; set; }
		public uint Quantity { get; set; }
		//public InventoryProduct (Product product, uint quantity, Cart cart)
		//{
		//	Product = product;
		//	Quantity = quantity;
		//	CartId = cart.UserId;
		//}
		public InventoryProduct(uint quantity)
		{
			Quantity = quantity;
		}
		public InventoryProduct(Product product, uint quantity):this(quantity)
		{
			Product = product;
			ProductUPC = product.UPC;
		}
	}
	internal interface ICart
	{
		ICollection<InventoryProduct> Products { get; }
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
		void RemoveProduct(Product product);
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
