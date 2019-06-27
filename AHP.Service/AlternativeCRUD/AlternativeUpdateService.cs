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
    class AlternativeUpdateService : IAlternativeUpdateService
    {
        IAlternativeRepository _altRepo;
        IUnitOfWorkFactory _unitOfWorkFactory;
        public AlternativeUpdateService(
            IAlternativeRepository altRepo,
            IUnitOfWorkFactory unitOfWorkFactory
            )
        {
            _altRepo = altRepo;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<IAlternativeModel> Update(IAlternativeModel alternative)
        {
            using (var uof = _unitOfWorkFactory.Create())
            {
                var _alternative = await _altRepo.GetByIDAsync(alternative.AlternativeID);
                _alternative.AlternativeName = alternative.AlternativeName;
                alternative = _altRepo.Add(alternative);
                await _altRepo.SaveAsync();
                uof.Commit();
                return alternative;
            }

        }



    }

}

