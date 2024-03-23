using System;
namespace ChefConnect.Entities
{
    public class OrderDetails
    {
        public int OrderDetailsId { get; set; }

        public string? OrderInstructions { get; set; }

        public int GuestQuantity { get; set; }

        public DateTime OrderDate { get; set; }

        public int TimeSlotId { get; set; }

        public int RecipeId { get; set; }

        public string CustomerId { get; set; }

        public string ChefId { get; set; }

        public double OrderSubTotal { get; set; }

        public double OrderTax { get; set; }

        public double Charges { get; set; }

        public double OrderTotal { get; set; }

        public int reviewId { get; set; }





        //public Reviews Review { get; set; }

        //public ChefRecipes Recipe{ get; set; }

        //public AppUser Customer { get; set; }
        //public AppUser Chef { get; set;}


    }
}

