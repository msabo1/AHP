using AHP.Model.Common;
using AHP.Service.Common.Choice_CRUD_Interfaces;
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
    public class ChoiceController : ApiController
    {
        IChoiceReadService _choiceRead;
        IChoiceCreateService _choiceCreate;
        IChoiceUpdateService _choiceUpdate;
        IChoiceDeleteService _choiceDelete;
        IMapper _mapper;

        public ChoiceController(
            IMapper mapper, 
            IChoiceReadService choiceRead, 
            IChoiceCreateService choiceCreate, 
            IChoiceUpdateService choiceUpdate, 
            IChoiceDeleteService choiceDelete)
        {
            _mapper = mapper;
            _choiceCreate = choiceCreate;
            _choiceRead = choiceRead;
            _choiceUpdate = choiceUpdate;
            _choiceDelete = choiceDelete;
            
        }

        [Route("Choice/Create")]
        public async Task<IHttpActionResult> Post(ChoiceControllerModel choice)
        {
            var _choice = _mapper.Map<ChoiceControllerModel, IChoiceModel>(choice);
            var status = await _choiceCreate.Check(_choice);

            if(status != null)
            {
                return Ok(_mapper.Map<IChoiceModel, ChoiceControllerModel>(status));
            }
            else
            {
                return NotFound();
            }
        }

        [Route("Choice/Read")]
        public async Task<IHttpActionResult> Get(ChoiceControllerModel choice)
        {
            var _choice = _mapper.Map<ChoiceControllerModel, IChoiceModel>(choice);
            var status = await _choiceRead.Check(_choice.ChoiceID);

            if (status != null)
            {
                return Ok(_mapper.Map<IChoiceModel, ChoiceControllerModel>(status));
            }
            else
            {
                return NotFound();
            }
        }

        [Route("Choice/Update")]
        public async Task<IHttpActionResult> Put(ChoiceControllerModel choice)
        {
            var _choice = _mapper.Map<ChoiceControllerModel, IChoiceModel>(choice);
            var status = await _choiceUpdate.Update(_choice);

            if (status != null)
            {
                return Ok(_mapper.Map<IChoiceModel, ChoiceControllerModel>(status));
            }
            else
            {
                return NotFound();
            }
        }

        [Route("Choice/Delete")]
        public async Task<IHttpActionResult> Delete(ChoiceControllerModel choice)
        {
            var _choice = _mapper.Map<ChoiceControllerModel, IChoiceModel>(choice);
            var status = await _choiceDelete.Delete(_choice);

            if (status)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }


    }

    public class ChoiceControllerModel
    {
        public System.Guid ChoiceID { get; set; }
        public string ChoiceName { get; set; }
        public System.Guid UserID { get; set; }
        public virtual ICollection<ICriterionModel> Criteria { get; set; }
        public virtual ICollection<IAlternativeModel> Alternatives { get; set; }
    }
}
