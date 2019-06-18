using AHP.Repository.Common;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Repository
{
    class EntityModelMapper<ModelT, EntityT>: IEntityModelMapper<ModelT, EntityT>
    {
        public ModelT EntityToModel(EntityT entity)
        {
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<EntityT, ModelT>();

            });
            return config.CreateMapper().Map<ModelT>(entity);
        }

        public EntityT ModelToEntity(ModelT model)
        {
            var config = new MapperConfiguration(cfg => {

                cfg.CreateMap<ModelT, EntityT>();

            });
             return config.CreateMapper().Map<EntityT>(model);
        }
    }
}
