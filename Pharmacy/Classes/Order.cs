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
		//public uint OrderID { get; }
		public List<IProduct> Products { get; }
		public decimal TotalPrice { get; }
		public DateTime OrderDate { get; }

		public Order(List<IProduct> products)
		{
			Products = products;
			TotalPrice = CalculateTotalPrice();
			//OrderDate = DateTime.Now;
		}
		public Order(IUser user)
		{
			Products = user.Cart.Products;
			TotalPrice = CalculateTotalPrice();
			//OrderDate = DateTime.Now;
		}

		public void PlaceOrder(IUser user)
		{
			user.Cart.RemoveAll();
		}

		private decimal CalculateTotalPrice()
		{
			decimal totalPrice = 0;
			foreach (var product in Products)
			{
				totalPrice += product.Price;
			}
			return totalPrice;
		}
	}
}
