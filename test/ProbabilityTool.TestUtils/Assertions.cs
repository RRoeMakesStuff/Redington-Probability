using ProbabilityTool.Models.DataModels;
using Xunit;

namespace ProbabilityTool.TestUtils;

public static class Assertions
{
    public static void AssertSaveDataCalculationsAreEqual(SaveData<Calculation> expected, SaveData<Calculation> actual)
    {
        Assert.Equal(expected.Id, actual.Id);
        AssertCalculationsAreEqual(expected.DataObject, actual.DataObject);
    }

    public static void AssertCalculationsAreEqual(Calculation expected, Calculation actual)
    {
        Assert.Equal(expected.Val1, actual.Val1);
        Assert.Equal(expected.Val2, actual.Val2);
        Assert.Equal(expected.Result, actual.Result);
        Assert.Equal(expected.Type, actual.Type);
    }
}