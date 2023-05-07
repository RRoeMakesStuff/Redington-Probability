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

public class DataStoreWriterTests
{
    private readonly string _filePath = Directory.GetCurrentDirectory() + "/dataWriterStoreTests";
    private readonly Mock<IOptions<DataStoreOptions>> _optionsMock;

    public DataStoreWriterTests()
    {
        _optionsMock = new Mock<IOptions<DataStoreOptions>>();
        _optionsMock.Setup(o => o.Value).Returns(new DataStoreOptions { FilePath = _filePath });
    }

    [Fact]
    public void SaveDataToStorage_SavesDataCorrectly()
    {
        var data = new CalculationBuilder()
            .WithFirstValue(0.5)
            .WithSecondValue(0.6)
            .Build();

        var id = GetSut().SaveDataToStorage(data);
        var filePath = Path.Combine(_optionsMock.Object.Value.FilePath, $"{id}.json");
        
        Assert.True(File.Exists(filePath));
        var jsonDataString = File.ReadAllText(filePath);
        var saveData = JsonSerializer.Deserialize<SaveData<Calculation>>(jsonDataString);
        Assert.NotNull(saveData);
        Assert.Equal(id, saveData.Id);
        Assertions.AssertCalculationsAreEqual(data, saveData.DataObject);
        
        File.Delete(filePath);
    }

    [Fact]
    public void SaveDataToStorage_CreatesDirectoryIfNotExists()
    {
        var data = new Calculation { Val1 = 0.5, Val2 = 0.6 };

        var id = GetSut().SaveDataToStorage(data);
        var directoryPath = Path.GetDirectoryName(_optionsMock.Object.Value.FilePath);

        Assert.True(Directory.Exists(directoryPath));
    }

    [Fact]
    public void Constructor_CreatesDirectoryIfNotExists()
    {
        var dummyFilePath = Directory.GetCurrentDirectory() + "/unit-test-file-path";
        var optionsMock = new Mock<IOptions<DataStoreOptions>>();
        optionsMock.Setup(o => o.Value).Returns(new DataStoreOptions { FilePath = dummyFilePath });
        
        var writer = new DataStoreWriter(optionsMock.Object);
        
        Assert.True(Directory.Exists(dummyFilePath));
    }

    private IDataStoreWriter GetSut() => new DataStoreWriter(_optionsMock.Object);
}