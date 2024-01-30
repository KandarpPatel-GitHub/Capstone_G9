using System;
using Microsoft.AspNetCore.Identity;

namespace ChefConnect.Entities
{
	public class AppUser : IdentityUser
	{
		public string Name { get; set; }

		public DateTime DateOfBirth { get; set; }
	}
}

