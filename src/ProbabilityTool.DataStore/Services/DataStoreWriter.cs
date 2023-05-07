using System.Text.Json;
using Microsoft.Extensions.Options;
using ProbabilityTool.DataStore.Interfaces;
using ProbabilityTool.Models.DataModels;

namespace ProbabilityTool.DataStore.Services;

public class DataStoreWriter: IDataStoreWriter
{
    private readonly IOptions<DataStoreOptions> _options;

    public DataStoreWriter(IOptions<DataStoreOptions> options)
    {
        _options = options;
        if (!Directory.Exists(_options.Value.FilePath))
        {
            Directory.CreateDirectory(_options.Value.FilePath);
            Console.WriteLine($"Directory not found at file path {_options.Value.FilePath}. Creating new directory.");
        }
    }

    public string SaveDataToStorage<T>(T data)
    {
        var id = Guid.NewGuid().ToString();
        var saveData = new SaveData<T>
        {
            Id = id,
            DataObject = data
        };
        
        var serialized = JsonSerializer.Serialize(saveData);
        File.WriteAllText($"{_options.Value.FilePath}/{id}.json", serialized);
        return id;
    }
}