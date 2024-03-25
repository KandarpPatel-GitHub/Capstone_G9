using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChefConnect.Data;
using ChefConnect.Entities;
using ChefConnect.Models;
using ChefConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChefConnect.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private HelperServices _helperServices = new HelperServices();
        private ChefConnectDbContext _dbcontext;

        public CustomerController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, ChefConnectDbContext dbcontext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _dbcontext = dbcontext;
        }

        [AllowAnonymous]
        [HttpGet("/Register/AsCustomer")]
        public async Task<IActionResult> CustomerRegister()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("/Register/AsCustomer")]
        public async Task<IActionResult> CustomerRegister(RegisterCustomerViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_userManager.FindByNameAsync(registerViewModel.UserName).Result != null)
                {
                    ModelState.AddModelError("UserName", "Username is already registered.");
                }
                if (_userManager.FindByEmailAsync(registerViewModel.Email).Result != null)
                {
                    ModelState.AddModelError("Email", "Email is already registered.");
                }
                if (!isUniquePhoneNumber(registerViewModel.PhoneNumber))
                {
                    ModelState.AddModelError("PhoneNumber", "Phone number is already registered.");
                }
                if (!_helperServices.IsPhoneNumberValid(registerViewModel.PhoneNumber))
                {
                    ModelState.AddModelError("PhoneNumber", "Please enter a valid Canadian phone number.");
                }
                if (ModelState.ErrorCount == 0)
                {

                    var user = new AppUser { UserName = registerViewModel.UserName, Name = registerViewModel.Name };
                    var result = await _userManager.CreateAsync(user, registerViewModel.Password);

                    if (result.Succeeded)
                    {

                        await _userManager.AddToRoleAsync(user, "Customer");
                        user.Email = registerViewModel.Email;
                        user.PhoneNumber = registerViewModel.PhoneNumber;
                        await _userManager.UpdateAsync(user);
                        await _signInManager.SignInAsync(user, false);
                        var message = $"\nHi,\n\nThanks for getting started with ChefConnect!\n\nWe need a little more information to complete your registration, including a confirmation of your email address.\n\nClick below to confirm your email address:\n\nhttps://localhost:7042/{registerViewModel.UserName}/Email-Verification-Success\n\nIf you have problems, please paste the above URL into your web browser.";
                        _helperServices.SendEmailAsync(registerViewModel.Email, "Email Verification", message);
                        if (!user.EmailConfirmed)
                        {
                            TempData["ConfirmEmailMessage"] = $"An email verification is sent to you. Please confirm your email there.";
                        }

                        return RedirectToAction("GetCustomerHome", new { username = registerViewModel.UserName });


                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("Password", error.Description);
                        }
                        return View();
                    }
                }
                else
                {

                    return View();
                }

            }
            else
            {
                return View();
            }
        }

        [HttpGet("/{username}/Home")]
        public async Task<IActionResult> GetCustomerHome(string username)
        {
            CustomerViewModel model = new CustomerViewModel()
            {
                ActiveUser = await _userManager.FindByNameAsync(username),
                AllRecipes = await _dbcontext.ChefRecipes.Include(r => r.RecipeCuisine).ToListAsync()
            };

            return View("CustomerHome", model);
        }

        [HttpGet("/{username}/Customer-Profile")]
        public async Task<IActionResult> GetCustomerAccountSettings(string username)
        {
            CustomerViewModel model = new CustomerViewModel()
            {
                ActiveUser = await _userManager.FindByNameAsync(username)
            };

            return View("AccountSettings", model);
        }

        [HttpPost()]
        public async Task<IActionResult> EditCustomerAccountDetails(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.ActiveUser.UserName);
                user.Name = model.ActiveUser.Name;
                user.PhoneNumber = model.ActiveUser.PhoneNumber;
                //user.DateOfBirth = model.activeUser.DateOfBirth;
                user.UserName = model.ActiveUser.UserName;
                user.Email = model.ActiveUser.Email;
                await _userManager.UpdateAsync(user);
                return RedirectToAction("GetCustomerHome", new { username = model.ActiveUser.UserName });
            }
            else
            {
                return View("AccountSettings", model);
            }
        }

        [HttpGet("/{username}/{id}/Add-Review")]
        public async Task<IActionResult> GetReviewsPage(string username, int id)
        {
            CustomerViewModel model = new CustomerViewModel()
            {
                ActiveUser = await _userManager.FindByNameAsync(username),
                ActiveRecipe = await _dbcontext.ChefRecipes.Include(r => r.RecipeCuisine).Where(r => r.ChefRecipesId == id).FirstOrDefaultAsync(),
                NewReview = new Reviews()
            };

            return View("CustomerAddReview", model);
        }

        [HttpPost()]
        public async Task<IActionResult> AddChefReview(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Reviews.Add(model.NewReview);
                _dbcontext.SaveChanges();
                return RedirectToAction("GetCustomerHome", new { username = User.Identity.Name });
            }
            else
            {
                return View("CustomerAddReview", model);
            }
        }


        //Get Method for Customer to Book a Chef
        [HttpGet("/{username}/{id}/Book-Chef")]
        public async Task<IActionResult> GetBookChefPage(string username, int id)
        {
            CustomerViewModel model = new CustomerViewModel()
            {
                ActiveUser = await _userManager.FindByNameAsync(username),
                ActiveRecipe = await _dbcontext.ChefRecipes.Include(r => r.RecipeCuisine).Where(r => r.ChefRecipesId == id).FirstOrDefaultAsync()

            };

            return View("CustomerBookChef", model);
        }


        //Get Method for Customer to add to cart feature
        [HttpGet("/{username}/{id}/Add-To-Cart")]
        public async Task<IActionResult> GetCartPage(string username, int id)
        {

            var user = await _userManager.FindByNameAsync(username);
            var recipe = await _dbcontext.ChefRecipes.Include(r => r.RecipeCuisine).Where(r => r.ChefRecipesId == id).FirstOrDefaultAsync();
            List<UserCartItem> _cartList = await _dbcontext.UserCartItems.Include(o => o.ChefRecipe).Where(o => o.CustomerId == user.Id).ToListAsync();


            UserCartItem item = new UserCartItem()
            {

                RecipeId = recipe.ChefRecipesId,
                CustomerId = user.Id,
                TimeSlotId = 1
            };

            if (_cartList.Count == 0)
            {
                
                _dbcontext.UserCartItems.Add(item);
                _dbcontext.SaveChanges();
            }
            else if(isNewCartItem(_cartList, item))
            {
                _dbcontext.UserCartItems.Add(item);
                _dbcontext.SaveChanges();

                
            }
           _cartList = await _dbcontext.UserCartItems.Include(o => o.ChefRecipe).Where(o => o.CustomerId == user.Id).ToListAsync();

            

            CustomerViewModel model = new CustomerViewModel()
            {
                ActiveUser = user,
                ActiveRecipe = recipe,
                cartList = _cartList,
                TimeSlots = await _dbcontext.TimeSlots.ToListAsync()

            };
            return View("CustomerCart", model);
        }

        //Method to update checkout page with cart items
        [HttpPost()]
        public async Task<IActionResult> UpdateCartPage(CustomerViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            //var recipe = await _dbcontext.ChefRecipes.Include(r => r.RecipeCuisine).Where(r => r.ChefRecipesId == model.ActiveRecipe.ChefRecipesId).FirstOrDefaultAsync();
            //List<UserCartItem> _cartList = await _dbcontext.UserCartItems.Include(o => o.ChefRecipe).Where(o => o.CustomerId == user.Id).ToListAsync();

            foreach (var item in model.cartList)
            {
                
                item.RecipeTotal = (item.ChefRecipe.Price + ((item.GuestQuantity - item.ChefRecipe.NumberOfPeople)* item.ChefRecipe.PricePerExtraPerson));

                _dbcontext.UserCartItems.Update(item);
                _dbcontext.SaveChanges();
            }

            List<UserCartItem> _cartList = await _dbcontext.UserCartItems.Include(o => o.ChefRecipe).Where(o => o.CustomerId == user.Id).ToListAsync();

            model.ActiveUser = user;
           
            model.cartList = _cartList;
            model.TimeSlots = await _dbcontext.TimeSlots.ToListAsync();

            return View("CustomerCart", model);
        }



        public bool isUniquePhoneNumber(string phone)
        {
            var allUsers = _userManager.Users;

            foreach (var user in allUsers)
            {
                if (user.PhoneNumber == phone)
                {
                    return false;
                }
            }

            return true;
        }


        public bool isNewCartItem(List<UserCartItem> cartList, UserCartItem item)
        {
            foreach (var cartItem in cartList)
            {
                if (cartItem.RecipeId == item.RecipeId)
                {
                    return false;
                }
            }
            return true;
        }

    }
}

