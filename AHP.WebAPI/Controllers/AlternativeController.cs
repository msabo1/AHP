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

        public ActionResult Index()
        {
            return RedirectToAction("ListAlternatives", "Alternative", new {ChoiceID = (Guid)Session["ChoiceID"] });
        }

        public async Task<ActionResult> ListAlternatives(Guid choiceID)
        {
            Session["ChoiceID"] = choiceID;
            Session["Page"] = 1;
            
            var alternatives = await _alternativeService.GetAsync((Guid)Session["ChoiceID"], 1);
            var _alternatives = new List<AlternativeMvcModel>();
            foreach (IAlternativeModel alternative in alternatives)
            {
                _alternatives.Add(new AlternativeMvcModel { AlternativeID = alternative.AlternativeID, ChoiceID = alternative.ChoiceID, AlternativeName = alternative.AlternativeName, DateUpdated = (DateTime)alternative.DateUpdated});
            }

            return View(_alternatives);
        }

        public async Task<ActionResult> ListAlternativesPage(int page)
        {
            ViewBag.Title = "Alterntatives";
            if (page < 1)
            {
                page = 1;
            }
            Session["Page"] = page;

            var alternatives = await _alternativeService.GetAsync((Guid)Session["ChoiceID"], page);
            var _alternatives = new List<AlternativeMvcModel>();
            foreach (IAlternativeModel alternative in alternatives)
            {
                _alternatives.Add(new AlternativeMvcModel { AlternativeID = alternative.AlternativeID, ChoiceID = alternative.ChoiceID, AlternativeName = alternative.AlternativeName, DateUpdated = (DateTime)alternative.DateUpdated });
            }

            return View(_alternatives);
        }

        public ActionResult CreateAlternative()
        {
            ViewBag.Title = "Create a Choice";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAlternative(AlternativeMvcModel model)
        {
            if (ModelState.IsValid)
            {
                IAlternativeModel _alternative = new AlternativeModel { ChoiceID = (Guid)Session["ChoiceID"], AlternativeName = model.AlternativeName};
                var status = await _alternativeService.AddAsync(_alternative);
                Guid _choiceid = status.ChoiceID;
                return RedirectToAction("ListAlternativesPage", "Alternative", new { page = Session["Page"] });
            }
            return View();
        }

        public async Task<ActionResult> DeleteAlternative(Guid alternativeID)
        {
            var alternative = await _alternativeService.GetByIdAsync(alternativeID);
            bool b = await _alternativeService.DeleteAsync(alternative);
            return RedirectToAction("ListAlternativesPage", "Alternative", new { page = Session["Page"] });
        }

    }
}