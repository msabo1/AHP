"use strict";
export default class Calculator {

  MatrixFiller(dimension, values) {
    var M = new Array(dimension);
    for (let k = 0; k < dimension; k++) M[k] = new Array(dimension);
    for (let i = 0; i < dimension; i++) {
      for (let j = i + 1; j < dimension; j++) {
        M[i][j] = (values[i + j - 1] > 0) ? values[i + j - 1] : -(1 / values[i + j - 1]);
        M[j][i] = 1 / M[i][j];
      }
      M[i][i] = 1;
    }
    return M;
  };
  
  NthRoots (dimension, matrix) {
    var Nth = [];
    for (let i = 0; i < dimension; i++) {
      let temp = 1;
      for (let j = 0; j < dimension; j++) {
        temp *= matrix[i][j];
      }
      Nth[i] = Math.pow(temp, 1.0 / dimension);
    }
    return Nth;
  };

  Weights (dimension, vector) {
    let temp = 0;
    for (let i = 0; i < dimension; i++) {
      temp += vector[i];
    }
    let weight = [];
    for (let i = 0; i < dimension; i++) {
      weight[i] = vector[i] / temp;
    }
    return weight;
  };

  CalculateConsistency (dimension, matrix) {
    var NthRoot = this.NthRoots(dimension, matrix);
    var Weight = this.Weights(dimension, NthRoot);

    let ColumnSum = [];
    for (let j = 0; j < dimension; j++) {
      let temp = 0;
      for (let i = 0; i < dimension; i++) {
        temp += matrix[i][j];
      }
      ColumnSum[j] = temp;
    }

    let LambdaMax = 0;

    for (let i = 0; i < dimension; i++) {
      LambdaMax += (ColumnSum[i] * Weight[i]);
    }

    let Coefficients = [0, 0, 0.58, 0.9, 1.12, 1.24, 1.32, 1.41, 1.45];

    return dimension <= 2 ? 0 : ((LambdaMax - dimension) / (dimension - 1)) / Coefficients[dimension];
  };

  CalculateFinalScore (AlternativeWeights, CriteriaWeights) {
    var FinalScore = [];
    for (let i = 0; i < AlternativeWeights.length; i++) {
      let temp = 0;
      for (let j = 0; j < CriteriaWeights.length; j++) {
        temp += AlternativeWeights[j][i] * CriteriaWeights[j];
      }
      FinalScore[i] = temp;
    }

    return FinalScore;
  };
}
