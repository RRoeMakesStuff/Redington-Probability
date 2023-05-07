using Microsoft.Extensions.DependencyInjection;
using ProbabilityTool.DataStore.Interfaces;
using ProbabilityTool.DataStore.Services;
using ProbabilityTool.Models.DataModels;

namespace ProbabilityTool.DataStore.IoC;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataStoreServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IDataStoreWriter, DataStoreWriter>();
        serviceCollection.AddSingleton<IDataStoreReader<Calculation>, CalculationDataStoreReader>();
        return serviceCollection;
    }
}