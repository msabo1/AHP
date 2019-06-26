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
        IMapper _mapper;
        public AlternativeController(
            IMapper mapper,
            IAlternativeAddService alternativeAdd,
            IAlternativeGetService alternativeGet
          )
        {
            _mapper = mapper;
            _alternativeAdd = alternativeAdd;
            _alternativeGet = alternativeGet;
        }

        public async Task<IHttpActionResult> Post(AlternativeControllerModel alternative)
        {
            var _alternative = _mapper.Map<AlternativeControllerModel, IAlternativeModel>(alternative);
            var status = await _alternativeAdd.Add(_alternative);
            return Ok(_mapper.Map<IAlternativeModel, AlternativeControllerModel> (status));
        }

        public async Task<IHttpActionResult> Get(AlternativeControllerModel alternative)
        {
            var _alternative = _mapper.Map<AlternativeControllerModel, IAlternativeModel>(alternative);
            var status = await _alternativeGet.Get(_alternative);
            return Ok(_mapper.Map<IAlternativeModel, AlternativeControllerModel>(status));
        }


        //public async Task<IHttpActionResult> Put(AlternativeControllerModel alternative)
        //{
        //    var _alternative = _mapper.Map<AlternativeControllerModel, IAlternativeModel>(alternative);
        //    var status = await _alternativeUpdate.Update(_alternative);
        //}


        //public async Task<IHttpActionResult> Delete(AlternativeControllerModel user)
        //{
        //    var _user = _mapper.Map<AlternativeControllerModel, IAlternativeModel>(user);
        //}


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

