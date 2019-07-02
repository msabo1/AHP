export default class Calculator {
  MatrixFiller(dimension: number, values: number[]): number[][];
  NthRoots(dimension: number, matrix: number[][]): number[];
  Weights(dimension: number, vector: number[]): number[];
  CalculateConsistency(dimension: number, matrix: number[][]): number;
  CalculateFinalScore(AlternativeWeights: number[][], CriteriaWeights: number[]);
}
