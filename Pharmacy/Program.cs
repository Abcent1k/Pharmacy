﻿using Microsoft.Extensions.Configuration;
using Pharmacy.Classes;
using Pharmacy.Data;
using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var u = new User("Саша", "Дядя");

			var contextFactory = new PharmacyContextFactory();

			using (var context = contextFactory.CreateDbContext(new string[] { }))
			{
				context.Users.Add(u);
				context.SaveChanges();
			}
		}
	}
}