using System;

namespace ChefConnect.Entities
{
    public class Chef
    {
        public int ChefId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}

