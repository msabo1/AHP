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
        /// <summary>
        /// Calculates criteria weights of a choice,
        /// invokes CalculateAlternativeWeights
        /// </summary>
        /// <param name="choiceId"></param>
        /// <returns>Returns list of updated AlternativeModel</returns>
        public async Task<List<IAlternativeModel>> CalculateCriteriaWeights(Guid choiceId)
        {
            var criteria = await _criterionRepository.GetPageByChoiceIDAsync(choiceId, 1);
            criteria.Sort((x, y) => x.DateCreated.CompareTo(y.DateCreated));
            List<List<double>> comparisons = new List<List<double>>();
            List<ICriteriaComparisonModel> sviComparisoni = new List<ICriteriaComparisonModel>();
            Guid[] CriteriaID = new Guid[criteria.Count];
            //loads all criteria comparisons in a list
            for (int i = 0; i < criteria.Count; i++)
            {
                var criteriaComparisons = await _criteriaComparisonRepo.GetByFirstCriterionIDAsync(criteria[i].CriteriaID);
                sviComparisoni.AddRange(criteriaComparisons);
                CriteriaID[criteriaComparisons.Count] = criteria[i].CriteriaID;
            }
            //Creates criteira comparisons for new criteria
            for (int i = 0; i < criteria.Count; i++)
            {
                List<double> comparisoni = new List<double>();
                for (int j = 0; j < criteria.Count; j++)
                {
                    var comparison = sviComparisoni.Find(a => a.CriteriaID2 == CriteriaID[j] && a.CriteriaID1 == CriteriaID[i]);
                    if (comparison != null)
                    {
                        comparisoni.Add(comparison.CriteriaRatio);
                    }
                }
                comparisons.Add(comparisoni);
            }
            var result = comparisons.OrderBy(x => x.Count);
            int dimension = comparisons.Count;
            List<double> krajnjaLista = new List<double>();
            foreach (List<double> lista in result)
            {
                krajnjaLista.AddRange(lista);
            }

            var weights = _matrixFiller.FillMatrix(dimension, krajnjaLista.ToArray());

            return await CalculateAlternativeWeights(choiceId, weights.Reverse().ToArray(), criteria);

        }
        /// <summary>
        /// Calculates alternative weights, calculates final scores, 
        /// alternatives with new scores
        /// </summary>
        /// <param name="choiceId"></param>
        /// <param name="choiceWeights"></param>
        /// <param name="criteria"></param>
        /// <returns>Returns list of AlternativeModel</returns>
        public async Task<List<IAlternativeModel>> CalculateAlternativeWeights(Guid choiceId, double[] choiceWeights, List<ICriterionModel> criteria)
        {
            var alternatives = await _alternativeRepository.GetByChoiceIDAsync(choiceId);
            List<List<double>> sviWeightovi = new List<List<double>>();
            List<IAlternativeComparisonModel> sviComparisoni = new List<IAlternativeComparisonModel>();
            for (int i = 0; i < criteria.Count; i++)
            {
                sviComparisoni.AddRange(await _alternativeComparisonRepo.GetByCriteriaIDAsync(criteria[i].CriteriaID));
            }
            for (int i = 0; i < criteria.Count; i++)
            {
                List<List<double>> comparisons = new List<List<double>>();
                for (int j = 0; j < alternatives.Count; j++)
                {
                    List<double> comparisoni = new List<double>();
                    for (int z = 0; z < alternatives.Count; z++)
                    {
                        var comparison = sviComparisoni.Find(a =>
                        a.AlternativeID2 == alternatives[z].AlternativeID &&
                        a.AlternativeID1 == alternatives[j].AlternativeID &&
                        a.CriteriaID == criteria[i].CriteriaID);
                        if (comparison != null)
                        {
                            comparisoni.Add(comparison.AlternativeRatio);
                        }
                    }
                    comparisons.Add(comparisoni);
                }
                var rezultat = comparisons.OrderBy(x => x.Count);
                int dimension = alternatives.Count;
                List<double> krajnjaLista = new List<double>();
                foreach (List<double> lista in rezultat)
                {
                    krajnjaLista.AddRange(lista);
                }
                var weights = _matrixFiller.FillMatrix(dimension, krajnjaLista.ToArray()).ToList();
                sviWeightovi.Add(weights);
            }
            var result = sviWeightovi.OrderBy(x => x.Count);
            //sprema alternativ weightove u matricu po kriterijima
            double[,] alternativeWeightMatrix = new double[alternatives.Count(), criteria.Count()];
            var duljina = sviWeightovi[0].ToArray().Length;
            for (int i = 0; i < sviWeightovi.Count; i++)
            {

                for (int j = 0; j < duljina; j++)
                {
                    alternativeWeightMatrix[j, i] = sviWeightovi[i][j];
                }
            }
            FinalScoreCalculator calculator = new FinalScoreCalculator();
            var alternativeScores = calculator.CalculateFinalScore(alternativeWeightMatrix, choiceWeights);
            //sprema dobivene scoreove
            using (var uof = _unitOfWorkFactory.Create())
            {
                for (int i = 0; i < alternatives.Count; i++)
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