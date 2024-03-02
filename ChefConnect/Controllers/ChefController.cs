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
            UserViewModel model = new UserViewModel()
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

    }

}

