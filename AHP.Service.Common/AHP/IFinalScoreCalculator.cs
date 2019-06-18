﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Service.Common
{
    public interface IFinalScoreCalculator
    {
        double[] CalculateFinalScore(double[,] AlternativeWeights, double[] CriteriaWeights);
    }
}
