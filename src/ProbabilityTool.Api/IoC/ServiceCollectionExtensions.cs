using ProbabilityTool.DataStore.IoC;
using ProbabilityTool.Calculations.IoC;

namespace ProbabilityTool.Api.IoC;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRequiredServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddProbabilityCalculations();
        serviceCollection.AddDataStoreServices();
        return serviceCollection;
    }
}