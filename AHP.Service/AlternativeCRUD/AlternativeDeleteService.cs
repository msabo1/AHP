using AHP.Model.Common;
using AHP.Repository.Common;
using AHP.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Service.AlternativeCRUD
{
    class AlternativeDeleteService : IAlternativeDeleteService
    {
        IAlternativeRepository _altRepo;
        IUnitOfWorkFactory _unitOfWorkFactory;
        public AlternativeDeleteService(
            IAlternativeRepository altRepo,
            IUnitOfWorkFactory unitOfWorkFactory
            )
        {
            _altRepo = altRepo;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<bool> DeleteAsync(IAlternativeModel alternative)
        {
            using (var uof = _unitOfWorkFactory.Create())
            {
                var deleted = await _altRepo.DeleteAsync(alternative);
                await _altRepo.SaveAsync();
                uof.Commit();
                return deleted;
            }
        }
    }
}
