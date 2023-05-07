using System;
using ProbabilityTool.Calculations.Services;
using ProbabilityTool.Models.Enums;
using ProbabilityTool.TestUtils;
using Xunit;

namespace ProbabilityTool.Calculations.Tests.Services;

public class CalculationServiceTests
{
    [Fact]
    public void CalculateAndProbability_ReturnsCorrectResult()
    {
        var calculation = new CalculationBuilder()
            .WithFirstValue(0.2)
            .WithSecondValue(0.4)
            .Build();
        
        var result = GetSut().CalculateAndProbability(calculation);
        
        Assert.Equal(CalculationType.AND, result.Type);
        Assert.Equal(0.08, Math.Round((double)result.Result, 2));
    }

    [Fact]
    public void CalculateOrProbability_ReturnsCorrectResult()
    {
        var calculation = new CalculationBuilder()
            .WithFirstValue(0.3f)
            .WithSecondValue(0.6f)
            .Build();
        
        var result = GetSut().CalculateOrProbability(calculation);
        
        Assert.Equal(CalculationType.OR, result.Type);
        Assert.Equal(0.72, Math.Round((double)result.Result, 2));
    }

    [Theory]
    [InlineData(-1, 0.5)]
    [InlineData(0.5, -1)]
    [InlineData(1.5, 0.5)]
    [InlineData(0.5, 1.5)]
    public void CalculateProbability_ThrowsArgumentException_WhenValueOutOfRange(double firstVal, double secondVal)
    {
        var calculation = new CalculationBuilder()
            .WithFirstValue(firstVal)
            .WithSecondValue(secondVal)
            .Build();
        
        var andError = Assert.Throws<ArgumentException>(() => GetSut().CalculateAndProbability(calculation));
        var orError = Assert.Throws<ArgumentException>(() => GetSut().CalculateOrProbability(calculation));
        Assert.Equal("Probability values must be between 0 and 1", andError.Message);
        Assert.Equal("Probability values must be between 0 and 1", orError.Message);
    }

    private ICalculationService GetSut() => new CalculationService();
}