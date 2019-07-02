using AHP.Model.Common;
using AHP.Repository.Common;
using AHP.Service.Common.CriterionCRUDInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Service.CriterionCRUD
{
    public class CriterionAddService : ICriterionAddService
    {
        ICriterionRepository _critRepo;
        IUnitOfWorkFactory _unitFactory;

        public CriterionAddService(ICriterionRepository critRepo, IUnitOfWorkFactory unitFactory) {
            _critRepo = critRepo;
            _unitFactory = unitFactory;
        }

        public async Task<List<ICriterionModel>> AddAsync(List<ICriterionModel> criteria)
        {
            foreach (ICriterionModel criterion in criteria)
            {
                criterion.CriteriaID = Guid.NewGuid();
                criterion.DateCreated = DateTime.Now;
                criterion.DateUpdated = DateTime.Now;
            }
            using (var uof = _unitFactory.Create())
            {
                criteria = _critRepo.AddRange(criteria);
                await _critRepo.SaveAsync();
                uof.Commit();
            }
            return criteria;
        }
    }
}