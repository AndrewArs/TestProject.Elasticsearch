using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProject.Elasticsearch.DataGenerator.Graph
{
    internal class Graph<T>
    {
        private List<Vertex<T>> _vertices;

        public readonly T SentenceStart;
        public readonly T SentenceEnd;

        public Graph(T sentenceStart, T sentenceEnd)
        {
            SentenceStart = sentenceStart;
            SentenceEnd = sentenceEnd;
            _vertices = new List<Vertex<T>>();
            GetVertex(SentenceStart);
        }

        public void AddNeighborVertex(T vertex, T neighbor)
        {
            var currentVertex = GetVertex(vertex);
            var neighborVertex = GetVertex(neighbor);
            currentVertex.AddNeighbor(neighborVertex);
        }

        public void AddToStart(T neighbor)
        {
            var currentVertex = GetRequiredVertex(SentenceStart);
            var neighborVertex = GetVertex(neighbor);
            currentVertex.AddNeighbor(neighborVertex);
        }

        public void AddEndNeighbor(T vertex)
        {
            var currentVertex = GetRequiredVertex(vertex);
            var neighborVertex = GetVertex(SentenceEnd);
            currentVertex.AddNeighbor(neighborVertex);
        }

        public void AddSentence(IEnumerable<T> values)
        {
            values = values.ToArray();
            var currentValue = values.First();
            AddToStart(currentValue);
            foreach (var value in values.Skip(1))
            {
                AddNeighborVertex(currentValue, value);
                currentValue = value;
            }
            AddEndNeighbor(currentValue);
        }

        public T GetNextVertex(T value)
        {
            var vertex = GetRequiredVertex(value);

            return vertex.GetNextVertex();
        }

        public T GetNextVertexRandomly(T value, Random rand = null)
        {
            var vertex = GetRequiredVertex(value);
            rand = rand ?? new Random();

            return vertex.GetNextVertexRandomly(rand).Data;
        }

        public IEnumerable<T> GetSentence(int maxSize = 10)
        {
            var vertex = GetRequiredVertex(SentenceStart);
            var result = new List<T> {vertex.Data};

            while (maxSize > result.Count || IsSentenceEnd(vertex.Data))
            {
                vertex = vertex.GetNextVertex();
                result.Add(vertex);
            }

            return result;
        }

        public IEnumerable<T> GetSentenceRandomly(Random rand, int maxSize = 10)
        {
            var vertex = GetRequiredVertex(SentenceStart);
            var result = new List<T> { vertex.Data };

            while (maxSize > result.Count && !IsSentenceEnd(vertex.Data))
            {
                vertex = vertex.GetNextVertexRandomly(rand);
                result.Add(vertex);
            }

            return result;
        }

        public bool IsSentenceEnd(T value)
        {
            return EqualityComparer<T>.Default.Equals(value, SentenceEnd);
        }

        private Vertex<T> GetRequiredVertex(T value)
        {
            var vertex = _vertices.FirstOrDefault(x => x == value);

            if (vertex is null)
            {
                throw new ArgumentException($"This value ( {value} ) not in graph");
            }

            return vertex;
        }

        private Vertex<T> GetVertex(T value)
        {
            var vertex = _vertices.FirstOrDefault(x => x == value);
            if (vertex is null)
            {
                vertex = new Vertex<T>(value);
                _vertices.Add(vertex);
            }

            return vertex;
        }
    }
}
