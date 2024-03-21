﻿using System;
using Microsoft.AspNetCore.Identity;

namespace ChefConnect.Entities
{
    public class AppUser : IdentityUser
    {
        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public ICollection<ChefCuisines>? ChefCuisines { get; set; }
        public ICollection<Reviews>? Reviews { get; set; }
        //public ICollection<OrderDetails>? OrderDetails { get; set; }
    }
}

