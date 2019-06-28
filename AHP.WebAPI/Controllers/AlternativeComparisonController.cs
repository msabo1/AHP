
//using AHP.Model.Common;
//using AutoMapper;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web.Http;

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



        public async Task<IHttpActionResult> Post(List<AltCompContModel> comparisons)
        {
            foreach(AltCompContModel comparison in comparisons)
            {
                if(comparison == null)
                {
                    return BadRequest();
                }
            }
            var comparisonList = _mapper.Map<List<AltCompContModel>, List<IAlternativeComparisonModel>>(comparisons);
            var status = await _alternativeComparisonService.AddAsync(comparisonList);
            return Ok(status);

        }

        public async Task<IHttpActionResult> Get(AltCompRequest request)
        {
            if(request == null)
            {
                return BadRequest();
            }
            var page = request.page;
            var alternativeId = request.alternativeId;
            var criteriaId = request.criteriaId;
            var status = await _alternativeComparisonService.GetAsync(alternativeId, criteriaId, page);

            return Ok(status);
        }


        public async Task<IHttpActionResult> Put(List<AltCompContModel> alternativeComps)
        {
            var comparisonList = _mapper.Map<List<AltCompContModel>, List<IAlternativeComparisonModel>>(alternativeComps);
            var status = await _alternativeComparisonService.UpdateAsync(comparisonList);
            return Ok();
        }


        public class AltCompRequest
        {
            public Guid criteriaId;
            public Guid alternativeId;
            public int page;
        }

        public class AltCompContModel
        {

            public System.Guid CriteriaID { get; set; }
            public System.Guid AlternativeID1 { get; set; }
            public System.Guid AlternativeID2 { get; set; }
            public double AlternativeRatio { get; set; }
        }
    }
}