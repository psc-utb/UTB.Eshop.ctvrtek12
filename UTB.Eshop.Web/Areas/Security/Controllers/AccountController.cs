using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UTB.Eshop.Web.Controllers;
using UTB.Eshop.Web.Models.ApplicationServices.Abstraction;
using UTB.Eshop.Web.Models.ViewModels;

namespace UTB.Eshop.Web.Areas.Security.Controllers
{
    [Area("Security")]
    public class AccountController : Controller
    {

        public AccountController()
        {

        }


        public IActionResult Register()
        {
            return View();
        }

        /*[HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {

        }*/


        public IActionResult Login()
        {
            return View();
        }

        /*[HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {

        }*/


        /*public async Task<IActionResult> Logout()
        {

        }*/

    }
}
