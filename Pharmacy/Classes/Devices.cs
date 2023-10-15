using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Classes
{
	enum DeviceType
	{
		Inhaler,
		Pulse_oximetr,
		Blood_pressure_monitor,
	}
	internal class Devices: Product
	{
		private DeviceType _device_type;
		public DeviceType DeviceType { get { return _device_type; } }
		public Devices(uint uPC,
					 string name,
					 decimal price,
					 uint eDRPOU,
					 DeviceType deviceType) : base(uPC, name, price, eDRPOU)
		{
			_device_type = deviceType;
		}

		public override void Show()
		{
			Console.WriteLine(this.ToString());
		}
		public override string ToString()
		{
			return $"{DeviceType}\nName: {Name}\nPrice: {Price}";
		}
	}
}
