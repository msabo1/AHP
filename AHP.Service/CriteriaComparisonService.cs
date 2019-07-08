﻿using AHP.Model.Common;
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
        IUnitOfWorkFactory _unitOfWorkFactory;
        public CriteriaComparisonService(ICriteriaComparisonRepository criteriaComparisonRepository, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _criteriaComparisonRepository = criteriaComparisonRepository;
            _unitOfWorkFactory = unitOfWorkFactory;
        }
       public async Task<List<ICriteriaComparisonModel>> AddAsync(List<ICriteriaComparisonModel> comparisons)
        {
            foreach(var comparison in comparisons)
            {
                comparison.DateCreated = DateTime.Now;
                comparison.DateUpdated = DateTime.Now;
            }
            
            comparisons = _criteriaComparisonRepository.AddRange(comparisons);
            await _criteriaComparisonRepository.SaveAsync();
            return comparisons;
        }

        public async Task<List<ICriteriaComparisonModel>> GetByCriteriaAsync(Guid choiceId, int page)
        {

            return await _criteriaComparisonRepository.GetPageByCriterionIDAsync(choiceId, page);
        }


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
