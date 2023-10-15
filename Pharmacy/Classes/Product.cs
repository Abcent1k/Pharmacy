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
		private uint _upc;
		private string _name;
		private decimal _price;
		private uint _edrpou;
		public uint UPC { get { return _upc; } set { _upc = value; } }
		public string Name { get { return _name; } }
		public decimal Price { get { return _price; } }
		public uint EDRPOU { get { return _edrpou; } }

		public Product(uint uPC, string name, decimal price, uint eDRPOU)
		{
			_upc = uPC;
			_name = name;
			_price = price;
			_edrpou = eDRPOU;
		}

		public abstract void Show();
		public abstract override string ToString();
	}
}
