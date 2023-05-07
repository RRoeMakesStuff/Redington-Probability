using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProbabilityTool.DataStore.Interfaces;
using ProbabilityTool.Api.Controllers;
using ProbabilityTool.Models.DataModels;
using ProbabilityTool.Models.Enums;
using ProbabilityTool.TestUtils;
using Xunit;

namespace ProbabilityTool.Api.Tests.Controllers;

public class DataControllerTests
{
    private readonly Mock<IDataStoreReader<Calculation>> _mockDataReader;

    public DataControllerTests()
    {
        _mockDataReader = new Mock<IDataStoreReader<Calculation>>();
    }

    [Fact]
    public void GetProbabilityResult_WithValidId_ReturnsOkResultWithObject()
    {
        var probability1 = 0.2f;
        var probability2 = 0.3f;
        var id = Guid.NewGuid().ToString();
        var expectedResult = new SaveData<Calculation>()
        {
            Id = id,
            DataObject = new CalculationBuilder()
                .WithFirstValue(probability1)
                .WithSecondValue(probability2)
                .WithResult(probability1 * probability2)
                .WithCalculationType(CalculationType.AND)
                .Build()
        };
        _mockDataReader.Setup(mock => mock.GetObjectById(id)).Returns(expectedResult);
            
        var result = GetSut().GetProbabilityResult(id);
            
        var okResult = Assert.IsType<OkObjectResult>(result);
        var actualResult = Assert.IsType<SaveData<Calculation>>(okResult.Value);
        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void GetProbabilityResult_WithInvalidId_ReturnsNotFoundObjectResult()
    {
        var id = "123";
        _mockDataReader.Setup(mock => mock.GetObjectById(id)).Throws(new FileNotFoundException());
            
        var result = GetSut().GetProbabilityResult(id);
            
        Assert.IsType<NotFoundObjectResult>(result);
    }
        
    [Fact]
    public void GetAllProbabilityResults_WithValidData_ReturnsOkResultWithListOfObjects()
    {
        var expectedResult = new List<SaveData<Calculation>> { new() { Id = "123", DataObject = new CalculationBuilder().Build()} };
        _mockDataReader.Setup(mock => mock.GetAll()).Returns(expectedResult);
            
        var result = GetSut().GetAllProbabilityResults();
            
        var okResult = Assert.IsType<OkObjectResult>(result);
        var actualResult = Assert.IsType<List<SaveData<Calculation>>>(okResult.Value);
        Assert.Equal(expectedResult, actualResult);
    }

    private DataController GetSut() => new(_mockDataReader.Object);
}