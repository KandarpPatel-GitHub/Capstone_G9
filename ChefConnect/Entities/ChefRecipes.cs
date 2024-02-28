﻿using System;
namespace ChefConnect.Entities
{
	public class ChefRecipes
	{
		public int ChefRecipesId { get; set; }

		public string RecipeName { get; set; }

		public string RecipeDescription { get; set; }

		public byte RecipeImage { get; set; }

		public int CuisineId { get; set; }

		public int NumberOfPeople { get; set; }

		public double Price { get; set; }

		public double PricePerExtraPerson { get; set; }

		public string ChefId { get; set; }
	}
}

