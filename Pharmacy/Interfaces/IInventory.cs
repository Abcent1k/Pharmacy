using Pharmacy.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Interfaces
{
	internal interface IInventory
	{
		List<IProduct> Products { get; }
		void AddProduct(IProduct product)
		{
			Products.Add(product);
		}
		void RemoveProduct(IProduct product);
		void RemoveProduct(List<IProduct> products);
		void RemoveAll()
		{
			Products.Clear();
		}
	}
}
