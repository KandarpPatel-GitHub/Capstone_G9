﻿// <auto-generated />
using System;
using ChefConnect.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ChefConnect.Migrations
{
    [DbContext(typeof(ChefConnectDbContext))]
    partial class ChefConnectDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("Identity")
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ChefConnect.Entities.Addresses", b =>
                {
                    b.Property<int>("AddressesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressesId"));

                    b.Property<string>("AptNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressesId");

                    b.ToTable("Addresses", "Identity");
                });

            modelBuilder.Entity("ChefConnect.Entities.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("User", "Identity");
                });

            modelBuilder.Entity("ChefConnect.Entities.ChefCuisines", b =>
                {
                    b.Property<int>("ChefCuisinesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChefCuisinesId"));

                    b.Property<string>("ChefId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CuisineId")
                        .HasColumnType("int");

                    b.HasKey("ChefCuisinesId");

                    b.ToTable("ChefCuisines", "Identity");
                });

            modelBuilder.Entity("ChefConnect.Entities.ChefRecipes", b =>
                {
                    b.Property<int>("ChefRecipesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ChefRecipesId"));

                    b.Property<string>("ChefId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CuisineId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfPeople")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<double>("PricePerExtraPerson")
                        .HasColumnType("float");

                    b.Property<string>("RecipeDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RecipeImage")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("RecipeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ChefRecipesId");

                    b.ToTable("ChefRecipes", "Identity");
                });

            modelBuilder.Entity("ChefConnect.Entities.Cuisines", b =>
                {
                    b.Property<int>("CuisinesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CuisinesId"));

                    b.Property<string>("CuisineName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CuisinesId");

                    b.ToTable("Cuisines", "Identity");

                    b.HasData(
                        new
                        {
                            CuisinesId = 1,
                            CuisineName = "Italian"
                        },
                        new
                        {
                            CuisinesId = 2,
                            CuisineName = "Mexican"
                        },
                        new
                        {
                            CuisinesId = 3,
                            CuisineName = "Japanese"
                        },
                        new
                        {
                            CuisinesId = 4,
                            CuisineName = "Chinese"
                        },
                        new
                        {
                            CuisinesId = 5,
                            CuisineName = "Indian"
                        },
                        new
                        {
                            CuisinesId = 6,
                            CuisineName = "French"
                        },
                        new
                        {
                            CuisinesId = 7,
                            CuisineName = "Thai"
                        },
                        new
                        {
                            CuisinesId = 8,
                            CuisineName = "Spanish"
                        },
                        new
                        {
                            CuisinesId = 9,
                            CuisineName = "Greek"
                        },
                        new
                        {
                            CuisinesId = 10,
                            CuisineName = "Turkish"
                        },
                        new
                        {
                            CuisinesId = 11,
                            CuisineName = "Korean"
                        },
                        new
                        {
                            CuisinesId = 12,
                            CuisineName = "Vietnamese"
                        },
                        new
                        {
                            CuisinesId = 13,
                            CuisineName = "Lebanese"
                        },
                        new
                        {
                            CuisinesId = 14,
                            CuisineName = "Brazilian"
                        },
                        new
                        {
                            CuisinesId = 15,
                            CuisineName = "Mediterranean"
                        },
                        new
                        {
                            CuisinesId = 16,
                            CuisineName = "German"
                        },
                        new
                        {
                            CuisinesId = 17,
                            CuisineName = "British"
                        },
                        new
                        {
                            CuisinesId = 18,
                            CuisineName = "Russian"
                        },
                        new
                        {
                            CuisinesId = 19,
                            CuisineName = "American"
                        },
                        new
                        {
                            CuisinesId = 20,
                            CuisineName = "Caribbean"
                        });
                });

            modelBuilder.Entity("ChefConnect.Entities.OrderDetails", b =>
                {
                    b.Property<int>("OrderDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailsId"));

                    b.Property<double>("Charges")
                        .HasColumnType("float");

                    b.Property<string>("ChefId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("GuestQuantity")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderInstructions")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("OrderSubTotal")
                        .HasColumnType("float");

                    b.Property<double>("OrderTax")
                        .HasColumnType("float");

                    b.Property<double>("OrderTotal")
                        .HasColumnType("float");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("TimeSlotId")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailsId");

                    b.ToTable("OrderDetails", "Identity");
                });

            modelBuilder.Entity("ChefConnect.Entities.PaymentMethods", b =>
                {
                    b.Property<int>("PaymentMethodsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentMethodsId"));

                    b.Property<string>("CardCvv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CardExpiry")
                        .HasColumnType("datetime2");

                    b.Property<string>("CardNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameOnCard")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentMethodsId");

                    b.ToTable("PaymentMethods", "Identity");
                });

            modelBuilder.Entity("ChefConnect.Entities.Reviews", b =>
                {
                    b.Property<int>("ReviewsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewsId"));

                    b.Property<string>("CustomerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Ratings")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<string>("ReviewDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReviewsId");

                    b.ToTable("Reviews", "Identity");
                });

            modelBuilder.Entity("ChefConnect.Entities.TimeSlots", b =>
                {
                    b.Property<int>("TimeSlotsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TimeSlotsId"));

                    b.Property<TimeSpan>("TimeSlot")
                        .HasColumnType("time");

                    b.HasKey("TimeSlotsId");

                    b.ToTable("TimeSlots", "Identity");

                    b.HasData(
                        new
                        {
                            TimeSlotsId = 1,
                            TimeSlot = new TimeSpan(0, 0, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 2,
                            TimeSlot = new TimeSpan(0, 0, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 3,
                            TimeSlot = new TimeSpan(0, 1, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 4,
                            TimeSlot = new TimeSpan(0, 1, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 5,
                            TimeSlot = new TimeSpan(0, 2, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 6,
                            TimeSlot = new TimeSpan(0, 2, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 7,
                            TimeSlot = new TimeSpan(0, 3, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 8,
                            TimeSlot = new TimeSpan(0, 3, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 9,
                            TimeSlot = new TimeSpan(0, 4, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 10,
                            TimeSlot = new TimeSpan(0, 4, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 11,
                            TimeSlot = new TimeSpan(0, 5, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 12,
                            TimeSlot = new TimeSpan(0, 5, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 13,
                            TimeSlot = new TimeSpan(0, 6, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 14,
                            TimeSlot = new TimeSpan(0, 6, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 15,
                            TimeSlot = new TimeSpan(0, 7, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 16,
                            TimeSlot = new TimeSpan(0, 7, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 17,
                            TimeSlot = new TimeSpan(0, 8, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 18,
                            TimeSlot = new TimeSpan(0, 8, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 19,
                            TimeSlot = new TimeSpan(0, 9, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 20,
                            TimeSlot = new TimeSpan(0, 9, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 21,
                            TimeSlot = new TimeSpan(0, 10, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 22,
                            TimeSlot = new TimeSpan(0, 10, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 23,
                            TimeSlot = new TimeSpan(0, 11, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 24,
                            TimeSlot = new TimeSpan(0, 11, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 25,
                            TimeSlot = new TimeSpan(0, 12, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 26,
                            TimeSlot = new TimeSpan(0, 12, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 27,
                            TimeSlot = new TimeSpan(0, 13, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 28,
                            TimeSlot = new TimeSpan(0, 13, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 29,
                            TimeSlot = new TimeSpan(0, 14, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 30,
                            TimeSlot = new TimeSpan(0, 14, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 31,
                            TimeSlot = new TimeSpan(0, 15, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 32,
                            TimeSlot = new TimeSpan(0, 15, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 33,
                            TimeSlot = new TimeSpan(0, 16, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 34,
                            TimeSlot = new TimeSpan(0, 16, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 35,
                            TimeSlot = new TimeSpan(0, 17, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 36,
                            TimeSlot = new TimeSpan(0, 17, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 37,
                            TimeSlot = new TimeSpan(0, 18, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 38,
                            TimeSlot = new TimeSpan(0, 18, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 39,
                            TimeSlot = new TimeSpan(0, 19, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 40,
                            TimeSlot = new TimeSpan(0, 19, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 41,
                            TimeSlot = new TimeSpan(0, 20, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 42,
                            TimeSlot = new TimeSpan(0, 20, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 43,
                            TimeSlot = new TimeSpan(0, 21, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 44,
                            TimeSlot = new TimeSpan(0, 21, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 45,
                            TimeSlot = new TimeSpan(0, 22, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 46,
                            TimeSlot = new TimeSpan(0, 22, 30, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 47,
                            TimeSlot = new TimeSpan(0, 23, 0, 0, 0)
                        },
                        new
                        {
                            TimeSlotsId = 48,
                            TimeSlot = new TimeSpan(0, 23, 30, 0, 0)
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("Role", "Identity");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Name = "Chef",
                            NormalizedName = "CHEF"
                        },
                        new
                        {
                            Id = "2",
                            Name = "Customer",
                            NormalizedName = "CUSTOMER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", "Identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ChefConnect.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ChefConnect.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ChefConnect.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ChefConnect.Entities.AppUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
