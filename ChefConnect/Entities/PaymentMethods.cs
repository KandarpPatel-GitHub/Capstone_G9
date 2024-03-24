using System;
namespace ChefConnect.Entities
{
	public class PaymentMethods
	{
		public int PaymentMethodsId { get; set; }

		public string PaymentType { get; set; }

		public string NameOnCard { get; set; }

		public string CardNumber { get; set; }

		public string CardCvv { get; set; }

		public DateTime CardExpiry { get; set; }

		public string CustomerId { get; set; }

		public AppUser? Customer { get; set; }
    }
}

