﻿using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Classes
{

	internal class Order: IOrder
	{
		public delegate void OrderHandler(string message);
		public event OrderHandler? Notify;

		[Required]
		public int Id { get; }
		
		public int UserId { get; private set; }
		
		public User? User { get; private set; }
		public ICollection<InventoryProduct> OrderItems { get; }
		[Required]
		public decimal TotalPrice { get; }
		public DateTime? OrderDate { get; private set; }

		//public Order(ICollection<InventoryProduct> products, User user)
		//{
		//	OrderItems = products;
		//	TotalPrice = CalculateTotalPrice();
		//	PlaceOrder(user);
		//}
		public Order()
		{
			TotalPrice = CalculateTotalPrice();
			OrderItems = new List<InventoryProduct>();
		}

		public void PlaceOrder(IUser user)
		{
			User = (User)user;
			OrderDate = DateTime.Now;
			Notify?.Invoke($"Замовлення на сумму {TotalPrice} від {OrderDate}\n" +
						   $"Замовник: {user.Name} {user.Surname}");
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