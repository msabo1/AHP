using AHP.Model.Common;
using AHP.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHP.Service.Common;

namespace AHP.Service
{
    class AlternativeAddService : IAlternativeAddService
    {
        IAlternativeRepository _altRepo;
        IUnitOfWorkFactory _unitOfWorkFactory;
        public AlternativeAddService(
            IAlternativeRepository altRepo,
            IUnitOfWorkFactory unitOfWorkFactory
            )
        {
            _altRepo = altRepo;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<IAlternativeModel> Add(IAlternativeModel alternative)
        {
            alternative.AlternativeID = Guid.NewGuid();
            alternative.DateCreated = DateTime.Now;
            alternative.DateUpdated = DateTime.Now;

            using (var uof = _unitOfWorkFactory.Create())
            {
                alternative = _altRepo.Add(alternative);
                await _altRepo.SaveAsync();
                uof.Commit();
            }
            return alternative;
        }



    }
}
