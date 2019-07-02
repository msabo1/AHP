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
            for(int i = 0; i < criteria.Count(); i++)
            { 
                for(int j = 0; j < i; j++)
                {
                    Guid[] array = { criteria[i].CriteriaID, criteria[j].CriteriaID };
                    var comparison = await _criteriaComparisonRepo.GetByIDAsync(array);
                    comparisons.Add(comparison.CriteriaRatio);
                }
                dimension = i;
            }
            var weights = _matrixFiller.FillMatrix(dimension, comparisons.ToArray());
            CalculateAlternativeWeights(choiceId, weights);
        }
        

        public async void CalculateAlternativeWeights(Guid choiceId, double[] choiceWeights)
        {
            var alternatives = await _alternativeRepository.GetAlternativesByChoiceIDAsync(choiceId, 1);
            alternatives.Sort((x, y) => x.DateCreated.CompareTo(y.DateCreated));
            var criteria = await _criterionRepository.GetPageByChoiceIDAsync(choiceId, 1);
            List<double[]> comparisons = new List<double[]>();
            for (int z = 0; z < criteria.Count(); z++)
            {
                List<double> polje = new List<double>();

                for (int i = 0; i < alternatives.Count(); i++)
                {
                    for (int j = 0; j < i; j++)
                    {
                        Guid[] array = { criteria[i].CriteriaID, alternatives[i].AlternativeID, alternatives[j].AlternativeID };
                        var comparison = await _alternativeComparisonRepo.GetByIDAsync(array);
                        polje.Add(comparison.AlternativeRatio);
                    }

                }
                var weights = _matrixFiller.FillMatrix(alternatives.Count(), polje.ToArray());
                comparisons.Add(weights);
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
