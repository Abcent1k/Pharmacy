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
		private string _name;
		private string _surname;
		private ICart _cart;

		public ICart Cart { get { return _cart; } }
		public string Name { get { return _name; } }
		public string Surname { get { return _surname; } }

		public User(string name, string surname)
		{
			_name = name;
			_surname = surname;
			_cart = new Cart();
		}
	}
}
