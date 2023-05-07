namespace ProbabilityTool.Models.DataModels;

public class SaveData<T>
{
    public string Id { get; set; }
    public T DataObject { get; set; }
}