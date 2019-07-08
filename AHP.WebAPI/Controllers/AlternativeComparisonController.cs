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
    public class AlternativeComparisonController : Controller
    {
        IAlternativeComparisonService _alternativeComparisonService;
        IMapper _mapper;
        public AlternativeComparisonController(
            IMapper mapper,
            IAlternativeComparisonService alternativeComparisonService
          )
        {
            _mapper = mapper;
            _alternativeComparisonService = alternativeComparisonService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("ListAlternativeComparisons", "AlternativeComparison", new { page = Session["page"] });
        }

        public async Task<ActionResult> ListAlternativeComparisons(int page)
        {
            ViewBag.Title = "Alternative comparisons";
            if (page < 1) { page = 1; }
            Session["Page"] = page;

            var caltcomps = await _alternativeComparisonService.GetByAlternativeIdAsync((Guid)Session["AlternativeID"], page);
            var _caltcomps = new List<AlternativeComparisonMvcModel>();
            Guid altID = (Guid)Session["AlternativeID"];
            IDictionary<Guid, string> CritNames = (IDictionary<Guid, string>)Session["CriteriaNames"];
            IDictionary<Guid, string> AltNames = (IDictionary<Guid, string>)Session["AlternativesNames"];

            foreach (IAlternativeComparisonModel altcomp in caltcomps)
            {
                Guid altID1 = altcomp.AlternativeID1;
                Guid altID2 = altcomp.AlternativeID2;
                Guid critID = altcomp.CriteriaID;

                if (altID1 == altID)
                {
                    //OVO S NUOOM je samo za test
                    double newaltratio;
                    if (altcomp.AlternativeRatio != 0)
                    {
                        newaltratio = altcomp.AlternativeRatio;
                        //Pretvara u oblik koji je prihvatljiv sliderima
                        if (newaltratio < 1)
                        {
                            newaltratio = -(Math.Round(1 / altcomp.AlternativeRatio) - 1);
                        }
                        else
                        {
                            newaltratio -= 1;
                        }
                    }
                    else
                    {
                        newaltratio = 0;
                    }
                    AlternativeComparisonMvcModel ac = new AlternativeComparisonMvcModel
                    {
                        TempID = CritNames[critID] + AltNames[altID1],
                        CriteriaID = altcomp.CriteriaID,
                        CriteriaName = CritNames[critID],
                        AlternativeID1 = altID1,
                        AlternativeName1 = AltNames[altID1],
                        AlternativeID2 = altID2,
                        AlternativeName2 = AltNames[altID2],
                        Flipped = false,
                        AlternativeRatio = newaltratio
                    };

                    _caltcomps.Add(ac);
                }
                else
                {
                    double newaltratio;
                    if (altcomp.AlternativeRatio != 0)
                    {
                        newaltratio = 1 / altcomp.AlternativeRatio;
                        //OVO SA NULOM POSLJE OBRISAT
                        //Pretvara u oblik koji je prihvatljiv sliderima
                        if (newaltratio < 1)
                        {
                            newaltratio = -(Math.Round(1 / altcomp.AlternativeRatio) - 1);
                        }
                        else
                        {
                            newaltratio -= 1;
                        }
                    }
                    else
                    {
                        newaltratio = 0;
                    }

                    AlternativeComparisonMvcModel ac = new AlternativeComparisonMvcModel
                    {
                        TempID = CritNames[critID] + AltNames[altID1],
                        CriteriaID = altcomp.CriteriaID,
                        CriteriaName = CritNames[critID],
                        AlternativeID1 = altID2,
                        AlternativeName1 = AltNames[altID2],
                        AlternativeID2 = altID1,
                        AlternativeName2 = AltNames[altID1],
                        Flipped = true,
                        AlternativeRatio = newaltratio
                    };

                    _caltcomps.Add(ac);
                }
            }
            if (!_caltcomps.Any() && page > 1)
            {
                Session["Page"] = page - 1;
                return RedirectToAction("ListAlternativeComparisons", "AlternativeComparison", new { page = Session["Page"] });
            }

            return View(_caltcomps);
        }

        [HttpPost]
        public ActionResult ListAlternativeComparisons(List<AlternativeComparisonMvcModel> comps)
        {
            double newratio;
            foreach(var item in comps)
            {
                if ((bool)item.Flipped) {
                    if (item.AlternativeRatio == 0)
                    {
                        newratio = 1;
                    }
                    else if (item.AlternativeRatio < 0)
                    {
                        newratio = -(item.AlternativeRatio - 1);
                    }
                    else
                    {
                        newratio = item.AlternativeRatio + 1;
                        newratio = 1 / newratio;
                    }
                    IAlternativeComparisonModel _ac = new AlternativeComparisonModel { AlternativeID1 = item.AlternativeID2, AlternativeID2 = item.AlternativeID1, CriteriaID = item.CriteriaID, AlternativeRatio = item.AlternativeRatio};
                }
                else
                {
                    if (item.AlternativeRatio == 0)
                    {
                        newratio = 1;
                    }
                    else if (item.AlternativeRatio < 0)
                    {
                        newratio = -(item.AlternativeRatio - 1);
                        newratio = 1 / newratio;
                    }
                    else
                    {
                        newratio = item.AlternativeRatio + 1;
                    }
                    IAlternativeComparisonModel _ac = new AlternativeComparisonModel { AlternativeID1 = item.AlternativeID2, AlternativeID2 = item.AlternativeID1, CriteriaID = item.CriteriaID, AlternativeRatio = item.AlternativeRatio };
                }

            }
            
            return View();
        }

        //public ActionResult CreateCriterion()
        //{
        //    ViewBag.Title = "Create a Criterion";
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> CreateCriterion(CriterionMvcModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        ICriterionModel _criterion = new CriterionModel { ChoiceID = (Guid)Session["ChoiceID"], CriteriaName = model.CriteriaName };
        //        var status = await _criterionService.AddAsync(_criterion);
        //        Session["Message"] = "There are unfilled comparisons";
        //        return RedirectToAction("ListCriteria", "Criterion", new { page = Session["Page"] });
        //    }
        //    return View();
        //}

        //public async Task<ActionResult> DeleteCriterion(Guid criterionID)
        //{
        //    var criterion = await _criterionService.GetByIdAsync(criterionID);
        //    bool b = await _criterionService.DeleteAsync(criterion);
        //    return RedirectToAction("ListCriteria", "Criterion", new { page = Session["Page"] });
        //}
    }
}
