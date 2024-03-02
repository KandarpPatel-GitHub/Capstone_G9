using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ChefConnect.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChefConnect.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        public readonly ChefConnectDbContext _chefConnectDbContext;

        public AdminController(ChefConnectDbContext chefConnectDbContext)
        {
            _chefConnectDbContext = chefConnectDbContext;
        }

        [HttpGet("/Admin/Home")]
        public async Task<IActionResult> AdminHome()
        {
            return View();
        }
    }
}

