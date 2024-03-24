using System;
namespace ChefConnect.Entities
{
	public class OrderRecipes
	{
		public int OrderId { get; set; }

		public OrderDetails? OrderDetails { get; set; }

		public int RecipeId { get; set; }

		public ChefRecipes? ChefRecipes { get; set; }

		public int GuestQuantity { get; set; }

        public int TimeSlotId { get; set; }

		public TimeSlots? TimeSlot { get; set; }

		public double RecipeTotal { get; set; }
    }
}

