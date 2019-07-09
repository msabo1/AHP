﻿using AHP.Model;
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
            return RedirectToAction("ListCriteria", "Criterion", new { page = Session["Page"] });
        }

        public async Task<ActionResult> ListCriteria(int page)
        {
            ViewBag.Title = "Criteria";
            if (page < 1) { page = 1; }
            Session["Page"] = page;

            var criteria = await _criterionService.GetAsync((Guid)Session["ChoiceID"], page);
            var _criteria = new List<CriterionMvcModel>();
            foreach (ICriterionModel criterion in criteria)
            {
                _criteria.Add(new CriterionMvcModel { CriteriaID = criterion.CriteriaID, ChoiceID = criterion.ChoiceID, CriteriaName = criterion.CriteriaName, DateUpdated = (DateTime)criterion.DateUpdated });
            }
            if (!_criteria.Any() && page > 1)
            {
                Session["Page"] = page - 1;
                return RedirectToAction("ListCriteria", "Criterion", new { page = Session["Page"] });
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
                var status = await _criterionService.AddAsync(_criterion);
                Session["Message"] = "There are unfilled comparisons";
                return RedirectToAction("ListCriteria", "Criterion", new { page = Session["Page"] });
            }
            return View();
        }

        public async Task<ActionResult> DeleteCriterion(Guid criterionID)
        {
            var criterion = await _criterionService.GetByIdAsync(criterionID);
            bool b = await _criterionService.DeleteAsync(criterion);
            return RedirectToAction("ListCriteria", "Criterion", new { page = Session["Page"] });
        }

        public async Task<ActionResult> EditCriterion(Guid criterionid)
        {
            var criteria = new List<ICriterionModel>();
            int page = 1;
            IDictionary<Guid, string> CritNames = new Dictionary<Guid, string>(10);
            do
            {
                criteria = await _criterionService.GetAsync((Guid)Session["ChoiceID"], page);
                page++;
                foreach (var crit in criteria)
                {
                    CritNames.Add(crit.CriteriaID, crit.CriteriaName);
                }
            } while (criteria.Count != 0);
            Session["CriteriaNames"] = CritNames;
            ViewBag.Title = "Edit a Criterion";
            var criterion = await _criterionService.GetByIdAsync(criterionid);
            Session["CriterionName"] = criterion.CriteriaName;
            Session["CriterionID"] = criterionid;
            Session["Page"] = 1;
            return RedirectToAction("ListCriteriaComparisons", "CriteriaComparison", new { page = Session["page"] });
        }
    }
}

