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
    class AlternativeComparisonService : IAlternativeComparisonService
    {
        IAlternativeComparisonRepository _altCompRepo;
        IAlternativeRepository _alternativeRepo;
        IUnitOfWorkFactory _unitFactory;
        public AlternativeComparisonService(
            IAlternativeComparisonRepository altCompRepo,
            IAlternativeRepository alternativeRepo,
            IUnitOfWorkFactory unitOfWorkFactory
            )
        {
            _altCompRepo = altCompRepo;
            _alternativeRepo = alternativeRepo;
            _unitFactory = unitOfWorkFactory;
        }


        /// <summary>
        /// Read method,
        /// gets alternative comparisons and sets their alternative names
        /// </summary>
        /// <param name="criteriaId"></param>
        /// <param name="page"></param>
        /// <returns>Returns list of AlternativeComparisonModel</returns>
        public async Task<List<IAlternativeComparisonModel>> GetAsync(Guid criteriaId, int page = 1)
        {
            var alternatives = await _altCompRepo.GetByCriteriaIDAsync(criteriaId);
            foreach(var comparison in alternatives)
            {
                comparison.AlternativeName1 = (await _alternativeRepo.GetByIDAsync(comparison.AlternativeID1)).AlternativeName;
                comparison.AlternativeName2 = (await _alternativeRepo.GetByIDAsync(comparison.AlternativeID2)).AlternativeName;
            }

            return alternatives;
        }
        /// <summary>
        /// Update method,
        /// updates alternative comparisons ratio
        /// </summary>
        /// <param name="comparisons"></param>
        /// <returns>Returns </returns>
        public async Task<List<IAlternativeComparisonModel>> UpdateAsync(List<IAlternativeComparisonModel> comparisons)
        {
            using (var uof = _unitFactory.Create())
            {

                foreach (IAlternativeComparisonModel comparison in comparisons)
                {
                    var baseComparison = await _altCompRepo.GetByIDAsync(new Guid[] { comparison.CriteriaID, comparison.AlternativeID1, comparison.AlternativeID2 });
                    baseComparison.DateUpdated = DateTime.Now;
                    baseComparison.AlternativeRatio = comparison.AlternativeRatio;
                    await _altCompRepo.UpdateAsync(baseComparison);
                }
                await _altCompRepo.SaveAsync();
                uof.Commit();
                return comparisons;
            }
        }
    }
}
