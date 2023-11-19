using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Classes.Products
{
    enum DeviceType
    {
        Inhaler,
        Pulse_oximetr,
        Blood_pressure_monitor,
    }
    internal class Devices : Product
    {
		[Required]
		public DeviceType DeviceType { get; }
        public Devices(uint uPC,
                     string name,
                     decimal price,
                     uint eDRPOU,
                     DeviceType deviceType) : base(uPC, name, price, eDRPOU)
        {
			DeviceType = deviceType;
        }

        public override void Show()
        {
            Console.WriteLine(ToString());
        }
        public override string ToString()
        {
            return $"{DeviceType}\nName: {Name}\nPrice: {Price}";
        }
    }
}
