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
    class CalculateAHPScores
    {
        IUnitOfWorkFactory _unitOfWorkFactory;
        ICriterionRepository _criterionRepository;
        IAlternativeRepository _alternativeRepository;
        IAlternativeComparisonRepository _alternativeComparisonRepo;
        ICriteriaComparisonRepository _criteriaComparisonRepo;
        IMatrixFiller _matrixFiller;

        public CalculateAHPScores(
            IUnitOfWorkFactory unitOfWorkFactory,
            IAlternativeRepository alternativeRepository,
            IAlternativeComparisonRepository alternativeComparisonRepo,
            ICriterionRepository criterionRepository,
            ICriteriaComparisonRepository criteriaComparisonRepo,
            IMatrixFiller matrixFiller)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _alternativeRepository = alternativeRepository;
            _alternativeComparisonRepo = alternativeComparisonRepo;
            _criterionRepository = criterionRepository;
            _criteriaComparisonRepo = criteriaComparisonRepo;
            _matrixFiller = matrixFiller;
        }

        public async void CalculateCriteriaWeights(Guid choiceId)
        {
            var criteria = await _criterionRepository.GetPageByChoiceIDAsync(choiceId, 1);
            criteria.Sort((x, y) => x.DateCreated.CompareTo(y.DateCreated));
            List<double> comparisons = new List<double>();
            int dimension = 0;
            for (int i = 0; i < criteria.Count(); i++)
            {
                var perCriteriaComparisons = await _criteriaComparisonRepo.GetByFirstCriterionIDAsync(criteria[i].CriteriaID);
                for (int j = 0; j < i; j++)
                {
                    var comparison = perCriteriaComparisons.Find(a => a.CriteriaID2 == criteria[j].CriteriaID);
                    comparisons.Add(comparison.CriteriaRatio);
                }
                dimension = i;
            }
            var weights = _matrixFiller.FillMatrix(dimension, comparisons.ToArray());
            CalculateAlternativeWeights(choiceId, weights);
        }
        

        public async void CalculateAlternativeWeights(Guid choiceId, double[] choiceWeights)
        {
            var alternatives = await _alternativeRepository.GetByChoiceIDAsync(choiceId);
            alternatives.Sort((x, y) => x.DateCreated.CompareTo(y.DateCreated));
            var criteria = await _criterionRepository.GetPageByChoiceIDAsync(choiceId, 1);
            List<double[]> comparisons = new List<double[]>();
     
                

                for (int i = 0; i < alternatives.Count(); i++)
                {
               var altComparisons = await _alternativeComparisonRepo.GetByFirstAlternativeIDAsync(alternatives[i].AlternativeID);

                    for (int j = 0; j < i; j++)
                    {
                        for (int z = 0; z < criteria.Count(); z++)
                        {
                      //  altComparisons.Find(a => ( a.AlternativeID2 == alternatives[j].AlternativeID, a.CriteriaID == criteria[z].CriteriaID));
                            //polje.Add(comparison.AlternativeRatio);
                        }
                    }

                }
    
            
            double[,] alternativeComparisonMatrix = new double[criteria.Count(), alternatives.Count()];
            for (int i = 0; i < comparisons.Count(); i++)
            {
                for (int j = 0; j < comparisons[i].Length; j++)
                {
                    alternativeComparisonMatrix[i, j] = comparisons[i][j];
                }
            }
            FinalScoreCalculator calculator = new FinalScoreCalculator();
            var alternativeScores = calculator.CalculateFinalScore(alternativeComparisonMatrix, choiceWeights);
            using (var uof = _unitOfWorkFactory.Create())
            {
                for (int i = 0; i < alternatives.Count(); i++)
                {
                    alternatives[i].AlternativeScore = alternativeScores[i];
                    await _alternativeRepository.UpdateAsync(alternatives[i]);
                }
            }
        }
       
    }
}
