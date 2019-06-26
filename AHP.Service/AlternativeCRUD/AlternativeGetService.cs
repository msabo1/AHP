using AHP.Model.Common;
using AHP.Repository.Common;
using AHP.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Service
{
    class AlternativeGetService : IAlternativeGetService
    {
        IAlternativeRepository _altRepo;
        IUnitOfWorkFactory _unitOfWorkFactory;
        public AlternativeGetService(
            IAlternativeRepository altRepo,
            IUnitOfWorkFactory unitOfWorkFactory
            )
        {
            _altRepo = altRepo;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<IAlternativeModel> Get(IAlternativeModel alternative)
        {
            using (var uof = _unitOfWorkFactory.Create())
            {
                var _alternative = await _altRepo.GetByIDAsync(alternative.AlternativeID);
                uof.Commit();
                return _alternative;
            }

        }


    }
}
