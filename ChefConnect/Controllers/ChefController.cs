﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChefConnect.Models;
using Microsoft.AspNetCore.Identity;
using ChefConnect.Entities;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChefConnect.Controllers
{
    [Authorize]
    public class ChefController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;

        public ChefController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        [HttpGet("/Chef/Register")]
        public async Task<IActionResult> ChefRegister()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("/Chef/Register")]
        public async Task<IActionResult> ChefRegister(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                if (_userManager.FindByNameAsync(registerViewModel.UserName).Result != null)
                {
                    ModelState.AddModelError("", "Username already in use please try a new one.");
                    return View();
                }
                else
                {
                    if (_userManager.FindByEmailAsync(registerViewModel.Email).Result != null)
                    {
                        ModelState.AddModelError("", "Email already in use please try a new one.");
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
                            return RedirectToAction("Index", "Home");

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
        

            return View();
        }

    }

}

