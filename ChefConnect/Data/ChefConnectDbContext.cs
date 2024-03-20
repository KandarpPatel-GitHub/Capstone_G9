using ChefConnect.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChefConnect.Data;

public class ChefConnectDbContext : IdentityDbContext<AppUser>
{
    public ChefConnectDbContext(DbContextOptions<ChefConnectDbContext> options)
        : base(options)
    {

    }

    public static async Task CreateAdminUser(IServiceProvider serviceProvider)
    {
        UserManager<AppUser> userManager =
            serviceProvider.GetRequiredService<UserManager<AppUser>>();
        RoleManager<IdentityRole> roleManager = serviceProvider
            .GetRequiredService<RoleManager<IdentityRole>>();

        string username = "admin";
        string password = "Sesame123#";
        string roleName = "Admin";

        // if role doesn't exist, create it
        if (await roleManager.FindByNameAsync(roleName) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
        // if username doesn't exist, create it and add it to role
        if (await userManager.FindByNameAsync(username) == null)
        {
            AppUser user = new AppUser { UserName = username };
            var result = await userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, roleName);
            }
        }
    }

    // Tables related to Chefs
    public DbSet<ChefRecipes> ChefRecipes { get; set; } 
    public DbSet<ChefCuisines> ChefCuisines { get; set; }

    // Tables related to Customer
    public DbSet<Addresses> Addresses { get; set; }
    public DbSet<PaymentMethods> PaymentMethods { get; set; }

    // Other Tables
    public DbSet<Cuisines> Cuisines { get; set; }
    public DbSet<OrderDetails> OrderDetails { get; set; }
    public DbSet<Reviews> Reviews { get; set; }
    public DbSet<TimeSlots> TimeSlots { get; set; }
    //public DbSet<RecipeImages> RecipeImages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Cuisines>().HasMany(c => c.Recipes).WithOne(r => r.RecipeCuisine).HasForeignKey(r => r.CuisineId).IsRequired();
        //var imageList = new[]
        //{
        //    new RecipeImages { RecipeImageId = 1, Image = new byte[0] },
        //    new RecipeImages { RecipeImageId = 2, Image = new byte[0] },
        //    new RecipeImages { RecipeImageId = 3, Image = new byte[0] },
        //    new RecipeImages { RecipeImageId = 4, Image = new byte[0] },
        //    new RecipeImages { RecipeImageId = 5, Image = new byte[0] },
        //    new RecipeImages { RecipeImageId = 6, Image = new byte[0] },
        //    new RecipeImages { RecipeImageId = 7, Image = new byte[0] },
        //    new RecipeImages { RecipeImageId = 8, Image = new byte[0] },
        //    new RecipeImages { RecipeImageId = 9, Image = new byte[0] }
        //};
        var cuisines = new[]
        {
            new Cuisines { CuisinesId = 1, CuisineName = "Italian" },
            new Cuisines { CuisinesId = 2, CuisineName = "Mexican" },
            new Cuisines { CuisinesId = 3, CuisineName = "Japanese" },
            new Cuisines { CuisinesId = 4, CuisineName = "Chinese" },
            new Cuisines { CuisinesId = 5, CuisineName = "Indian" },
            new Cuisines { CuisinesId = 6, CuisineName = "French" },
            new Cuisines { CuisinesId = 7, CuisineName = "Thai" },
            new Cuisines { CuisinesId = 8, CuisineName = "Spanish" },
            new Cuisines { CuisinesId = 9, CuisineName = "Greek" },
            new Cuisines { CuisinesId = 10, CuisineName = "Turkish" },
            new Cuisines { CuisinesId = 11, CuisineName = "Korean" },
            new Cuisines { CuisinesId = 12, CuisineName = "Vietnamese" },
            new Cuisines { CuisinesId = 13, CuisineName = "Lebanese" },
            new Cuisines { CuisinesId = 14, CuisineName = "Brazilian" },
            new Cuisines { CuisinesId = 15, CuisineName = "Mediterranean" },
            new Cuisines { CuisinesId = 16, CuisineName = "German" },
            new Cuisines { CuisinesId = 17, CuisineName = "British" },
            new Cuisines { CuisinesId = 18, CuisineName = "Russian" },
            new Cuisines { CuisinesId = 19, CuisineName = "American" },
            new Cuisines { CuisinesId = 20, CuisineName = "Caribbean" }
     
        };


        var timeSlots = new[]
        {
            new TimeSlots { TimeSlotsId = 1, TimeSlot = new TimeSpan(0, 0, 0) },   // 00:00
            new TimeSlots { TimeSlotsId = 2, TimeSlot = new TimeSpan(0, 30, 0) },  // 00:30
            new TimeSlots { TimeSlotsId = 3, TimeSlot = new TimeSpan(1, 0, 0) },   // 01:00
            new TimeSlots { TimeSlotsId = 4, TimeSlot = new TimeSpan(1, 30, 0) },  // 01:30
            new TimeSlots { TimeSlotsId = 5, TimeSlot = new TimeSpan(2, 0, 0) },   // 02:00
            new TimeSlots { TimeSlotsId = 6, TimeSlot = new TimeSpan(2, 30, 0) },  // 02:30
            new TimeSlots { TimeSlotsId = 7, TimeSlot = new TimeSpan(3, 0, 0) },   // 03:00
            new TimeSlots { TimeSlotsId = 8, TimeSlot = new TimeSpan(3, 30, 0) },  // 03:30
            new TimeSlots { TimeSlotsId = 9, TimeSlot = new TimeSpan(4, 0, 0) },   // 04:00
            new TimeSlots { TimeSlotsId = 10, TimeSlot = new TimeSpan(4, 30, 0) }, // 04:30
            new TimeSlots { TimeSlotsId = 11, TimeSlot = new TimeSpan(5, 0, 0) },  // 05:00
            new TimeSlots { TimeSlotsId = 12, TimeSlot = new TimeSpan(5, 30, 0) }, // 05:30
            new TimeSlots { TimeSlotsId = 13, TimeSlot = new TimeSpan(6, 0, 0) },  // 06:00
            new TimeSlots { TimeSlotsId = 14, TimeSlot = new TimeSpan(6, 30, 0) }, // 06:30
            new TimeSlots { TimeSlotsId = 15, TimeSlot = new TimeSpan(7, 0, 0) },  // 07:00
            new TimeSlots { TimeSlotsId = 16, TimeSlot = new TimeSpan(7, 30, 0) }, // 07:30
            new TimeSlots { TimeSlotsId = 17, TimeSlot = new TimeSpan(8, 0, 0) },  // 08:00
            new TimeSlots { TimeSlotsId = 18, TimeSlot = new TimeSpan(8, 30, 0) }, // 08:30
            new TimeSlots { TimeSlotsId = 19, TimeSlot = new TimeSpan(9, 0, 0) },  // 09:00
            new TimeSlots { TimeSlotsId = 20, TimeSlot = new TimeSpan(9, 30, 0) }, // 09:30
            new TimeSlots { TimeSlotsId = 21, TimeSlot = new TimeSpan(10, 0, 0) }, // 10:00
            new TimeSlots { TimeSlotsId = 22, TimeSlot = new TimeSpan(10, 30, 0) },// 10:30
            new TimeSlots { TimeSlotsId = 23, TimeSlot = new TimeSpan(11, 0, 0) }, // 11:00
            new TimeSlots { TimeSlotsId = 24, TimeSlot = new TimeSpan(11, 30, 0) },// 11:30
            new TimeSlots { TimeSlotsId = 25, TimeSlot = new TimeSpan(12, 0, 0) }, // 12:00 PM
            new TimeSlots { TimeSlotsId = 26, TimeSlot = new TimeSpan(12, 30, 0) },// 12:30 PM
            new TimeSlots { TimeSlotsId = 27, TimeSlot = new TimeSpan(13, 0, 0) }, // 01:00 PM
            new TimeSlots { TimeSlotsId = 28, TimeSlot = new TimeSpan(13, 30, 0) },// 01:30 PM
            new TimeSlots { TimeSlotsId = 29, TimeSlot = new TimeSpan(14, 0, 0) }, // 02:00 PM
            new TimeSlots { TimeSlotsId = 30, TimeSlot = new TimeSpan(14, 30, 0) },// 02:30 PM
            new TimeSlots { TimeSlotsId = 31, TimeSlot = new TimeSpan(15, 0, 0) }, // 03:00 PM
            new TimeSlots { TimeSlotsId = 32, TimeSlot = new TimeSpan(15, 30, 0) },// 03:30 PM
            new TimeSlots { TimeSlotsId = 33, TimeSlot = new TimeSpan(16, 0, 0) }, // 04:00 PM
            new TimeSlots { TimeSlotsId = 34, TimeSlot = new TimeSpan(16, 30, 0) },// 04:30 PM
            new TimeSlots { TimeSlotsId = 35, TimeSlot = new TimeSpan(17, 0, 0) }, // 05:00 PM
            new TimeSlots { TimeSlotsId = 36, TimeSlot = new TimeSpan(17, 30, 0) },// 05:30 PM
            new TimeSlots { TimeSlotsId = 37, TimeSlot = new TimeSpan(18, 0, 0) }, // 06:00 PM
            new TimeSlots { TimeSlotsId = 38, TimeSlot = new TimeSpan(18, 30, 0) },// 06:30 PM
            new TimeSlots { TimeSlotsId = 39, TimeSlot = new TimeSpan(19, 0, 0) }, // 07:00 PM
            new TimeSlots { TimeSlotsId = 40, TimeSlot = new TimeSpan(19, 30, 0) },// 07:30 PM
            new TimeSlots { TimeSlotsId = 41, TimeSlot = new TimeSpan(20, 0, 0) }, // 08:00 PM
            new TimeSlots { TimeSlotsId = 42, TimeSlot = new TimeSpan(20, 30, 0) },// 08:30 PM
            new TimeSlots { TimeSlotsId = 43, TimeSlot = new TimeSpan(21, 0, 0) }, // 09:00 PM
            new TimeSlots { TimeSlotsId = 44, TimeSlot = new TimeSpan(21, 30, 0) },// 09:30 PM
            new TimeSlots { TimeSlotsId = 45, TimeSlot = new TimeSpan(22, 0, 0) }, // 10:00 PM
            new TimeSlots { TimeSlotsId = 46, TimeSlot = new TimeSpan(22, 30, 0) },// 10:30 PM
            new TimeSlots { TimeSlotsId = 47, TimeSlot = new TimeSpan(23, 0, 0) }, // 11:00 PM
            new TimeSlots { TimeSlotsId = 48, TimeSlot = new TimeSpan(23, 30, 0) } // 11:30 PM
        };




        base.OnModelCreating(builder);
        builder.HasDefaultSchema("Identity");
        builder.Entity<AppUser>(entity =>
        {
            entity.ToTable(name: "User");
        });
        builder.Entity<IdentityRole>(entity =>
        {
            entity.ToTable(name: "Role");
        });
        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.ToTable("UserRoles");
        });
        builder.Entity<IdentityUserClaim<string>>(entity =>
        {
            entity.ToTable("UserClaims");
        });
        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.ToTable("UserLogins");
        });
        builder.Entity<IdentityRoleClaim<string>>(entity =>
        {
            entity.ToTable("RoleClaims");
        });
        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.ToTable("UserTokens");
        });

        builder.Entity<IdentityRole>().HasData(new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = "1",
                Name = "Chef",
                NormalizedName = "CHEF"
            },
            new IdentityRole
            {
                Id = "2",
                Name = "Customer",
                NormalizedName = "CUSTOMER"
            }
        });

        builder.Entity<Cuisines>().HasData(cuisines);

        builder.Entity<TimeSlots>().HasData(timeSlots);
    }
}
