using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Classes
{
	internal class User: IUser
	{
		public int UserId { get; }
		public ICollection<Order>? Orders { get; }
		public Cart Cart { get; private set; }
		[MaxLength(30)]
		public string Name { get; private set; }
		[MaxLength(30)]
		public string Surname { get; private set; }

		public User(string name, string surname)
		{
			Name = name;
			Surname = surname;
			Cart = new Cart();
			Cart.SetUser(this);
		}
	}
}
