using AHP.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHP.Service
{
    class AHPService : IAHPService
    {
       public double[] CalculatePriortyVector(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            double[] vector = new double[n];
            for(int i = 0; i< n; i++)
            {
                vector[i] = 1;
                for (int j = 0; j < n; j++)
                {
                    vector[i] *= matrix[i, j];
                }
                vector[i] = Math.Pow(vector[i], 1.0 / n);
            }
            var sum = vector.Sum();
            for(int i = 0; i < vector.Length; i++)
            {
                vector[i] /= sum;
            }
            return vector;
        }

        public double[] FinalCalculate(double[] criteriaPriorityVector, double[,] alternativesPriorityMatrix)
        {
            var n = alternativesPriorityMatrix.GetLength(0);
            var m = alternativesPriorityMatrix.GetLength(1);
            double[] scores = new double[n];
            for(int i = 0; i<n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    scores[i] += criteriaPriorityVector[j] * alternativesPriorityMatrix[i, j]; 
                }
            }
            return scores;
        }
    }
}
