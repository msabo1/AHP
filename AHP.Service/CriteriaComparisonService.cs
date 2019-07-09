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
    class CriteriaComparisonService: ICriteriaComparisonService
    {
        ICriteriaComparisonRepository _criteriaComparisonRepository;
        ICriterionRepository _criterionRepository;
        IUnitOfWorkFactory _unitOfWorkFactory;
        public CriteriaComparisonService(ICriteriaComparisonRepository criteriaComparisonRepository, IUnitOfWorkFactory unitOfWorkFactory, ICriterionRepository criterionRepository)
        {
            _criterionRepository = criterionRepository;

            _criteriaComparisonRepository = criteriaComparisonRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        /// <summary>
        /// Read method,
        /// gets a page of criteria comparisons by choiceID
        /// </summary>
        /// <param name="choiceId"></param>
        /// <param name="page"></param>
        /// <returns>Returns a list of criteria comparisons</returns>
        public async Task<List<ICriteriaComparisonModel>> GetByCriteriaAsync(Guid choiceId, int page)
        {
            var comparisons = await _criteriaComparisonRepository.GetPageByCriterionIDAsync(choiceId, page);
            foreach (var comparison in comparisons)
            {
                comparison.CriteriaName1 = (await _criterionRepository.GetByIDAsync(comparison.CriteriaID1)).CriteriaName;
                comparison.CriteriaName2 = (await _criterionRepository.GetByIDAsync(comparison.CriteriaID2)).CriteriaName;
            }
            return comparisons;
        }

        /// <summary>
        /// Update method,
        /// updates criteria comparisons
        /// </summary>
        /// <param name="comparisons"></param>
        /// <returns>Returns updated comparisons</returns>
        public async Task<List<ICriteriaComparisonModel>> UpdateAsync(List<ICriteriaComparisonModel> comparisons)
        {
            using (var uof = _unitOfWorkFactory.Create())
            {
                foreach (var comparison in comparisons)
                {
                    var baseComparison = await _criteriaComparisonRepository.GetByIDAsync(comparison.CriteriaID1, comparison.CriteriaID2);
                    baseComparison.DateUpdated = DateTime.Now;
                    baseComparison.CriteriaRatio = comparison.CriteriaRatio;
                    await _criteriaComparisonRepository.UpdateAsync(baseComparison);
                }

                await _criteriaComparisonRepository.SaveAsync();
                uof.Commit();
                return comparisons;
            }
        }
    }
}
