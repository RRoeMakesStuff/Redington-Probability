using System;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProbabilityTool.Api.Controllers;
using ProbabilityTool.DataStore.Interfaces;
using ProbabilityTool.Calculations.Services;
using ProbabilityTool.TestUtils;
using Xunit;

namespace ProbabilityTool.Api.Tests.Controllers;

public class CalculationControllerTests
{
    private readonly Mock<ICalculationService> _mockCalculationService;
    private readonly Mock<IDataStoreWriter> _mockDataWriter;

    public CalculationControllerTests()
    {
        _mockCalculationService = new Mock<ICalculationService>();
        _mockDataWriter = new Mock<IDataStoreWriter>();
    }

    [Fact]
    public void CalculateCombinedProbability_ShouldReturnOk_WhenCalculationIsValid()
    {
        var calc = new CalculationBuilder().Build();
        var calcResult = new CalculationBuilder().WithResult(0.06f).Build();
        _mockCalculationService.Setup(x => x.CalculateAndProbability(calc)).Returns(calcResult);
        
        var result = GetSut().CalculateCombinedProbability(calc);
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(calcResult, okResult.Value);

        _mockDataWriter.Verify(x => x.SaveDataToStorage(calc), Times.Once);
    }

    [Fact]
    public void CalculateCombinedProbability_ShouldReturnBadRequest_WhenCalculationServiceThrowsArgumentException()
    {
        var calc = new CalculationBuilder().WithFirstValue(-0.5f).Build();
        var exceptionMessage = "Probability cannot be negative.";
        _mockCalculationService.Setup(x => x.CalculateAndProbability(calc))
            .Throws(new ArgumentException(exceptionMessage));
        
        var result = GetSut().CalculateCombinedProbability(calc);
        
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(exceptionMessage, badRequestResult.Value);
    }

    [Fact]
    public void CalculateCombinedProbability_ShouldReturnProblem_WhenCalculationServiceThrowsJsonException()
    {
        var calc = new CalculationBuilder().Build();
        var exceptionMessage = "Invalid JSON format.";
        _mockCalculationService.Setup(x => x.CalculateAndProbability(calc))
            .Throws(new JsonException(exceptionMessage));
        
        var result = GetSut().CalculateCombinedProbability(calc);
        
        var problemResult = Assert.IsType<ObjectResult>(result);
        var problemDetails = Assert.IsType<ProblemDetails>(problemResult.Value);
        Assert.Equal(500, problemResult.StatusCode);
        Assert.Equal("Invalid JSON format.", problemDetails.Detail);
    }
    
    [Fact]
    public void CalculateOrProbability_ShouldReturnOk_WhenCalculationIsValid()
    {
        var calc = new CalculationBuilder().Build();
        var calcResult = new CalculationBuilder().WithResult(0.06f).Build();
        _mockCalculationService.Setup(x => x.CalculateOrProbability(calc)).Returns(calcResult);
        
        var result = GetSut().CalculateOrProbability(calc);
        
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(calcResult, okResult.Value);

        _mockDataWriter.Verify(x => x.SaveDataToStorage(calc), Times.Once);
    }

    [Fact]
    public void CalculateOrProbability_ShouldReturnBadRequest_WhenCalculationServiceThrowsArgumentException()
    {
        var calc = new CalculationBuilder().WithFirstValue(-0.5f).Build();
        var exceptionMessage = "Probability cannot be negative.";
        _mockCalculationService.Setup(x => x.CalculateOrProbability(calc))
            .Throws(new ArgumentException(exceptionMessage));
        
        var result = GetSut().CalculateOrProbability(calc);
        
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(exceptionMessage, badRequestResult.Value);
    }

    [Fact]
    public void CalculateOrProbability_ShouldReturnProblem_WhenCalculationServiceThrowsJsonException()
    {
        var calc = new CalculationBuilder().Build();
        var exceptionMessage = "Invalid JSON format.";
        _mockCalculationService.Setup(x => x.CalculateOrProbability(calc))
            .Throws(new JsonException(exceptionMessage));
        
        var result = GetSut().CalculateOrProbability(calc);
        
        var problemResult = Assert.IsType<ObjectResult>(result);
        var problemDetails = Assert.IsType<ProblemDetails>(problemResult.Value);
        Assert.Equal(500, problemResult.StatusCode);
        Assert.Equal("Invalid JSON format.", problemDetails.Detail);
    }

    private CalculationController GetSut() => new(_mockCalculationService.Object, _mockDataWriter.Object);
}