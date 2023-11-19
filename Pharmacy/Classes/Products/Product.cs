using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Classes.Products
{
    internal abstract class Product : IProduct
    {
        private uint _upc;
        private string _name;
        private decimal _price;
        private uint _edrpou;
		[Key]
		public uint UPC { get { return _upc; } set { _upc = value; } }
		[Required]
		[MaxLength(64)]
		public string Name { get { return _name; } private set { _name = value; } }
		[Required]
		public decimal Price { get { return _price; } private set { _price = value; } }
		[Required]
		public uint EDRPOU { get { return _edrpou; } private set { _edrpou = value; } }

        public Product(uint uPC, string name, decimal price, uint eDRPOU)
        {
            _upc = uPC;
            Name = name;
            _price = price;
			EDRPOU = eDRPOU;
        }
		public Product(uint uPC, decimal price)
		{
			_upc = uPC;
			_price = price;
		}

		public abstract void Show();
        public abstract override string ToString();
    }
}
