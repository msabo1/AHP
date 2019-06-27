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
    public class CriterionDeleteService : ICriterionDeleteService
    {
        ICriterionRepository _critDelete;
        IUnitOfWorkFactory _unitOfWorkFactory;

        public CriterionDeleteService(ICriterionRepository critDelete, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _critDelete = critDelete;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<bool> Delete(ICriterionModel criteria)
        {
            using (var uof = _unitOfWorkFactory.Create())
            {
                var deleted = await _critDelete.DeleteAsync(criteria);
                await _critDelete.SaveAsync();
                uof.Commit();
                return deleted;
            }
        }
    }
}
