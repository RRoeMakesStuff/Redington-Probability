using ProbabilityTool.Models.DataModels;
using ProbabilityTool.Models.Enums;

namespace ProbabilityTool.Calculations.Services;

public class CalculationService: ICalculationService
{
    public Calculation CalculateAndProbability(Calculation calculation)
    {
        if (calculation.Val1 is < 0 or > 1 ||
            calculation.Val2 is < 0 or > 1)
        {
            throw new ArgumentException("Probability values must be between 0 and 1");
        }

        calculation.Type = CalculationType.AND;

        calculation.Result = calculation.Val1 * calculation.Val2;
        return calculation;
    }

    public Calculation CalculateOrProbability(Calculation calculation)
    {
        if (calculation.Val1 is < 0 or > 1 ||
            calculation.Val2 is < 0 or > 1)
        {
            throw new ArgumentException("Probability values must be between 0 and 1");
        }

        calculation.Type = CalculationType.OR;

        calculation.Result = calculation.Val1 + calculation.Val2 - (calculation.Val1 * calculation.Val2);
        return calculation;
    }
}