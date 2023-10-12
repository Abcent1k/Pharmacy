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
		public ProductType ProductType { get; }
		public decimal Price { get; }
		public uint EDRPOU { get; }

		public Product(uint uPC, 
					   string name, 
					   ProductType productType,
					   decimal price, 
					   uint eDRPOU)
		{
			UPC = uPC;
			Name = name;
			ProductType = productType;
			Price = price;
			EDRPOU = eDRPOU;
		}

		public abstract void Show();
		public abstract override string ToString();
	}
}
