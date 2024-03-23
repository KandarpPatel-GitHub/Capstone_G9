using System;
using ChefConnect.Entities;

namespace ChefConnect.Models
{
	public class ChefViewModel
	{
        public AppUser? ActiveUser { get; set; }

        public ChefRecipes? ActiveChefRecipe { get; set; }

        public ChefRecipes? NewRecipe { get; set; }

        public List<ChefRecipes>? chefRecipes { get; set; }

        public List<ChefCuisines>? chefCuisines { get; set; }

        public List<Cuisines>? allCuisines { get; set; }

        public ChefCuisines? NewChefCuisine { get; set; }

        public List<ChefRecipes>? otherChefRecipes { get; set; }
    }
}

