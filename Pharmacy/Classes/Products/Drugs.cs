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
		private DateTime _expiration_date;
		private DrugType _drug_type;
		private bool _need_recipe;
		[Required]
		public DateTime ExpirationDate { get { return _expiration_date; } }
		[Required]
		public DrugType DrugType { get { return _drug_type; } }
		[Required]
		public bool NeedRecipe { get { return _need_recipe; } }

        public Drugs(uint uPC,
                     string name,
                     decimal price,
                     uint eDRPOU,
                     DateTime expirationDate,
                     DrugType drugType,
                     bool needRecipe) : base(uPC, name, price, eDRPOU)
        {
            _expiration_date = expirationDate;
            _drug_type = drugType;
            _need_recipe = needRecipe;
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
