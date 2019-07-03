using AHP.Model;
using AHP.Model.Common;
using AHP.Service;
using AHP.Service.Common;
using AHP.WebAPI.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace AHP.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult SignUp()
        {
            ViewBag.Title = "User Sign Up";

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUp(UserMvcModel model)
        {
            if (ModelState.IsValid)
            {
                IUserService us = new UserService();
                IUserModel _userToCreate = new UserModel { Username = model.Username, Password = model.Password };
                
                return RedirectToAction("Index");
            }

            return View();
        }
    }

}
