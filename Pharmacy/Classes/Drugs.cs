using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Classes
{
	enum DrugType
	{
		Pills,
		Spray,
		Drops,
		Syrop,
		Inhalation_solution,
		Injection_solution,
	}
	internal class Drugs: Product, IExpiration, IProductFormat
	{
		public DateTime ExpirationDate { get; }
		public DrugType DrugType { get; }
		public bool NeedRecipe { get; }

		public Drugs(uint uPC, 
					 string name, 
					 decimal price, 
					 uint eDRPOU,
					 DateTime expirationDate,
					 DrugType drugType,
					 bool needRecipe) :base(uPC, name, price, eDRPOU)
		{
			ExpirationDate = expirationDate;
			DrugType = drugType;
			NeedRecipe = needRecipe;
		}

		public override void Show()
		{
			Console.WriteLine(this.ToString());
		}
		public override string ToString()
		{
			return $"{DrugType}\nName: {Name}\nNeed a recipe: {NeedRecipe}\nPrice: {Price}";
		}

		void IProductFormat.Show()
		{
			Console.WriteLine(((IProductFormat)this).ToString());
		}

		string IProductFormat.ToString()
		{
			return $"{DrugType}; Name: {Name}; Need a recipe: {NeedRecipe}; Price: {Price}";
		}
	}
}
