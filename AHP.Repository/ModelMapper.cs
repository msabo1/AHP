using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Repository
{
    class ModelMapper<ModelT, DALT>
    {
        public DALT ModelToDAL(ModelT model)
        {
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<ModelT, DALT>();

            });
            DALT entity = config.CreateMapper().Map<DALT>(model);
            return entity;
        }
    }
}
