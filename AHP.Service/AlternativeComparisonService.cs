using AHP.Model.Common;
using AHP.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Service
{
    class AlternativeComparisonService
    {
        IAlternativeComparisonRepository _altCompRepo;
        IUnitOfWorkFactory _unitOfWorkFactory;
        public AlternativeComparisonService(
            IAlternativeComparisonRepository altCompRepo,
            IUnitOfWorkFactory unitOfWorkFactory
            )
        {
            _altCompRepo = altCompRepo;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<List<IAlternativeComparisonModel>> AddAsync(List<IAlternativeComparisonModel> comparisons)
        {
            using (var uof = _unitOfWorkFactory.Create())
            {
                comparisons = _altCompRepo.AddRange(comparisons);
                await _altCompRepo.SaveAsync();
                uof.Commit();
            }
            return comparisons;

        }
    }
}
