using System;
using System.IO;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Moq;
using ProbabilityTool.DataStore.Interfaces;
using ProbabilityTool.DataStore.Services;
using ProbabilityTool.Models.DataModels;
using ProbabilityTool.TestUtils;
using Xunit;

namespace ProbabilityTool.DataStore.Tests.Services;

public class CalculationDataReaderTests
{
    private readonly Mock<IOptions<DataStoreOptions>> _optionsMock;
    private readonly string _filePath = Directory.GetCurrentDirectory() + "/dataWriterReaderTests";
    public CalculationDataReaderTests()
    {
        _optionsMock = new Mock<IOptions<DataStoreOptions>>();
        if (!Directory.Exists(_filePath))
        {
            Directory.CreateDirectory(_filePath);
        }
        
        foreach (var file in Directory.GetFiles(_filePath))
        {
            File.Delete(file);
        }
        
        _optionsMock.Setup(x => x.Value).Returns(new DataStoreOptions
        {
            FilePath = _filePath
        });
    }

    [Fact]
    public void GetObjectById_FileDoesNotExist_ThrowsFileNotFoundException()
    {
        Assert.Throws<FileNotFoundException>(() => GetSut().GetObjectById("non-existent-id"));
    }
    
    [Fact]
    public void GetObjectById_InvalidJsonData_ThrowsJsonException()
    {
        var saveData = new SaveData<Calculation> { Id = "test-id" };
        var filePath = Path.Combine(_optionsMock.Object.Value.FilePath, $"{saveData.Id}.json");
        var jsonDataString = "{ invalid json data }";
        File.WriteAllText(filePath, jsonDataString);
        
        Assert.Throws<JsonException>(() => GetSut().GetObjectById(saveData.Id));

        // Cleanup
        File.Delete(filePath);
    }
    
    [Fact]
    public void GetObjectById_ValidData_ReturnsSaveData()
    {
        var saveData = new SaveData<Calculation> { Id = "test-id", DataObject = new CalculationBuilder().Build() };
        var filePath = Path.Combine(_optionsMock.Object.Value.FilePath, $"{saveData.Id}.json");
        var jsonDataString = JsonSerializer.Serialize(saveData);
        File.WriteAllText(filePath, jsonDataString);

        var result = GetSut().GetObjectById(saveData.Id);

        Assertions.AssertSaveDataCalculationsAreEqual(saveData, result);
        
        File.Delete(filePath);
    }
    
    [Fact]
    public void GetAll_NoFiles_ReturnsEmptyList()
    {
        var result = GetSut().GetAll();
        
        Assert.Empty(result);
    }
    
    [Fact]
    public void GetAll_InvalidJsonData_SkipsCorruptedFile()
    {
        var saveData = new SaveData<Calculation> { Id = Guid.NewGuid().ToString(), DataObject = new CalculationBuilder().Build() };
        var filePath1 = Path.Combine(_optionsMock.Object.Value.FilePath, $"{saveData.Id}.json");
        var filePath2 = Path.Combine(_optionsMock.Object.Value.FilePath, "invalid-data.json");
        var jsonDataString1 = JsonSerializer.Serialize(saveData);
        var jsonDataString2 = JsonSerializer.Serialize(new
        {
            abc=123,
            def=234
        });
        File.WriteAllText(filePath1, jsonDataString1);
        File.WriteAllText(filePath2, jsonDataString2);
        
        var result = GetSut().GetAll();
        
        Assert.Single(result);
        Assertions.AssertSaveDataCalculationsAreEqual(saveData, result[0]);

        // Cleanup
        File.Delete(filePath1);
        File.Delete(filePath2);
    }

    private IDataStoreReader<Calculation> GetSut() => new CalculationDataStoreReader(_optionsMock.Object);
}
