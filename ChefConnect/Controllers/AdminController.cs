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


        //Get Method to get all the reviews
        [HttpGet("/Admin/Reviews")]
        public async Task<IActionResult> GetAllPendingReviews()
        {
            var reviews = await _chefConnectDbContext.Reviews.Include(r => r.Customer).Include(r => r.ChefRecipe).ThenInclude(r => r.Chef).Where(r => r.Status == Entities.Reviews.ReviewStatus.Reported).ToListAsync();

            return View("AdminReview",reviews);
        }

    }
}

