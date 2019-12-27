using Microsoft.Extensions.DependencyInjection;
using TestProject.Elasticsearch.DataGenerator.Graph;
using TestProject.Elasticsearch.DataGenerator.MarkovChain;

namespace TestProject.Elasticsearch.DataGenerator
{
    public static class TextGeneratorExtensions
    {
        public static IServiceCollection AddTextGenerator(this IServiceCollection services, string sentenceStart, string sentenceEnd)
        {
            services.AddSingleton<Graph<string>>(provider => new Graph<string>(sentenceStart, sentenceEnd));
            services.AddSingleton<ITextGenerator, MarkovChainTextGenerator>();

            return services;
        }
    }
}
