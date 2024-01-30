using System;
using System.Text.RegularExpressions;

namespace ChefConnect.Services
{
	public class HelperServices
	{
		public bool IsValidUserName(string username)
		{
			// Username requirements:
			// Should start with alphabet uppercase or lowercase
			// Should be 8-30 chars long
			// Should contain only alphanumeric (uppercase[A Z], lowercase[a z] alphabets and digits[0 9]) chars, underscores
			string pattern = "^[A-Za-z][A-Za-z0-9_]{7,29}$";
			Regex regex = new Regex(pattern);

			if (regex.IsMatch(username))
			{
				return true;
			}
			else
			{
				return false;
			}
        }

		public bool IsPhoneNumberValid(string phone)
		{
			return true;
		}

		public bool IsPostalCodeValid(string postalcode)
		{
			return true;
		}
	}
}

