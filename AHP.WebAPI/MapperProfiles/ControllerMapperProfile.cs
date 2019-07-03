using AHP.DAL;
using AHP.Model.Common;
using AHP.WebAPI.Controllers;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHP.WebAPI.MapperProfiles
{
    public class ControllerMapperProfile : Profile
    {
        public ControllerMapperProfile(){
        CreateMap<UserControllerModel, IUserModel>();
        CreateMap<IUserModel, UserControllerModel>();

        CreateMap<AlternativeControllerModel, IAlternativeModel>();
        CreateMap<IAlternativeModel, AlternativeControllerModel>();

        CreateMap<ChoiceControllerModel, IChoiceModel>();
        CreateMap<IChoiceModel, ChoiceControllerModel>();

        CreateMap<CriterionControllerModel, ICriterionModel>();
        CreateMap<ICriterionModel, CriterionControllerModel >();

        CreateMap<CriteriaComparisonControllerModel,ICriteriaComparisonModel>();
        CreateMap<ICriteriaComparisonModel, CriteriaComparisonControllerModel>();

        CreateMap<AlternativeComparisonControllerModel, IAlternativeComparisonModel>();
        CreateMap<IAlternativeComparisonModel, AlternativeComparisonControllerModel>();
        }
    }
}