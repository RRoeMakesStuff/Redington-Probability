using ProbabilityTool.Models.DataModels;

namespace ProbabilityTool.DataStore.Interfaces;

public interface IDataStoreReader<T>
{
    public SaveData<T> GetObjectById(string id);
    public List<SaveData<T>> GetAll();
}