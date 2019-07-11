﻿using AHP.Model.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AHP.Service.Common
{
    public interface ICalculateAHPScores
    {
        Task<List<string>> CalculateAlternativeWeights(Guid choiceId, double[] choiceWeights, List<ICriterionModel> criteria);
        Task<List<string>> CalculateCriteriaWeights(Guid choiceId);
        
    }
}