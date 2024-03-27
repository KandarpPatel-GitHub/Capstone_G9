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
                AllRecipes = await _dbcontext.ChefRecipes.Include(r => r.RecipeCuisine).Include(r => r.Chef).ToListAsync()
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
        //[HttpGet("/{username}/{id}/Book-Chef")]
        //public async Task<IActionResult> GetBookChefPage(string username, int id)
        //{
        //    CustomerViewModel model = new CustomerViewModel()
        //    {
        //        ActiveUser = await _userManager.FindByNameAsync(username),
        //        ActiveRecipe = await _dbcontext.ChefRecipes.Include(r => r.RecipeCuisine).Where(r => r.ChefRecipesId == id).FirstOrDefaultAsync()

        //    };

        //    return View("CustomerBookChef", model);
        //}
        [HttpGet("/{username}/Cart")]
        public async Task<IActionResult> GetCustomerCart(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            List<UserCartItem> _cartList = await _dbcontext.UserCartItems.Include(o => o.ChefRecipe).Where(o => o.CustomerId == user.Id).ToListAsync();

            CustomerViewModel model = new CustomerViewModel()
            {
                ActiveUser = user,
                cartList = _cartList,
                TimeSlots = await _dbcontext.TimeSlots.ToListAsync()

            };
            return View("CustomerCart", model);
        }

        //Get Method for Customer to add to cart feature
        [HttpGet("/{username}/{id}/Cart")]
        public async Task<IActionResult> AddRecipeToCart(string username,int id)
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
            //_cartList = await _dbcontext.UserCartItems.Include(o => o.ChefRecipe).Where(o => o.CustomerId == user.Id).ToListAsync();



            // CustomerViewModel model = new CustomerViewModel()
            // {
            //     ActiveUser = user,
            //     ActiveRecipe = recipe,
            //     cartList = _cartList,
            //     TimeSlots = await _dbcontext.TimeSlots.ToListAsync()

            // };
            return RedirectToAction("GetCustomerCart", new { username = user.UserName });
        }

        //Method to update checkout page with cart items
        [HttpPost()]
        public async Task<IActionResult> UpdateCartPage(IFormCollection form)
        {
            int id = int.Parse(form["itemId"]);
            var timeSlotId = form["timeSlotId"];
            var guestCount = form["GuestQuantity"];
            
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
           var userCartItem = await _dbcontext.UserCartItems.Include(uc => uc.ChefRecipe).Where(o => o.UserCartItemId == id).FirstOrDefaultAsync();
            userCartItem.TimeSlotId = Convert.ToInt32(timeSlotId);
            userCartItem.GuestQuantity = Convert.ToInt32(guestCount);
            userCartItem.RecipeTotal = (userCartItem.ChefRecipe.Price + (userCartItem.GuestQuantity*userCartItem.ChefRecipe.PricePerExtraPerson));
            _dbcontext.UserCartItems.Update(userCartItem);
            _dbcontext.SaveChanges();
            return RedirectToAction("GetCustomerCart", new { username = user.UserName });
        }

        [HttpGet()]
        public async Task<IActionResult> RemoveItemFromCart( int id)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var cartItem = await _dbcontext.UserCartItems.Where(u => u.UserCartItemId == id).FirstOrDefaultAsync();

            _dbcontext.UserCartItems.Remove(cartItem);
            _dbcontext.SaveChanges();

            return RedirectToAction("GetCustomerCart", new { username = user.UserName });
        }


        //Navigate to Secure Checkout page
        [HttpGet("/{username}/Checkout")]
        public async Task<IActionResult> GetSecureCheckoutPage(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            CustomerViewModel model = new CustomerViewModel()
            {
                ActiveUser = user,
                cartList = await _dbcontext.UserCartItems.Include(o => o.ChefRecipe).Where(o => o.CustomerId == user.Id).ToListAsync(),
               
            };
            return View("CustomerCheckout",model);
        }


        //Validation with Idictionary
        [HttpPost()]
        public async Task<IActionResult> CheckValidations(CustomerViewModel model)
        {

            
            
                Addresses _address = new Addresses();
                _address.Name = model.ActiveUser.CustomerAddress.Name;
                _address.CustomerId = model.ActiveUser.CustomerAddress.CustomerId;
        
                //_dbcontext.Addresses.Add(_address);
                //_dbcontext.SaveChanges();
                return RedirectToAction("GetSecureCheckoutPage", new { username = User.Identity.Name });

            
        }



        //Get the customer to address page
        [HttpGet("/{username}/Address")]
        public async Task<IActionResult> GetCustomerAddressPage(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            var address = await _dbcontext.Addresses.Where(a => a.CustomerId == user.Id).FirstOrDefaultAsync();
            AddressViewModel model = new AddressViewModel();
            if (address != null)
            {
                model.Name = address.Name;
                model.AptNumber = address.AptNumber;
                model.StreetAddress = address.StreetAddress;
                model.City = address.City;
                model.Province = address.Province;
                model.Country = address.Country;
                model.PostalCode = address.PostalCode;
                model.PhoneNumber = address.PhoneNumber;
                model.CustomerId = address.CustomerId;
            }
            
            
           

            return View("CustomerAddress", model);
        }


        //Post Method for Customer to add address
        [HttpPost()]
        public async Task<IActionResult> AddCustomerAddress(AddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                Addresses address = new Addresses()
                {
                    Name = model.Name,
                    AptNumber = model.AptNumber,
                    StreetAddress = model.StreetAddress,
                    City = model.City,
                    Province = model.Province,
                    Country = model.Country,
                    PostalCode = model.PostalCode,
                    PhoneNumber = model.PhoneNumber,
                    CustomerId = user.Id
                };
                _dbcontext.Addresses.Add(address);
                _dbcontext.SaveChanges();
                return RedirectToAction("GetCustomerHome", new { username = user.UserName });
            }
            else
            {
                return View("CustomerAddress", model);
            }
        }


        //[HttpPost()]
        //public async Task<IActionResult> SecureCheckout(CustomerViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByNameAsync(model.ActiveUser.UserName);
        //        var cartList = await _dbcontext.UserCartItems.Include(o => o.ChefRecipe).Where(o => o.CustomerId == user.Id).ToListAsync();
        //        var order = new OrderDetails()
        //        {
        //            CustomerId = user.Id,
        //            OrderDate = DateTime.Now,
        //            OrderTotal = cartList.Sum(o => o.RecipeTotal),
        //            OrderStatus = "Pending",
        //            PaymentMethodId = model.NewPaymentMethod.PaymentMethodId
        //        };
        //        _dbcontext.OrderDetails.Add(order);
        //        _dbcontext.SaveChanges();

        //        foreach (var item in cartList)
        //        {
        //            var orderItem = new OrderItems()
        //            {
        //                OrderId = order.OrderId,
        //                RecipeId = item.RecipeId,
        //                GuestQuantity = item.GuestQuantity,
        //                TimeSlotId = item.TimeSlotId,
        //                RecipeTotal = item.RecipeTotal
        //            };
        //            _dbcontext.OrderItems.Add(orderItem);
        //            _dbcontext.SaveChanges();
        //        }

        //        return RedirectToAction("GetCustomerHome", new { username = user.UserName });
        //    }
        //    else
        //    {
        //        var user = await _userManager.FindByNameAsync(model.ActiveUser.UserName);
        //        model.cartList = await _dbcontext.UserCartItems.Include(o => o.ChefRecipe).Where(o => o.CustomerId == user.Id).ToListAsync();
        //        return View("CustomerCheckout", model);
        //    }
        //}   


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

