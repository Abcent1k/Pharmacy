using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Interfaces
{
	internal interface IUser
	{
		string Name { get; }
		string Surname { get; }
		ICart Cart { get; }
	}
}
