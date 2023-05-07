using ProbabilityTool.Models.DataModels;

namespace ProbabilityTool.Calculations.Services;

public interface ICalculationService
{
    public Calculation CalculateAndProbability(Calculation calculation);
    public Calculation CalculateOrProbability(Calculation calculation);
}