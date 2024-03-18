using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ChefConnect.Models;
using Microsoft.AspNetCore.Authorization;
using ChefConnect.Entities;
using Microsoft.AspNetCore.Identity;
using ChefConnect.Services;

namespace ChefConnect.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private UserManager<AppUser> _userManager;
    private SignInManager<AppUser> _signInManager;
    private readonly IWebHostEnvironment webHostEnvironment;
    private HelperServices HelperServices = new HelperServices();

    public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    
    public IActionResult Index()
    {
        if (_signInManager.IsSignedIn(User))
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
        return View();
    }

   
    [HttpGet("/Login")]
    public async Task<IActionResult> Login()
    {

        return View();
    }

    
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
                        if (!userByUserName.EmailConfirmed)
                        {
                            TempData["ConfirmEmailMessage"] = $"An email verification is sent to you. Please confirm your email there.";
                        }
                        if (_userManager.IsInRoleAsync(userByUserName, "Chef").Result)
                        {
                            return RedirectToAction("ChefProfile", "Chef", new {username = userByUserName.UserName});
                        }
                        else if(_userManager.IsInRoleAsync(userByUserName, "Admin").Result)
                        {
                            return RedirectToAction("AdminHome", "Admin");
                        }
                        else
                        {
                            return RedirectToAction("CustomerHome", "Customer", new { username = userByUserName.UserName });
                        }
                    }
                    else
                    {
                        ViewBag.LogIn = false;
                        ModelState.AddModelError("", "Invalid username/password.");
                        return View(loginViewModel);
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
                else
                {
                    ViewBag.LogIn = false;
                    ModelState.AddModelError("", "Invalid username/password.");
                    return View(loginViewModel);
                }
            }
        }
        else
        {
            return View();
        }
        
    }

    
    [HttpGet("/Register")]
    public async Task<IActionResult> SelectRegistration()
    {
        return View();
    }

    [Authorize(Roles = "Chef, Customer")]
    [HttpGet("/{username}/ResendVerfification")]
    public async Task<IActionResult> ResendVerificationMail(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        await HelperServices.SendVerificationEmailAsync(user.Email, user.UserName);
        TempData["EmailReSentMessage"] = "A new verification mail has been sent your mail, please confirm your email there.";

        return RedirectToAction("ChefProfile","Chef",new { username = user.UserName });
    }

    [HttpGet("/{username}/Email-Verification-Success")]
    public async Task<IActionResult> EmailVerificationSuccess(string username)
    {
        var user = await _userManager.FindByNameAsync(username);
        user.EmailConfirmed = true;
        await _userManager.UpdateAsync(user);
        
        return View();
    }

    [Authorize]
    [HttpGet("/Logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index");
    }

    
    public IActionResult Privacy()
    {
        return View();
    }

    
    public IActionResult Highlights()
    {
        return View();
    }

    
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

