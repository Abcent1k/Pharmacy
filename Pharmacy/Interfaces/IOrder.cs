using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Interfaces
{
	internal interface IOrder
	{
		//uint OrderID { get; }
		List<InventoryProduct> OrderItems { get; }
		decimal TotalPrice { get; }
		DateTime OrderDate { get; }
		void PlaceOrder(IUser user);
	}
}
