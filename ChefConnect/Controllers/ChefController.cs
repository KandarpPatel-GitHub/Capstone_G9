using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChefConnect.Models;
using Microsoft.AspNetCore.Identity;
using ChefConnect.Entities;
using ChefConnect.Services;
using ChefConnect.Data;
using Microsoft.EntityFrameworkCore;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChefConnect.Controllers
{
    [Authorize(Roles ="Chef")]
    public class ChefController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private HelperServices _helperServices = new HelperServices();
        private ChefConnectDbContext _chefConnectDbContext;

        public ChefController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ChefConnectDbContext chefConnectDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _chefConnectDbContext = chefConnectDbContext;
        }

        [AllowAnonymous]
        [HttpGet("/Register/AsChef")]
        public async Task<IActionResult> ChefRegister()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("/Register/AsChef")]
        public async Task<IActionResult> ChefRegister(RegisterViewModel registerViewModel)
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
                if (!_helperServices.IsValidAge(registerViewModel.DateOfBirth))
                {
                    ModelState.AddModelError("DateOfBirth", "You must be 18 years or more to register.");
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
                        await _userManager.AddToRoleAsync(user, "Chef");
                        user.Email = registerViewModel.Email;
                        user.PhoneNumber = registerViewModel.PhoneNumber;
                        user.DateOfBirth = registerViewModel.DateOfBirth;
                        await _userManager.UpdateAsync(user);
                        await _signInManager.SignInAsync(user, false);
                        await _helperServices.SendVerificationEmailAsync(registerViewModel.Email, registerViewModel.UserName);
                        if (!user.EmailConfirmed)
                        {
                            TempData["ConfirmEmailMessage"] = $"An email verification is sent to you. Please confirm your email there.";
                        }

                        return RedirectToAction("ChefProfile", new { username = registerViewModel.UserName });


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
            else
            {
                return View();
            }
        }

        [HttpGet("/{username}/Profile")]
        public async Task<IActionResult> ChefProfile(string username)
        {
            ChefViewModel model = new ChefViewModel()
            {
                ActiveUser = await _userManager.FindByNameAsync(username)
            };

            return View(model);
        }

        [HttpGet("/{username}/My-Bookings")]
        public async Task<IActionResult> GetMyBookingsPage(string username)
        {
            return View("MyBookings");
        }

        [HttpGet("/{username}/My-Recipes-Cuisines")]
        public async Task<IActionResult> GetMyRecipesAndCuisinesPage(string username)
        {
            var User = await _userManager.FindByNameAsync(username);
            ChefViewModel model = new ChefViewModel()
            {
                ActiveUser = await _userManager.FindByNameAsync(username),
                chefRecipes = await _chefConnectDbContext.ChefRecipes.Where(r => r.ChefId == User.Id).ToListAsync(),
                chefCuisines = await _chefConnectDbContext.ChefCuisines.Where(r => r.ChefId == User.Id).ToListAsync(),
                allCuisines = await _chefConnectDbContext.Cuisines.ToListAsync()
            };
            return View("MyRecipesAndCuisines", model);
        }

        [HttpGet("/{username}/Add-Recipes")]
        public async Task<IActionResult> GetAddRecipesPage(string username)
        {
            ChefViewModel model = new ChefViewModel()
            {
                ActiveUser = await _userManager.FindByNameAsync(username),
                allCuisines = await _chefConnectDbContext.Cuisines.ToListAsync(),
                NewRecipe = new ChefRecipes()
            };
            return View("AddRecipes", model);
        }

        [HttpPost("/New-Recipe-Added")]
        public async Task<IActionResult> AddNewRecipe(ChefViewModel model)
        {
            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    model.NewRecipe.RecipeImage = dataStream.ToArray();
                }
            }

            _chefConnectDbContext.ChefRecipes.Add(model.NewRecipe);
            _chefConnectDbContext.SaveChanges();

            return RedirectToAction("GetMyRecipesAndCuisinesPage", new { username = User.Identity.Name });
            
        }

        [HttpGet("/{username}/Recipe-Details/{id}")]
        public async Task<IActionResult> GetRecipeDetailsPage(string username,int id)
        {
            ChefViewModel model = new ChefViewModel()
            {
                ActiveUser = await _userManager.FindByNameAsync(username),
                ActiveChefRecipe = await _chefConnectDbContext.ChefRecipes.Where(r => r.ChefRecipesId == id).FirstOrDefaultAsync()
            };

            return View("RecipeDetails", model);
        }

        [HttpGet("/{username}/Edit-Recipe/{id}")]
        public async Task<IActionResult> GetEditRecipesPage(string username,int id)
        {
            
            ChefViewModel model = new ChefViewModel()
            {
                ActiveUser = await _userManager.FindByNameAsync(username),
                allCuisines = await _chefConnectDbContext.Cuisines.ToListAsync(),
                ActiveChefRecipe = await _chefConnectDbContext.ChefRecipes.Where(r => r.ChefRecipesId == id).FirstOrDefaultAsync()
            };
            return View("EditRecipes", model);
        }

        [HttpPost("/Recipe-Edit-Success")]
        public async Task<IActionResult> EditRecipe(ChefViewModel model)
        {
            Console.WriteLine(model.ActiveChefRecipe.RecipeName);
            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    model.ActiveChefRecipe.RecipeImage = dataStream.ToArray();
                }
            }

            _chefConnectDbContext.ChefRecipes.Update(model.ActiveChefRecipe);
            _chefConnectDbContext.SaveChanges();

            return RedirectToAction("GetRecipeDetailsPage", new { id = model.ActiveChefRecipe.ChefId });
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



        public IActionResult Index()
        {
            return View();
        }

    }

}

