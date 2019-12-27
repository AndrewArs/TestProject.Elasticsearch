using System;
using System.Collections.Generic;
using System.Linq;

namespace TestProject.Elasticsearch.DataGenerator.Graph
{
    internal class Vertex<T>
    {
        private readonly ICollection<Vertex<T>> _neighbors;
        private readonly IDictionary<Vertex<T>, int> _edges;

        public readonly T Data;

        public Vertex(T data)
        {
            Data = data;
            _neighbors = new List<Vertex<T>>();
            _edges = new Dictionary<Vertex<T>, int>();
        }

        public Vertex(T data, ICollection<Vertex<T>> neighbors)
        {
            Data = data;
            _neighbors = neighbors;
            _edges = new Dictionary<Vertex<T>, int>();
        }

        public void AddNeighbor(Vertex<T> vertex)
        {
            _neighbors.Add(vertex);

            if (_edges.TryGetValue(vertex, out var value))
            {
                _edges[vertex] = value + 1;
            }
            else
            {
                _edges.Add(vertex, 1);
            }
        }

        public void RemoveNeighbor(Vertex<T> vertex)
        {
            _neighbors.Remove(vertex);
            if (_edges.TryGetValue(vertex, out var value) && value > 1)
            {
                _edges[vertex] = value - 1;
            }
            else if (value == 1)
            {
                _edges.Remove(vertex);
            }
            else
            {
                throw new ArgumentException($"{vertex.Data} isn't neighbor");
            }
        }

        public Vertex<T> GetNextVertex()
        {
            return _edges.OrderByDescending(x => x.Value).FirstOrDefault().Key;
        }
        
        public Vertex<T> GetNextVertexRandomly(Random rand)
        {
            var max = _neighbors.Count;
            var index = rand.Next(max);

            return _neighbors.ElementAt(index);
        }

        public static bool operator ==(Vertex<T> v1, T value)
        {
            if (v1 == null)
            {
                return false;
            }

            return EqualityComparer<T>.Default.Equals(v1.Data, value);
        }

        public static bool operator !=(Vertex<T> v1, T value)
        {
            return !(v1 == value);
        }

        public static implicit operator T(Vertex<T> v) => v.Data;

        protected bool Equals(Vertex<T> other)
        {
            return EqualityComparer<T>.Default.Equals(Data, other.Data) && Equals(_neighbors, other._neighbors);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Vertex<T>)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = EqualityComparer<T>.Default.GetHashCode(Data);
                hashCode = (hashCode * 397) ^ (_neighbors != null ? _neighbors.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
