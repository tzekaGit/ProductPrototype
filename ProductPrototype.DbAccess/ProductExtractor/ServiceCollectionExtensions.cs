using Microsoft.Extensions.DependencyInjection;
using ProductPrototype.DbAccess.Interfaces;

namespace ProductPrototype.DbAccess.ProductExtractor;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDatabaseProject(this IServiceCollection services, string configFilePath)
    {
        services.AddSingleton<IJsonConfigStore>(_ => new JsonDocumentConfigStore(Directory.GetCurrentDirectory() + configFilePath));
        return services;
    }
}


public sealed record WriteRulesConfig(int BatchSize, bool UseTransactions);