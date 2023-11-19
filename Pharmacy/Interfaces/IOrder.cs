using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Interfaces
{
	internal interface IOrder
	{
		ICollection<InventoryProduct> OrderItems { get; }
		decimal TotalPrice { get; }
		DateTime? OrderDate { get; }
		void PlaceOrder(IUser user);
	}
}
