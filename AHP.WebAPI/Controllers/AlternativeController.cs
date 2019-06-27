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
    public class AlternativeController : ApiController
    {
        IAlternativeAddService _alternativeAdd;
        IAlternativeGetService _alternativeGet;
        IAlternativeUpdateService _alternativeUpdate;
        IAlternativeDeleteService _alternativeDelete;
        IMapper _mapper;
        public AlternativeController(
            IMapper mapper,
            IAlternativeUpdateService alternativeUpdate,
            IAlternativeAddService alternativeAdd,
            IAlternativeGetService alternativeGet,
            IAlternativeDeleteService alternativeDelete
          )
        {
            _mapper = mapper;
            _alternativeUpdate = alternativeUpdate;
            _alternativeAdd = alternativeAdd;
            _alternativeGet = alternativeGet;
            _alternativeDelete = alternativeDelete;
        }

        public async Task<IHttpActionResult> Post(AlternativeControllerModel alternative)
        {
            if (alternative.Equals(null))
            {
                return BadRequest();
            }

            var _alternative = _mapper.Map<AlternativeControllerModel, IAlternativeModel>(alternative);
            var status = await _alternativeAdd.AddAsync(_alternative);
            return Ok(_mapper.Map<IAlternativeModel, AlternativeControllerModel> (status));
        }

        public async Task<IHttpActionResult> Get(ChoiceControllerModel choice, int page = 1)
        {
            if (choice.Equals(null) || page < 1)
            {
                return BadRequest();
            }
            var _choice = _mapper.Map<ChoiceControllerModel, IChoiceModel>(choice);
            var status = await _alternativeGet.GetAsync(_choice, page);
            return Ok(_mapper.Map<IChoiceModel, ChoiceControllerModel>(status));
        }


        public async Task<IHttpActionResult> Put(AlternativeControllerModel alternative)
        {
            if (alternative.Equals(null))
            {
                return BadRequest();
            }

            var _alternative = _mapper.Map<AlternativeControllerModel, IAlternativeModel>(alternative);
            var status = await _alternativeUpdate.UpdateAsync(_alternative);
            return Ok(status);
        }


        public async Task<IHttpActionResult> Delete(AlternativeControllerModel alternative)
        {
            if (alternative.Equals(null))
            {
                return BadRequest();
            }

            var _alternative = _mapper.Map<AlternativeControllerModel, IAlternativeModel>(alternative);
            var status = await _alternativeDelete.DeleteAsync(_alternative);
            if (status)
            {
                return Ok(status);
            }
            else
            {
                return BadRequest();
            }
        }


    }
    public class AlternativeControllerModel
    {
        public ICollection<IAlternativeComparisonModel> AlternativeComparisons1 { get; set; }
        public ICollection<IAlternativeComparisonModel> AlternativeComparisons2 { get; set; }
        public System.Guid AlternativeID { get; set; }
        public string AlternativeName { get; set; }
        public Nullable<double> AlternativeScore { get; set; }
        public System.Guid ChoiceID { get; set; }
    }
}

