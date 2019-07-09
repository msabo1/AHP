

using AHP.Model.Common;
using AHP.Service.Common;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace AHP.WebAPI.Controllers
{
    public class AlternativeComparisonController : ApiController
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

        /// <summary>
        /// Get method,
        /// /api/alternativecomparison/?criteriaID=&page=
        /// </summary>
        /// <param name="criteriaID"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        [Route("api/alternativecomparison/{criteriaID}/{page}")]
        public async Task<IHttpActionResult> Get(Guid criteriaID, int page)
        {
            if(criteriaID == null|| page < 1)
            {
                return BadRequest();
            }
            var status = await _alternativeComparisonService.GetAsync(criteriaID, alternativeID, page);

            return Ok(status);
        }

        /// <summary>
        /// Put method,
        /// /api/alternativecomparison/
        /// </summary>
        /// <param name="alternativeComps">list of alternative comparisons</param>
        /// <returns>Returns updated list of  AlternativeComparisonControllerModel</returns>
        public async Task<IHttpActionResult> Put(List<AlternativeComparisonControllerModel> alternativeComps)
        {
            var comparisonList = _mapper.Map<List<AlternativeComparisonControllerModel>, List<IAlternativeComparisonModel>>(alternativeComps);
            var status = await _alternativeComparisonService.UpdateAsync(comparisonList);
            return Ok(status);
        }


    
    }
 

    public class AlternativeComparisonControllerModel
    {

        public System.Guid CriteriaID { get; set; }
        public System.Guid AlternativeID1 { get; set; }
        public System.Guid AlternativeID2 { get; set; }
        public double AlternativeRatio { get; set; }
        public string AlternativeName1 { get; set; }
        public string AlternativeName2 { get; set; }
    }
}	
