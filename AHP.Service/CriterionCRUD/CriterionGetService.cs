using AHP.Model.Common;
using AHP.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Service.CriterionCRUD
{
    public class CriterionGetService
    {
        ICriterionRepository _critRepo;
        IUnitOfWorkFactory _unitOfWorkFactory;

        public CriterionGetService(ICriterionRepository critRepo, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _critRepo = critRepo;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        //public async Task<List<ICriterionModel>> GetAsync(Guid id)
        //{
        //    var criteria = await _critRepo.GetByIDAsync(id);
        //    return criteria;
        //}   
    }
}
