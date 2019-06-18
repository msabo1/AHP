using AHP.DAL;
using AHP.Model;
using AHP.Repository.Common;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Repository
{
    public class ModelMapperProfile : Profile
    {
        public ModelMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserModel, User>();

            CreateMap<Choice, ChoiceModel>();
            CreateMap<ChoiceModel, Choice>();

            CreateMap<Criterion, CriterionModel>();
            CreateMap<CriterionModel, UserModel>();

            CreateMap<Alternative, AlternativeModel>();
            CreateMap<AlternativeModel, Alternative>();

            CreateMap<CriteriaComparison, CriteriaComparisonModel>();
            CreateMap<CriteriaComparisonModel, CriteriaComparison>();

            CreateMap<AlternativeComparison, AlternativeComparisonModel>();
            CreateMap<AlternativeComparisonModel, AlternativeComparison>();


        }
    }
}

