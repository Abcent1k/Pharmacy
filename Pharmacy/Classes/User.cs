﻿using Pharmacy.Classes.Products;
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
		public ICollection<InventoryProduct> Products { get; }
		[MaxLength(30)]
		public string Name { get; private set; }
		[MaxLength(30)]
		public string Surname { get; private set; }
		public string? Email { get; set; }

		public User(string name, string surname)
		{
			Name = name;
			Surname = surname;
			Products = new List<InventoryProduct>();
			Orders = new List<Order>();
		}
		public User(string name, string surname, int userId):this(name, surname)
		{
			UserId = userId;
		}
		public User(string name, string surname, int userId, string email):
			this(name, surname, userId)
		{
			Email = email;
		}

	}
}
