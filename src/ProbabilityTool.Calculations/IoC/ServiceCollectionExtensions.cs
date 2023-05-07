using Microsoft.Extensions.DependencyInjection;
using ProbabilityTool.Calculations.Services;

namespace ProbabilityTool.Calculations.IoC;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProbabilityCalculations(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICalculationService, CalculationService>();
        return serviceCollection;
    }
}