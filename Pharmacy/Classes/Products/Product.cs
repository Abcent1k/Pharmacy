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
		[Key]
		public uint UPC { get; set; }
		[Required]
		[MaxLength(64)]
		public string Name { get; private set; }
		[Required]
		public decimal Price { get; private set; }
		[Required]
		public uint EDRPOU { get; private set; }

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
