using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Classes
{

	internal class Order: IOrder
	{
		public delegate void OrderHandler(string message);
		public event OrderHandler? Notify;

		//public uint OrderID { get; }
		public List<InventoryProduct> OrderItems { get; }
		public decimal TotalPrice { get; }
		public DateTime OrderDate { get; private set; }

		public Order(List<InventoryProduct> products)
		{
			OrderItems = products;
			TotalPrice = CalculateTotalPrice();
		}
		public Order(IUser user)
		{
			OrderItems = user.Cart.Products;
			TotalPrice = CalculateTotalPrice();
		}

		public void PlaceOrder(IUser user)
		{
			OrderDate = DateTime.Now;
			Notify?.Invoke($"Замовлення на сумму {TotalPrice} від {OrderDate}");
			user.Cart.RemoveAll();
		}

		private decimal CalculateTotalPrice()
		{
			decimal totalPrice = 0;
			foreach (var inv_product in OrderItems)
			{
				totalPrice += inv_product.Quantity * inv_product.Product.Price;
			}
			return totalPrice;
		}
	}
}
