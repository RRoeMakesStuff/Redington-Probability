using ProbabilityTool.Models.Enums;

namespace ProbabilityTool.Models.DataModels;

public class Calculation
{
    public double Val1 { get; set; }
    public double Val2 { get; set; }
    public CalculationType? Type { get; set; }
    public double? Result { get; set; }
}