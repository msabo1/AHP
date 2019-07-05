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
                    _caltcomps.Add(new AlternativeComparisonMvcModel { CriteriaID = altcomp.CriteriaID, CriteriaName = CritNames[critID],
                                                                       AlternativeID1 = altID1, AlternativeName1 = AltNames[altID1],
                                                                       AlternativeID2 = altID2, AlternativeName2 = AltNames[altID2],
                                                                       Flipped = false, AlternativeRatio = altcomp.AlternativeRatio});
                }
                else
                {
                    _caltcomps.Add(new AlternativeComparisonMvcModel
                    {
                        CriteriaID = altcomp.CriteriaID,
                        CriteriaName = CritNames[critID],
                        AlternativeID1 = altID2,
                        AlternativeName1 = AltNames[altID2],
                        AlternativeID2 = altID1,
                        AlternativeName2 = AltNames[altID1],
                        Flipped = true,
                        AlternativeRatio = 1 / altcomp.AlternativeRatio
                    });
                }
            }
            if (!_caltcomps.Any() && page > 1)
            {
                Session["Page"] = page - 1;
                return RedirectToAction("ListAlternativeComparisons", "AlternativeComparison", new { page = Session["Page"] });
            }

            return View(_caltcomps);
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
