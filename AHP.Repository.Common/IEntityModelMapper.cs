using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Repository.Common
{
    public interface IEntityModelMapper<ModelT, EntityT>
    {
        EntityT ModelToEntity(ModelT model);
        ModelT EntityToModel(EntityT entity);
    }
}
