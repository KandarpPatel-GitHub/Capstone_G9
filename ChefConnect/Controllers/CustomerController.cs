using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChefConnect.Entities;
using ChefConnect.Models;
using ChefConnect.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChefConnect.Controllers
{
    [Authorize(Roles ="Customer")]
    public class CustomerController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private HelperServices _helperServices = new HelperServices();

        public CustomerController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet("/Register/AsCustomer")]
        public async Task<IActionResult> CustomerRegister()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("/Register/AsCustomer")]
        public async Task<IActionResult> CustomerRegister(RegisterViewModel registerViewModel)
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

                        await _userManager.AddToRoleAsync(user, "Chef");
                        user.Email = registerViewModel.Email;
                        user.PhoneNumber = registerViewModel.PhoneNumber;
                        user.DateOfBirth = registerViewModel.DateOfBirth;
                        await _userManager.UpdateAsync(user);
                        await _signInManager.SignInAsync(user, false);
                        var message = $"\nHi,\n\nThanks for getting started with ChefConnect!\n\nWe need a little more information to complete your registration, including a confirmation of your email address.\n\nClick below to confirm your email address:\n\nhttps://localhost:7042/{registerViewModel.UserName}/Email-Verification-Success\n\nIf you have problems, please paste the above URL into your web browser.";
                        await _helperServices.SendEmailAsync(registerViewModel.Email, "Email Verification", message);
                        if (!user.EmailConfirmed)
                        {
                            TempData["ConfirmEmailMessage"] = $"An email verification is sent to you. Please confirm your email there.";
                        }

                        return RedirectToAction("Index","Home");


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

