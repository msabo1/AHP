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
    class CalculateAHPScores : ICalculateAHPScores
    {
        IUnitOfWorkFactory _unitOfWorkFactory;
        ICriterionRepository _criterionRepository;
        IAlternativeRepository _alternativeRepository;
        IAlternativeComparisonRepository _alternativeComparisonRepo;
        ICriteriaComparisonRepository _criteriaComparisonRepo;
        IAHPService _AHPService;

        public CalculateAHPScores(
            IUnitOfWorkFactory unitOfWorkFactory,
            IAlternativeRepository alternativeRepository,
            IAlternativeComparisonRepository alternativeComparisonRepo,
            ICriterionRepository criterionRepository,
            ICriteriaComparisonRepository criteriaComparisonRepo,
            IAHPService AHPService)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _alternativeRepository = alternativeRepository;
            _alternativeComparisonRepo = alternativeComparisonRepo;
            _criterionRepository = criterionRepository;
            _criteriaComparisonRepo = criteriaComparisonRepo;
            _AHPService = AHPService;
        }

        public async Task<List<IAlternativeModel>> CalculateCriteriaWeights(Guid choiceId)
        {
            var criteria = await _criterionRepository.GetByChoiceIDAsync(choiceId);
            var hash = new Dictionary<Guid, int>();
            int i = 0;
            foreach(var criterion in criteria)
            {
                hash[criterion.CriteriaID] = i;
                i++;
            }
            double[,] matrix = new double[criteria.Count, criteria.Count];
            foreach(var criterion in criteria)
            {
                matrix[hash[criterion.CriteriaID], hash[criterion.CriteriaID]] = 1;
                var comparisons = await _criteriaComparisonRepo.GetByCriterionIDAsync(criterion.CriteriaID);
                foreach(var comparison in comparisons)
                {
                    if(criterion.CriteriaID == comparison.CriteriaID1)
                    {
                        matrix[hash[comparison.CriteriaID2], hash[comparison.CriteriaID1]] = comparison.CriteriaRatio;
                        matrix[hash[comparison.CriteriaID1], hash[comparison.CriteriaID2]] = 1 / comparison.CriteriaRatio;
                    }
                    else
                    {
                        matrix[hash[comparison.CriteriaID2], hash[comparison.CriteriaID1]] = 1 / comparison.CriteriaRatio;
                        matrix[hash[comparison.CriteriaID1], hash[comparison.CriteriaID2]] = comparison.CriteriaRatio;
                    }
                }
            }
            var weights = _AHPService.CalculatePriortyVector(matrix);
            
            return await CalculateAlternativeWeights(choiceId, weights, criteria);
            
        }


        public async Task<List<IAlternativeModel>> CalculateAlternativeWeights(Guid choiceId, double[] choiceWeights, List<ICriterionModel> criteria)
        {
            var alternatives = await _alternativeRepository.GetByChoiceIDAsync(choiceId);
            var hash = new Dictionary<Guid, int>();
            int i = 0;
            foreach(var alternative in alternatives)
            {
                hash[alternative.AlternativeID] = i;
                i++;
            }
            var weights = new List<double[]>();
            foreach(var criterion in criteria)
            {
                double[,] matrix = new double[alternatives.Count, alternatives.Count];
                foreach(var alternative in alternatives)
                {
                    matrix[hash[alternative.AlternativeID], hash[alternative.AlternativeID]] = 1;
                    var comparisons = await _alternativeComparisonRepo.GetByCriteriaAlternativesIDAsync(criterion.CriteriaID, alternative.AlternativeID);
                    foreach(var comparison in comparisons)
                    {
                        if(alternative.AlternativeID == comparison.AlternativeID1)
                        {
                            matrix[hash[comparison.AlternativeID2], hash[comparison.AlternativeID1]] = comparison.AlternativeRatio;
                            matrix[hash[comparison.AlternativeID1], hash[comparison.AlternativeID2]] = 1 / comparison.AlternativeRatio;
                        }
                        else
                        {
                            matrix[hash[comparison.AlternativeID2], hash[comparison.AlternativeID1]] = 1 / comparison.AlternativeRatio;
                            matrix[hash[comparison.AlternativeID1], hash[comparison.AlternativeID2]] = comparison.AlternativeRatio;
                        }
                    }
                }
                var weight = _AHPService.CalculatePriortyVector(matrix);
                weights.Add(weight);
            }
            double[,] alternativeWeightMatrix = new double[criteria.Count, alternatives.Count];
            i = 0;
            foreach(var weight in weights)
            {
                for(int j = 0; j<alternatives.Count; j++) {
                    alternativeWeightMatrix[j, i] = weight[j];
                }
                i++;
            }
            var alternativeScores = _AHPService.FinalCalculate(choiceWeights, alternativeWeightMatrix);
            //sprema dobivene scoreove

            using (var uof = _unitOfWorkFactory.Create())
            {
                i = 0;
                foreach(var alternative in alternatives)
                {
                    alternative.AlternativeScore = alternativeScores[i];
                    await _alternativeRepository.UpdateAsync(alternative);
                    i++;
                }
                await _alternativeRepository.SaveAsync();
                uof.Commit();
            }
            return alternatives;

        }

    }
}
