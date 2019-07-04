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
    public class CriterionController : Controller
    {
        ICriterionService _criterionService;
        IMapper _mapper;
        public CriterionController(
            IMapper mapper,
            ICriterionService criterionService
          )
        {
            _mapper = mapper;
            _criterionService = criterionService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("ListCriteria", "Criterion", new { ChoiceID = (Guid)Session["ChoiceID"] });
        }

        public async Task<ActionResult> ListCriteria(Guid choiceID)
        {
            Session["ChoiceID"] = choiceID;
            Session["Page"] = 1;

            var criteria = await _criterionService.GetAsync((Guid)Session["ChoiceID"], 1);
            var _criteria = new List<CriterionMvcModel>();
            foreach (ICriterionModel criterion in criteria)
            {
                _criteria.Add(new CriterionMvcModel { CriteriaID = criterion.CriteriaID, ChoiceID = criterion.ChoiceID, CriteriaName = criterion.CriteriaName, DateUpdated = (DateTime)criterion.DateUpdated });
            }

            return View(_criteria);
        }

        public async Task<ActionResult> ListCriteriaPage(int page)
        {
            ViewBag.Title = "Alterntatives";
            if (page < 1)
            {
                page = 1;
            }
            Session["Page"] = page;

            var criteria = await _criterionService.GetAsync((Guid)Session["ChoiceID"], page);
            var _criteria = new List<CriterionMvcModel>();
            foreach (ICriterionModel criterion in criteria)
            {
                _criteria.Add(new CriterionMvcModel { CriteriaID = criterion.CriteriaID, ChoiceID = criterion.ChoiceID, CriteriaName = criterion.CriteriaName, DateUpdated = (DateTime)criterion.DateUpdated });
            }

            return View(_criteria);
        }

        public ActionResult CreateCriterion()
        {
            ViewBag.Title = "Create a Criterion";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateCriterion(CriterionMvcModel model)
        {
            if (ModelState.IsValid)
            {
                ICriterionModel _criterion = new CriterionModel { ChoiceID = (Guid)Session["ChoiceID"], CriteriaName = model.CriteriaName };
                List < ICriterionModel > criterion = new List<ICriterionModel>();
                criterion.Add(_criterion);
                var status = await _criterionService.AddAsync(criterion);
                return RedirectToAction("ListCriteriaPage", "Criterion", new { page = Session["Page"] });
            }
            return View();
        }

        public async Task<ActionResult> DeleteCriterion(Guid criterionID)
        {
            var criterion = await _criterionService.GetByIdAsync(criterionID);
            bool b = await _criterionService.DeleteAsync(criterion);
            return RedirectToAction("ListCriteriaPage", "Criterion", new { page = Session["Page"] });
        }
    }
}

