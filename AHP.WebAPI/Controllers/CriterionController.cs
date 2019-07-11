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
        /// <summary>
        /// Post method,
        /// /api/criterion/
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns>Returns created CriterionControllerModel</returns>
        public async Task<IHttpActionResult> Post(CriterionControllerModel criteria)
        {
            var _criteria = _mapper.Map<CriterionControllerModel, ICriterionModel>(criteria);
            var status = await _criterionService.AddAsync(_criteria);
            
            return Ok(_mapper.Map< ICriterionModel  ,CriterionControllerModel>(status));
        }
        /// <summary>
        /// Delete method,
        /// /api/criterion/
        /// </summary>
        /// <param name="criterion"></param>
        /// <returns>Returns bool</returns>
        public async Task<IHttpActionResult> Delete(CriterionControllerModel criterion)
        {
            var _criterion = _mapper.Map<CriterionControllerModel, ICriterionModel>(criterion);
            var status = await _criterionService.DeleteAsync(_criterion);
            return Ok(status);
        }
        /// <summary>
        /// Get method,
        /// /api/criterion/?choiceID=&page=
        /// </summary>
        /// <param name="choiceID"></param>
        /// <param name="page"></param>
        /// <returns>Returns list of CriterionControllerModel</returns>
       [Route("api/criterion/{choiceID}/{page}")]
        public async Task<IHttpActionResult> Get(Guid choiceID, int page)
        {
      
            var status = await _criterionService.GetPageAsync(choiceID, page);
            return Ok(_mapper.Map<List<ICriterionModel>, List<CriterionControllerModel>>(status));
        }

        /// <summary>
        /// Put method,
        /// /api/criterion
        /// </summary>
        /// <param name="criterion"></param>
        /// <returns>Returns bool</returns>
        public async Task<IHttpActionResult> Put(CriterionControllerModel criterion)
        {
            var _criterion = _mapper.Map<CriterionControllerModel, ICriterionModel>(criterion);
            var status = await _criterionService.UpdateAsync(_criterion);

            return Ok(status);
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
