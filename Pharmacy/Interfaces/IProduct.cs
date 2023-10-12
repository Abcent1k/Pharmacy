using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Interfaces
{
	enum ProductType
	{
		Drug,
		Сonsumable,
		Device,
	}

	internal interface IExpiration
	{
		DateTime ExpirationDate { get; }
	}

	internal interface IProduct
	{
		uint UPC { get; set; }
		string Name { get; }
		ProductType ProductType { get; }
		decimal Price { get; }
		uint EDRPOU { get; }
		void Show();
		string ToString();
	}
}
