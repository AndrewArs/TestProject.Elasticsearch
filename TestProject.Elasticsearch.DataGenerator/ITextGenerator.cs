using System;

namespace TestProject.Elasticsearch.DataGenerator
{
    public interface ITextGenerator
    {
        void SeedChain(string text);
        string GenerateText(Random random, int numberOfSentences = 10);
    }
}
