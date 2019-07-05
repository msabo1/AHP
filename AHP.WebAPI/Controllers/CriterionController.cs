using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using AHP.Model.Common;
using AHP.Service.Common;
using System.Threading.Tasks;


namespace AHP.WebAPI.Controllers
{
    public class CriterionController : ApiController
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
        
        public async Task<IHttpActionResult> Post(List<CriterionControllerModel> criteria)
        {
            var _criteria = _mapper.Map<List<CriterionControllerModel>, List<ICriterionModel>>(criteria);
            var status = await _criterionService.AddAsync(_criteria);
            
            return Ok(_mapper.Map< List < ICriterionModel > ,List<CriterionControllerModel>>(status));
        }

        public async Task<IHttpActionResult> Delete(CriterionControllerModel criterion)
        {
            var _criterion = _mapper.Map<CriterionControllerModel, ICriterionModel>(criterion);
            var status = await _criterionService.DeleteAsync(_criterion);
            return Ok();
        }

        [HttpGet]
        [Route("api/criterion/get/{ID}/{page}")]
        public async Task<IHttpActionResult> Get(Guid ID, int page)
        {
      
            var status = await _criterionService.GetAsync(ID, page);
            return Ok(_mapper.Map<List<ICriterionModel>, List<CriterionControllerModel>>(status));
        }


        public async Task<IHttpActionResult> Put(CriterionControllerModel criterion)
        {
            var _criterion = _mapper.Map<CriterionControllerModel, ICriterionModel>(criterion);
            var status = await _criterionService.UpdateAsync(_criterion);

            return Ok();
        }


    }
    public class CriterionControllerModel
    {
        public System.Guid CriteriaID { get; set; }
        public string CriteriaName { get; set; }
        public Nullable<double> CriteriaScore { get; set; }
        public System.Guid ChoiceID { get; set; }

    }
}
