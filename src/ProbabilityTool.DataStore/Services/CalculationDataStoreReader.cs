using System.Text.Json;
using Microsoft.Extensions.Options;
using ProbabilityTool.DataStore.Interfaces;
using ProbabilityTool.Models.DataModels;

namespace ProbabilityTool.DataStore.Services;

public class CalculationDataStoreReader: IDataStoreReader<Calculation>
{
    private readonly IOptions<DataStoreOptions> _options;
    public CalculationDataStoreReader(IOptions<DataStoreOptions> options)
    {
        _options = options;
    }
    public SaveData<Calculation> GetObjectById(string id)
    {
        var filePath = $"{_options.Value.FilePath}/{id}.json";
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Missing data record: {id}");
        }
        var jsonDataString = File.ReadAllText(filePath);
        var saveData = JsonSerializer.Deserialize<SaveData<Calculation>>(jsonDataString);
        if (saveData?.Id is null)
        {
            throw new JsonException("Failed to decode savedata.");
        }

        return saveData;
    }

    public List<SaveData<Calculation>> GetAll()
    {
        var saveDataList = new List<SaveData<Calculation>>();
        foreach (string filePath in Directory.GetFiles(_options.Value.FilePath, "*.json"))
        {
            string jsonString = File.ReadAllText(filePath);
            var saveData = JsonSerializer.Deserialize<SaveData<Calculation>>(jsonString);
            if (saveData?.Id is null)
            {
                Console.WriteLine($"Failed to read event {filePath}.json. Likely a corrupted file.");
                continue;
            }
            saveDataList.Add(saveData);
        }

        return saveDataList;
    }
}