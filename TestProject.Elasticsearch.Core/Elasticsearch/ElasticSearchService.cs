using Nest;

namespace TestProject.Elasticsearch.Core.Elasticsearch
{
    public class ElasticSearchService : IElasticSearchService
    {
        private readonly IElasticClient _client;

        public ElasticSearchService(IElasticClient client)
        {
            _client = client;
        }


    }
}
