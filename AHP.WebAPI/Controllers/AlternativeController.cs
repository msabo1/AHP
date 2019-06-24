using AHP.Model.Common;
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
        IMapper _mapper;
        public AlternativeController(
            IAlternativeAddService alternativeAdd;
            IMapper mapper
            )
        {
            _alternativeAdd = alternativeAdd;
            _mapper = mapper;
           
        }


        [Route("Alternative/Add")]
        public async Task<IHttpActionResult> Post(AlternativeControllerModel alternative)
        {
            var _alternative = _mapper.Map<AlternativeControllerModel, IAlternativeModel>(alternative);
            var status = await _alternativeAdd.Add(_alternative);

            if (status != null)
                return Ok(_mapper.Map<IAlternativeModel, AlternativeControllerModel>(status));
            else
                return NotFound();
        }

    }
    public class AlternativeControllerModel
    {
        public ICollection<IAlternativeComparisonModel> AlternativeComparisons1 { get; set; }
        public System.Guid AlternativeID { get; set; }
        public string AlternativeName { get; set; }
        public Nullable<double> AlternativeScore { get; set; }
        public System.Guid ChoiceID { get; set; }
    }
}

