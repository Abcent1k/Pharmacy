using Pharmacy.Classes;
using Pharmacy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy
{
	internal class Program
	{
		static void Main(string[] args)
		{
			var date1 = new DateTime(2023, 7, 20);

			var Pills = new Drugs(123456789, "Tantum verde", 112.25m, 1236747675, date1, DrugType.Spray, false);



			Pills.Show();

			Console.WriteLine();

			((IProduct)Pills).Show();

			Console.WriteLine();

			((IProductFormat)Pills).Show();



			Console.ReadLine();
		}
	}
}
