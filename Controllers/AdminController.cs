using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityTestProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityTestProject.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        public AdminController(UserManager<ApplicationUser> _userManager)
        {
            userManager = _userManager;
        }
        public IActionResult Index()
        {
            return View(userManager.Users);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RegisterModel user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser Createuser = new ApplicationUser();
                Createuser.UserName = user.UserName;
                Createuser.Email = user.Email;
                var result = await userManager.CreateAsync(Createuser,user.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(user);
        }
    }
}
