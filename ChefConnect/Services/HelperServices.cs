using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using ChefConnect.Data;

namespace ChefConnect.Services
{
	public class HelperServices
	{
        public bool IsPhoneNumberValid(string phone)
		{
			string pattern = "^([2-9]{1}[0-9]{2})(([2-9]{1})(1[0,2-9]{1}|[0,2-9]{1}[0-9]{1}))([0-9]{4})$";
            Regex regex = new Regex(pattern);

            if (regex.IsMatch(phone))
            {
                return true;
            }
            else
            {
                return false;
            }

		}

		public bool IsPostalCodeValid(string postalcode)
		{
			string pattern = "^[A-Za-z]\\d[A-Za-z][ -]?\\d[A-Za-z]\\d$";

            Regex regex = new Regex(pattern);

            if (regex.IsMatch(postalcode))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		public bool IsValidAge(DateTime? dateOfBirth)
		{
			DateTime today = DateTime.Now;
			DateTime addEighteenYears = Convert.ToDateTime(dateOfBirth).AddYears(18);

            if (today >= addEighteenYears)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public Task SendVerificationEmailAsync(string email,string username)
		{
			var mail = "teamchefconnect@outlook.com";
			var password = "#(Admin@Chefconnect)113!";
			var subject = "Email Verification";
			var message = $"\nHi,\n\nThanks for getting started with ChefConnect!\n\nWe need a little more information to complete your registration, including a confirmation of your email address.\n\nClick below to confirm your email address:\n\nhttps://localhost:7042/{username}/Email-Verification-Success\n\nIf you have problems, please paste the above URL into your web browser.";

			var client = new SmtpClient("smtp-mail.outlook.com", 587)
			{
				EnableSsl = true,
				Credentials = new NetworkCredential(mail,password)
			};
            return client.SendMailAsync(
                new MailMessage(
                    from: mail,
                    to: email,
                    subject,
                    message
                    ));

        }
	}
}

