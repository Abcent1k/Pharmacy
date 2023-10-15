using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Classes
{
	internal class User: IUser
	{
		IInventory _cart;
		IInventory _favorites;

		public IInventory Cart { get { return _cart; } }
		public IInventory Favorites { get { return _favorites; } }

		public User()
		{
			_cart = new Cart();
			_favorites = new Favorites();
		}
	}
}
