using AHP.Model.Common;
using AHP.Service.Common;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AHP.WebAPI.Controllers
{
    public class CriteriaComparisonController : ApiController
    {
        ICriteriaComparisonService _criteriaComparisonService;
        IMapper _mapper;

        public CriteriaComparisonController(IMapper mapper, ICriteriaComparisonService criteriaComparisonService)
        {
            _mapper = mapper;
            _criteriaComparisonService = criteriaComparisonService;
        }

        public async Task<IHttpActionResult> Post(List<CriteriaComparisonControllerModel> criteriaComparisons)
        {
            if(criteriaComparisons == null)
            {
                return BadRequest();
            }
            foreach(var comparison in criteriaComparisons)
            {
                if (comparison == null)
                {
                    return BadRequest();
                }
            }
            

            var _criteriaComparisons = _mapper.Map<List<CriteriaComparisonControllerModel>, List<ICriteriaComparisonModel>>(criteriaComparisons);
            var status = await _criteriaComparisonService.AddAsync(_criteriaComparisons);
            return Ok(_mapper.Map<List<ICriteriaComparisonModel>, List<CriteriaComparisonControllerModel>>(status));
        }

        [HttpGet]
        [Route("api/criteriaComparison/get/{ID}/{page}")]
        public async Task<IHttpActionResult> Get(Guid ID, int page)
        {
            if (ID == null || page < 1)
            {
                return BadRequest();
            }

            var status = await _criteriaComparisonService.GetAsync(ID, page);
            
            return Ok(_mapper.Map<List<ICriteriaComparisonModel>, List<CriteriaComparisonControllerModel>>(status));
            
        }

        public async Task<IHttpActionResult> Put(List<CriteriaComparisonControllerModel> criteriaComparisons)
        {
            if (criteriaComparisons == null)
            {
                return BadRequest();
            }
            foreach (var comparison in criteriaComparisons)
            {
                if (comparison == null)
                {
                    return BadRequest();
                }
            }

            var _criteriaComparisons = _mapper.Map<List<CriteriaComparisonControllerModel>, List<ICriteriaComparisonModel>>(criteriaComparisons);
            var status = await _criteriaComparisonService.UpdateAsync(_criteriaComparisons);
            return Ok(status);
        }
    }

    public class CriteriaComparisonControllerModel
    {
        public Guid CriteriaID1 { get; set; }
        public Guid CriteriaID2 { get; set; }
        public double CriteriaRatio { get; set; }
    }

    public class CritCompGetModel
    {
        public Guid Id;
        public int page;
    }
}
