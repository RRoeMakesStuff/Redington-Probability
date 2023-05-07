namespace ProbabilityTool.DataStore.Interfaces;

public interface IDataStoreWriter
{
    public string SaveDataToStorage<T>(T data);
}