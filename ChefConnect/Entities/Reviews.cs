using System;
namespace ChefConnect.Entities
{
	public class Reviews
	{
		public int ReviewsId { get; set; }

        public string? ReviewDescription { get; set; }

        public int Ratings { get; set; }

        public string ChefId { get; set; }

        public string CustomerId { get; set; }

        public AppUser? Customer { get; set; }


        //public int OrderId { get; set; }    
        //public OrderDetails Order { get; set; }

        //public int ReviewsId { get; set; }

        //public string? ReviewDescription { get; set; }

        //public int Ratings { get; set; }

        //public int RecipeId { get; set; }

        //public string CustomerId { get; set; }
        //public AppUser Customer { get; set; }

        //public int OrderId { get; set; }


    }
}

