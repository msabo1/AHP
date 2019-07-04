using AHP.Model;
using AHP.Model.Common;
using AHP.Service;
using AHP.Service.Common;
using AHP.WebAPI.Models;
using AutoMapper;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHP.WebAPI.Controllers
{
    public class HomeController : Controller
    {
        

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        
    }

}
