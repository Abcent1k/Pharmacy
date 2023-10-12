using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Classes
{
	internal abstract class Product: IProduct
	{
		public uint UPC { get; set; }
		public string Name { get; }
		public decimal Price { get; }
		public uint EDRPOU { get; }

		public Product(uint uPC, string name, decimal price, uint eDRPOU)
		{
			UPC = uPC;
			Name = name;
			Price = price;
			EDRPOU = eDRPOU;
		}

		public abstract void Show();
		public abstract override string ToString();
	}
}
