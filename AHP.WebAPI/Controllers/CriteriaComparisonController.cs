using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHP.WebAPI.Controllers
{
    public class CriteriaComparisonController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult FillInComparisons()
        {
            return View();
        }
    }
}