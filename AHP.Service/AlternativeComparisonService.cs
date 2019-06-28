﻿using AHP.Model.Common;
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
                foreach(IAlternativeComparisonModel comparison in comparisons)
            {
                comparison.DateCreated = DateTime.Now;
                comparison.DateUpdated = DateTime.Now;
            }
                comparisons = _altCompRepo.AddRange(comparisons);
                await _altCompRepo.SaveAsync();
                
            
            return comparisons;

        }
        public async Task<List<IAlternativeComparisonModel>> GetAsync(Guid alternativeId,Guid criteriaId, int page = 1)
        {
            var alternatives = await _altCompRepo.GetByCriteriaAlternativesIDAsync(criteriaId, alternativeId, page);

            return alternatives;
        }
        public async Task<bool> UpdateAsync(List<IAlternativeComparisonModel> comparisons)
        {
            foreach(IAlternativeComparisonModel comparison in comparisons)
            {
                await _altCompRepo.UpdateAsync(comparison);
            }
            await _altCompRepo.SaveAsync();
            return true;
        }
    }
}
