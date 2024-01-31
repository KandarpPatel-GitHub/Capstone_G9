using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChefConnect.Entities;
using ChefConnect.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChefConnect.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;

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

                            await _userManager.AddToRoleAsync(user, "Customer");
                            user.Email = registerViewModel.Email;
                            user.PhoneNumber = registerViewModel.PhoneNumber;
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

