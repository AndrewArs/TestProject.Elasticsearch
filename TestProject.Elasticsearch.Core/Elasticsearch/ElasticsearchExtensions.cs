using System;
using Elasticsearch.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;

namespace TestProject.Elasticsearch.Core.Elasticsearch
{
    public static class ElasticsearchExtensions
    {
    public static IServiceCollection AddElasticsearch(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        var options = configuration.GetValue<ElasticsearchOptions>("Elasticsearch");

        IConnectionPool connectionPool;

        if (options.ConnectionPool.Length == 1)
        {
            connectionPool = new SingleNodeConnectionPool(options.ConnectionPool[0]);
        }
        else
        {
            throw new NotImplementedException();
        }

        var settings =
            new ConnectionSettings(connectionPool)
                .BasicAuthentication(options.Credentials.Username, options.Credentials.Password);
                //.DefaultIndex(defaultIndex);

        var client = new ElasticClient(settings);

        services.AddSingleton<IElasticClient>(client);
        services.AddSingleton<IElasticSearchService, ElasticSearchService>();

        return services;
    }
}
}
