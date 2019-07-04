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
    public class LoggedInController : Controller
    {
        IChoiceService _choiceService;
        IMapper _mapper;

        public LoggedInController( IMapper mapper, IChoiceService choiceService)
        {
            _mapper = mapper;
            _choiceService = choiceService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> ListChoices()
        {
            Guid userID = (Guid) this.Session["UserID"];
            var choices = await _choiceService.GetAsync(userID, 1);
            var _choices = new List<ChoiceMvcModel>();
            foreach(IChoiceModel choice in choices)
            {
                _choices.Add(new ChoiceMvcModel {ChoiceID = choice.ChoiceID, Name = choice.ChoiceName, DateUpdated = (DateTime) choice.DateUpdated, UserID = choice.UserID});
            }
            return View(_choices);
        }

        public ActionResult CreateChoice()
        {
            ViewBag.Title = "Create a Choice";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateChoice(ChoiceMvcModel model)
        {
            if (ModelState.IsValid)
            {
                IChoiceModel _choice = new ChoiceModel { ChoiceName = model.Name, UserID = (Guid)Session["UserID"] };
                var status = await _choiceService.CreateAsync(_choice);
                Guid _userid = status.UserID;
                return RedirectToAction("ListChoices", "LoggedIn", new { userid = _userid });
            }
            return View();
        }

        public async Task<ActionResult> DeleteChoice(Guid choiceID)
        {
            var choice = await _choiceService.GetByIdAsync(choiceID);
            bool b = await _choiceService.DeleteAsync(choice);
            return RedirectToAction("ListChoices", "LoggedIn", new { userid = choice.UserID });
        }
    }


}