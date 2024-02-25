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


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChefConnect.Controllers
{
    [Authorize(Roles ="Chef")]
    public class ChefController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private HelperServices _helperSerivces = new HelperServices();

        public ChefController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
                    ModelState.AddModelError("UserName", "Username already in use please try a new one.");
                    return View();
                }
                else
                {
                    if (_userManager.FindByEmailAsync(registerViewModel.Email).Result != null)
                    {
                        ModelState.AddModelError("Email", "Email already in use please try a new one.");
                        return View();
                    }
                    else
                    {
                        if (!_helperSerivces.IsValidAge(registerViewModel.DateOfBirth))
                        {
                            ModelState.AddModelError("DateOfBirth", "You must be 18 years or more to register.");
                            return View();
                           
                        }
                        else
                        {
                            if (!_helperSerivces.IsPhoneNumberValid(registerViewModel.PhoneNumber))
                            {
                                ModelState.AddModelError("PhoneNumber", "Please enter a valid phone number.");
                                return View();
                            }
                            else
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
                                    await _signInManager.SignInAsync(user,false);
                                    await _helperSerivces.SendVerificationEmailAsync(registerViewModel.Email, registerViewModel.UserName);
                                    if (!user.EmailConfirmed)
                                    {
                                        TempData["LastActionMessage"] = "An email verification is sent to you. Please confirm your email there.";
                                    }
                                    
                                    return RedirectToAction("ChefProfile", new {username = registerViewModel.UserName});
                                   

                                }
                                else
                                {
                                    foreach (var error in result.Errors)
                                    {
                                        ModelState.AddModelError("", error.Description);
                                    }

                                }
                            }
                        }
                        
                    }
                }

            }
        

            return View();
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

    }

}

