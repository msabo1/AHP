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
        IVectorFiller _vectorFiller;

        public CalculateAHPScores(
            IUnitOfWorkFactory unitOfWorkFactory,
            IAlternativeRepository alternativeRepository,
            IAlternativeComparisonRepository alternativeComparisonRepo,
            ICriterionRepository criterionRepository,
            ICriteriaComparisonRepository criteriaComparisonRepo,
            IVectorFiller vectorFiller)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _alternativeRepository = alternativeRepository;
            _alternativeComparisonRepo = alternativeComparisonRepo;
            _criterionRepository = criterionRepository;
            _criteriaComparisonRepo = criteriaComparisonRepo;
            _vectorFiller = vectorFiller;
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
                var comparisons = await _criteriaComparisonRepo.GetByCriterionIDAsync(criterion.CriteriaID);
                foreach(var comparison in comparisons)
                {
                    if(criterion.CriteriaID == comparison.CriteriaID1)
                    {
                        matrix[hash[criterion.CriteriaID], hash[comparison.CriteriaID2]] = comparison.CriteriaRatio;
                        matrix[hash[comparison.CriteriaID2], hash[criterion.CriteriaID]] = 1 / comparison.CriteriaRatio;
                    }
                    else
                    {
                        matrix[hash[criterion.CriteriaID], hash[comparison.CriteriaID2]] = 1 / comparison.CriteriaRatio;
                        matrix[hash[comparison.CriteriaID2], hash[criterion.CriteriaID]] = comparison.CriteriaRatio;
                    }
                }
            }
            var weights = _vectorFiller.NthRoots(criteria.Count, matrix);
            
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
                    var comparisons = await _alternativeComparisonRepo.GetByCriteriaAlternativesIDAsync(criterion.CriteriaID, alternative.AlternativeID);
                    foreach(var comparison in comparisons)
                    {
                        if(alternative.AlternativeID == comparison.AlternativeID1)
                        {
                            matrix[hash[alternative.AlternativeID], hash[comparison.AlternativeID2]] = comparison.AlternativeRatio;
                            matrix[hash[comparison.AlternativeID2], hash[alternative.AlternativeID]] = 1 / comparison.AlternativeRatio;
                        }
                        else
                        {
                            matrix[hash[alternative.AlternativeID], hash[comparison.AlternativeID2]] = 1 / comparison.AlternativeRatio;
                            matrix[hash[comparison.AlternativeID2], hash[alternative.AlternativeID]] = comparison.AlternativeRatio;
                        }
                    }
                }
                var weight = _vectorFiller.NthRoots(alternatives.Count, matrix);
                weights.Add(weight);
            }
            double[,] alternativeWeightMatrix = new double[criteria.Count, alternatives.Count];
            i = 0;
            foreach(var weight in weights)
            {
                for(int j = 0; j<alternatives.Count; j++) {
                    alternativeWeightMatrix[i, j] = weight[j];
                }
                i++;
            }
            FinalScoreCalculator calculator = new FinalScoreCalculator();
            var alternativeScores = calculator.CalculateFinalScore(alternativeWeightMatrix, choiceWeights);
            //sprema dobivene scoreove

            using (var uof = _unitOfWorkFactory.Create())
            {
                for (i = 0; i < alternatives.Count; i++)
                {
                    alternatives[i].AlternativeScore = alternativeScores[i];
                    await _alternativeRepository.UpdateAsync(alternatives[i]);
                   
                }
                await _alternativeRepository.SaveAsync();
                uof.Commit();
            }
            return alternatives;

        }

    }
}
