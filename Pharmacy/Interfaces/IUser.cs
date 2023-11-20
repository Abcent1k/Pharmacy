using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pharmacy.Classes;

namespace Pharmacy.Interfaces
{
	internal interface IUser
	{
		int UserId { get; }
		string Name { get; }
		string Surname { get; }
	}
}
