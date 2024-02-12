using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefConnect.Models;
using Microsoft.AspNetCore.Authorization;
using ChefConnect.Entities;
using Microsoft.AspNetCore.Identity;

namespace ChefConnect.Controllers;

[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private UserManager<AppUser> _userManager;
    private SignInManager<AppUser> _signInManager;

    public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        _signInManager.SignOutAsync();
        return View();
    }

    [AllowAnonymous]
    [HttpGet("/Login")]
    public async Task<IActionResult> Login()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost("/Login")]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (ModelState.IsValid)
        {
            var userByUserName = await _userManager.FindByNameAsync(loginViewModel.UserName);
            var userByEmail = await _userManager.FindByEmailAsync(loginViewModel.UserName);

            if (userByEmail == null)
            {
                if (userByUserName == null)
                {
                    ViewBag.LogIn = false;
                    ModelState.AddModelError("", "Invalid username/password.");
                    return View(loginViewModel);
                }
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password,
                     isPersistent: false, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        if (_userManager.IsInRoleAsync(userByUserName, "Chef").Result)
                        {
                            return RedirectToAction("ChefProfile", "Chef", new {username = userByUserName.UserName});
                        }
                        else
                        {
                            return RedirectToAction("CustomerHome", "Customer", new { username = userByUserName.UserName });
                        }
                    }

                }
            }
            else
            {
                var result = await _signInManager.PasswordSignInAsync(userByEmail.UserName, loginViewModel.Password,
                     isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (_userManager.IsInRoleAsync(userByEmail, "Chef").Result)
                    {
                        return RedirectToAction("ChefProfile", "Chef", new { username = userByEmail.UserName });
                    } 
                    else
                    {
                        return RedirectToAction("CustomerHome", "Customer", new { username = userByEmail.UserName });
                    }
                }
            }
        }
        return View();
    }

    [AllowAnonymous]
    [HttpGet("/Register")]
    public async Task<IActionResult> SelectRegistration()
    {
        return View();
    }

    [HttpGet("/Logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index");
    }

    [AllowAnonymous]
    public IActionResult Privacy()
    {
        return View();
    }

    [AllowAnonymous]
    public IActionResult About(string id)
    {

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

