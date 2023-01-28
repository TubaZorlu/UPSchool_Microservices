using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UPSchoolECommerce.IdentityServer.Dtos
{
	public class SignUpDto
	{
		public string USerName { get; set; }
		public string Password { get; set; }
		public string City { get; set; }
		public string Email { get; set; }
	}
}
