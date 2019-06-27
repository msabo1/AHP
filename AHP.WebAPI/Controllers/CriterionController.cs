using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using AHP.Model.Common;
using AHP.Service.Common;
using AutoMapper;
using System.Threading.Tasks;
using System.Web.Http;
using AHP.Service.Common.CriterionCRUDInterfaces;

namespace AHP.WebAPI.Controllers
{
    public class CriterionController : ApiController
    {
        ICriterionAddService _criterionAdd;
        ICriterionGetService _criterionGet;
        ICriterionUpdateService _criterionUpdate;
        ICriterionDeleteService _criterionDelete;
        IMapper _mapper;
        public CriterionController(
            IMapper mapper,
            ICriterionUpdateService criterionUpdate,
            ICriterionAddService criterionAdd,
            ICriterionGetService criterionGet,
            ICriterionDeleteService criterionDelete
          )
        {
            _mapper = mapper;
            _criterionUpdate = criterionUpdate;
            _criterionAdd = criterionAdd;
            _criterionGet = criterionGet;
            _criterionDelete = criterionDelete;
        }

        public async Task<IHttpActionResult> Post(List<CriterionControllerModel> criteria)
        {
            var _criteria = _mapper.Map<List<CriterionControllerModel>, List<ICriterionModel>>(criteria);
            var status = await _criterionAdd.AddAsync(_criteria);
            
            return Ok(_mapper.Map< List < ICriterionModel > ,List<CriterionControllerModel>>(status));
        }

    //    public async Task<bool> Delete(ICriterionModel criterion)
    //    {
    //        var _criterion = await _context.Criteria.FindAsync(criterion.CriteriaID);
    //        _context.Criteria.Remove(_criterion);
    //        return true;
    //    }

    //    public async Task<ICriterionModel> GetByID(params Guid[] idValues)
    //    {
    //        var criterion = await _context.Criteria.FindAsync(idValues[0]);
    //        return _mapper.Map<ICriterionModel>(criterion);
    //    }


    //    public async Task<ICriterionModel> Updatec(ICriterionModel criterion)
    //    {
    //        var _criterion = await _context.Criteria.FindAsync(criterion.CriteriaID);
    //        _context.Entry(_criterion).CurrentValues.SetValues(_mapper.Map<ICriterionModel>(criterion));
    //        return criterion;
    //    }


    }
    public class CriterionControllerModel
    {
        public System.Guid CriteriaID { get; set; }
        public string CriteriaName { get; set; }
        public Nullable<double> CriteriaScore { get; set; }
        public System.Guid ChoiceID { get; set; }

    }
}
