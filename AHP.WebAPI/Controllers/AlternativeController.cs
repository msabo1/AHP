using AHP.Model;
using AHP.Model.Common;
using AHP.Service.Common;
using AHP.WebAPI.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHP.WebAPI.Controllers
{
    public class AlternativeController : Controller
    {
        IAlternativeService _alternativeService;
        IMapper _mapper;
        public AlternativeController(
            IMapper mapper,
            IAlternativeService alternativeService
          )
        {
            _mapper = mapper;
            _alternativeService = alternativeService;
        }

        public async Task<ActionResult> ListAlternatives(Guid choiceID)
        {
            ViewBag.Title = "Alterntatives";
            Session["ChoiceID"] = choiceID;

            var alternatives = await _alternativeService.GetAsync(choiceID, 1);
            var _alternatives = new List<AlternativeMvcModel>();
            foreach (IAlternativeModel alternative in alternatives)
            {
                _alternatives.Add(new AlternativeMvcModel { ChoiceID = alternative.ChoiceID, AlternativeName = alternative.AlternativeName, DateUpdated = (DateTime)alternative.DateUpdated});
            }

            return View(_alternatives);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Create a Choice";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(AlternativeMvcModel model)
        {
            if (ModelState.IsValid)
            {
                IAlternativeModel _alternative = new AlternativeModel { AlternativeName = model.AlternativeName, ChoiceID = (Guid)Session["ChoiceID"] };
                var status = await _alternativeService.AddAsync(_alternative);
                Guid _choiceid = status.ChoiceID;
                return RedirectToAction("ListAlternatives", "LoggedIn", new { choiceID = _choiceid });
            }
            return View();
        }

    }
}