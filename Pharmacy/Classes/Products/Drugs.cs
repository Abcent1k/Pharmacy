using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Classes.Products
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
    internal class Drugs : Product, IExpiration, IProductFormat
    {
		[Required]
		public DateTime ExpirationDate { get; }
		[Required]
		public DrugType DrugType { get; }
		[Required]
		public bool NeedRecipe { get; }

        public Drugs(uint uPC,
                     string name,
                     decimal price,
                     uint eDRPOU,
                     DateTime expirationDate,
                     DrugType drugType,
                     bool needRecipe) : base(uPC, name, price, eDRPOU)
        {
			ExpirationDate = expirationDate;
			DrugType = drugType;
			NeedRecipe = needRecipe;
        }
		public Drugs(uint uPC,
			 string name,
			 decimal price,
			 uint eDRPOU,
			 DrugType drugType,
			 bool needRecipe) : base(uPC, name, price, eDRPOU)
		{
			DrugType = drugType;
			NeedRecipe = needRecipe;
		}

		public override void Show()
        {
            Console.WriteLine(ToString());
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
