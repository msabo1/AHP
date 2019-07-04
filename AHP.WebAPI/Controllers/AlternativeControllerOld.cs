﻿using AHP.Model.Common;
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
    public class AlternativeControllerOld : ApiController
    {
        IAlternativeService _alternativeService;
        IMapper _mapper;
        public AlternativeControllerOld(
            IMapper mapper,
            IAlternativeService alternativeService
          )
        {
            _mapper = mapper;
            _alternativeService = alternativeService;
        }

        public async Task<IHttpActionResult> Post(AlternativeControllerModel alternative)
        {
            if (alternative==null)
            {
                return BadRequest();
            }

            var _alternative = _mapper.Map<AlternativeControllerModel, IAlternativeModel>(alternative);
            var status = await _alternativeService.AddAsync(_alternative);
            return Ok(_mapper.Map<IAlternativeModel, AlternativeControllerModel>(status));
        }

        public async Task<IHttpActionResult> Get(GetPage request)
        {
            if(request == null)
            {
                return BadRequest();
            }
            int page = request.page;
            Guid choiceId = request.Id;
            if (choiceId.Equals(null) || page < 1)
            {
                return BadRequest();
            }

            var status = await _alternativeService.GetAsync(choiceId, page);
            if (status.Any())
            {
                return Ok(_mapper.Map<List<IAlternativeModel>, List<AlternativeControllerModel>>(status));
            }
            else
            {
                return BadRequest();
            }
        }


        public async Task<IHttpActionResult> Put(AlternativeControllerModel alternative)
        {
            if (alternative==null)
            {
                return BadRequest();
            }

            var _alternative = _mapper.Map<AlternativeControllerModel, IAlternativeModel>(alternative);
            var status = await _alternativeService.UpdateAsync(_alternative);
            return Ok(status);
        }


        public async Task<IHttpActionResult> Delete(AlternativeControllerModel alternative)
        {
            if (alternative == null)
            {
                return BadRequest();
            }

            var _alternative = _mapper.Map<AlternativeControllerModel, IAlternativeModel>(alternative);
            var status = await _alternativeService.DeleteAsync(_alternative);
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
    public class GetPage{
        public Guid Id;
        public int page;
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
