

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



        public async Task<IHttpActionResult> Post(List<AlternativeComparisonControllerModel> comparisons)
        {
            foreach(AlternativeComparisonControllerModel comparison in comparisons)
            {
                if(comparison == null)
                {
                    return BadRequest();
                }
            }
            var comparisonList = _mapper.Map<List<AlternativeComparisonControllerModel>, List<IAlternativeComparisonModel>>(comparisons);
            var status = await _alternativeComparisonService.AddAsync(comparisonList);
            return Ok(status);

        }
        [Route("api/alternativecomparison/{criteriaID}/{page}")]
        public async Task<IHttpActionResult> Get(Guid criteriaID, int page)
        {
            if(criteriaID == null|| page < 1)
            {
                return BadRequest();
            }
            var status = await _alternativeComparisonService.GetAsync(criteriaID, page);

            return Ok(status);
        }


        public async Task<IHttpActionResult> Put(List<AlternativeComparisonControllerModel> alternativeComps)
        {
            var comparisonList = _mapper.Map<List<AlternativeComparisonControllerModel>, List<IAlternativeComparisonModel>>(alternativeComps);
            var status = await _alternativeComparisonService.UpdateAsync(comparisonList);
            return Ok();
        }


    
    }
    public class AltCompRequest
    {
        public Guid criteriaID;
        public Guid alternativeID;
        public int page;
    }

    public class AlternativeComparisonControllerModel
    {

        public System.Guid CriteriaID { get; set; }
        public System.Guid AlternativeID1 { get; set; }
        public System.Guid AlternativeID2 { get; set; }
        public double AlternativeRatio { get; set; }
    }
}	