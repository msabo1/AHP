using AHP.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHP.WebAPI.Controllers
{
    public class LoggedInController : Controller
    {
        // GET: LoggedIn
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListChoices()
        {
            List<ChoiceMvcModel> choices = new List<ChoiceMvcModel>();

            choices.Add(new ChoiceMvcModel{ ChoiceID = Guid.NewGuid(), UserID = Guid.NewGuid(), Name = "Nekretnine", DateUpdated = DateTime.Now });
            choices.Add(new ChoiceMvcModel { ChoiceID = Guid.NewGuid(), UserID = Guid.NewGuid(), Name = "Olovke", DateUpdated = DateTime.Now });
            choices.Add(new ChoiceMvcModel { ChoiceID = Guid.NewGuid(), UserID = Guid.NewGuid(), Name = "Plaze", DateUpdated = DateTime.Now });
            choices.Add(new ChoiceMvcModel { ChoiceID = Guid.NewGuid(), UserID = Guid.NewGuid(), Name = "Patike", DateUpdated = DateTime.Now });
            return View(choices);
        }
    }


}