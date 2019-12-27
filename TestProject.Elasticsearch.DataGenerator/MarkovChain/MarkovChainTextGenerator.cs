using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TestProject.Elasticsearch.DataGenerator.Graph;

namespace TestProject.Elasticsearch.DataGenerator.MarkovChain
{
    internal class MarkovChainTextGenerator : ITextGenerator
    {
        private readonly Graph<string> _graph;

        public MarkovChainTextGenerator(Graph<string> graph)
        {
            _graph = graph;
        }

        public void SeedChain(string text)
        {
            var sentences = SplitBySentences(text);
            foreach (var sentence in sentences)
            {
                var words = SplitByWords(sentence);
                _graph.AddSentence(words);
            }
        }

        public string GenerateText(Random random, int numberOfSentences = 10)
        {
            var rand = new Random();
            var maxSizeRandom = new Random();
            var sb = new StringBuilder();

            for (var i = 0; i < numberOfSentences; i++)
            {
                var maxSize = maxSizeRandom.Next(3, 20);
                sb.Append(string.Join(" ", _graph.GetSentenceRandomly(rand, maxSize).ToArray()));
            }

            return sb.ToString();
        }

        private static IEnumerable<string> SplitBySentences(string text)
        {
            return Regex.Split(text, @"(?<=[\.!\?])\s+");
        }

        private static IEnumerable<string> SplitByWords(string text)
        {
            return Regex.Split(text, @"\W+");
        }
    }
}
