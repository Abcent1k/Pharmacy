using Pharmacy.Classes.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Classes
{
	internal class InventoryProduct
	{
		public int OrderId { get; set; }
		public Order Order { get; set; }
		public int UserId { get; set; }
		public User User { get; private set; }
		public Product Product { get; set; }
		public uint ProductUPC { get; set; }
		public int Quantity { get; set; }

		public InventoryProduct(int quantity)
		{
			Quantity = quantity;
		}
		public InventoryProduct(Product product, int quantity) : this(quantity)
		{
			Product = product;
			ProductUPC = product.UPC;
		}
	}
}
