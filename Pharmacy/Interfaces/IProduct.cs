using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Interfaces
{
	internal interface IExpiration
	{
		DateTime ExpirationDate { get; }
	}

	internal interface IProduct
	{
		uint UPC { get; set; }
		string Name { get; }
		decimal Price { get; }
		uint EDRPOU { get; }
		void Show();
		string ToString();
	}

	internal interface IProductFormat
	{
		void Show();
		string ToString();
	}
}
