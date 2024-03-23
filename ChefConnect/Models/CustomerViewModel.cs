using System;
using ChefConnect.Entities;

namespace ChefConnect.Models
{
	public class CustomerViewModel
	{
		public AppUser? ActiveUser { get; set; }

		public List<ChefRecipes>? AllRecipes { get; set; }

		public ChefRecipes? ActiveRecipe { get; set; }

		public Reviews? NewReview { get; set; }

		public OrderDetails? NewOrder { get; set; }
	}
}

