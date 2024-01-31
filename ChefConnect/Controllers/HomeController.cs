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
        if (_signInManager.IsSignedIn(this.User))
        {
            Console.WriteLine("User is logged in");
        }
        return View();
    }

    [AllowAnonymous]
    [HttpGet("/Login")]
    public async Task<IActionResult> Login()
    {
        return View();
    }

    [AllowAnonymous]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

