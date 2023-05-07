using ProbabilityTool.Models.DataModels;
using ProbabilityTool.Models.Enums;

namespace ProbabilityTool.TestUtils;

public class CalculationBuilder
{
    private double _val1;
    private double _val2;
    public CalculationType? _type;
    private double? _result;

    public CalculationBuilder()
    {
        _val1 = 0.1;
        _val2 = 0.2;
    }

    public Calculation Build()
    {
        return new Calculation
        {
            Val1 = _val1,
            Val2 = _val2,
            Result = _result,
            Type = _type
        };
    }

    public CalculationBuilder WithFirstValue(double val)
    {
        _val1 = val;
        return this;
    }
    
    public CalculationBuilder WithSecondValue(double val)
    {
        _val2 = val;
        return this;
    }
    
    public CalculationBuilder WithCalculationType(CalculationType type)
    {
        _type = type;
        return this;
    }
    
    public CalculationBuilder WithResult(double val)
    {
        _result = val;
        return this;
    }
}