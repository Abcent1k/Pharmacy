using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Classes.Products
{
    enum ConsumableType
    {
        Patch,
        Syringe,
        Needle,
        Gauze
    }

    internal class Consumables : Product, IExpiration
    {
        private DateTime _expiration_date;
		[Required]
        private ConsumableType _consumable_type;
		[Required]
		public DateTime ExpirationDate { get { return _expiration_date; } }
		[Required]
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
            Console.WriteLine(ToString());
        }
        public override string ToString()
        {
            return $"{ConsumableType}\nName: {Name}\nPrice: {Price}";
        }
    }
}
