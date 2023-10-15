using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Classes
{
	enum ConsumableType
	{
		Patch,
		Syringe,
		Needle,
		Gauze
	}

	internal class Consumables: Product, IExpiration
	{
		private DateTime _expiration_date;
		private ConsumableType _consumable_type;
		public DateTime ExpirationDate { get { return _expiration_date; } }
		public ConsumableType ConsumableType { get { return _consumable_type; } }

		public Consumables(uint uPC,
					 string name,
					 decimal price,
					 uint eDRPOU,
					 DateTime expirationDate,
					 ConsumableType consumableType) : base(uPC, name, price, eDRPOU)
		{
			_expiration_date = expirationDate;
			_consumable_type = consumableType;
		}

		public override void Show()
		{
			Console.WriteLine(this.ToString());
		}
		public override string ToString()
		{
			return $"{ConsumableType}\nName: {Name}\nPrice: {Price}";
		}
	}
}
