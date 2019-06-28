
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
            var comparisonList = _mapper.Map<List<AltCompContModel>, List<IAlternativeComparisonModel>>(comparisons);
            var status = _alternativeComparisonService.AddAsync(comparisonList);
            return Ok(status);

        }

        //        //public async Task<IHttpActionResult> Get(GetPage request)
        //        //{

        //        //}


        //        //public async Task<IHttpActionResult> Put(AlternativeControllerModel alternative)
        //        //{

        //        //}


        //        //public async Task<IHttpActionResult> Delete(AlternativeControllerModel alternative)
        //        //{

        //        //}





        public class AltCompContModel
        {
            public System.Guid CriteriaID1 { get; set; }
            public System.Guid CriteriaID2 { get; set; }
            public double CriteriaRatio { get; set; }
        }
    }
}