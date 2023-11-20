using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; }
		public int? UserId { get; private set; }
		public User? User { get; private set; }
		public ICollection<InventoryProduct> InventoryProducts { get; private set; }
		[Required]
		public decimal? TotalPrice { get; private set; }
		public DateTime? OrderDate { get; private set; }

		public Order()
		{
		}


		public void PlaceOrder(IUser user)
		{
			User = (User)user;
			OrderDate = DateTime.Now;
			TotalPrice = CalculateTotalPrice();
			Notify?.Invoke($"Замовлення на сумму {TotalPrice} від {OrderDate}\n" +
						   $"Замовник: {user.Name} {user.Surname}");
		}

		private decimal CalculateTotalPrice()
		{
			decimal totalPrice = 0;
			foreach (var inv_product in User.Products)
			{
				totalPrice += inv_product.Quantity * inv_product.Product.Price;
			}
			return totalPrice;
		}
	}
}
