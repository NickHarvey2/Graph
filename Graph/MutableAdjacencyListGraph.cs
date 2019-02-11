using System;
using System.Collections.Generic;
using System.Linq;

namespace Graphs
{
	public class MutableAdjacencyListGraph<T> : ImmutableAdjacencyListGraph<T>,  IMutableGraph<T>
	{
		public MutableAdjacencyListGraph(IEqualityComparer<T> equalityComparer = null) : base(equalityComparer) { }

		public void AddVertex(T vertex)
		{
			if (vertex == null)
			{
				throw new ArgumentNullException(nameof(vertex));
			}

			if (Vertices.Contains(vertex, EqualityComparer))
			{
				throw new ArgumentException($"Vertex {vertex} already in graph");
			}

			_vertices.Add(vertex);
		}

		public void RemoveVertex(T vertex)
		{
			if (vertex == null)
			{
				throw new ArgumentNullException(nameof(vertex));
			}

			if (!Vertices.Contains(vertex, EqualityComparer))
			{
				throw new ArgumentException($"Vertex {vertex} not in graph");
			}

			foreach (var edge in Edges)
			{
				if (EqualityComparer.Equals(edge.From, vertex) || EqualityComparer.Equals(edge.To, vertex))
				{
					_edges.Remove(edge);
				}
			}

			_vertices.Remove(vertex);
		}

		public void AddEdge(T from, T to)
		{
			if (from == null)
			{
				throw new ArgumentNullException(nameof(from));
			}

			if (to == null)
			{
				throw new ArgumentNullException(nameof(to));
			}

			if (!Vertices.Contains(from, EqualityComparer))
			{
				throw new ArgumentException($"Cannot add edge from nonexistant vertex {from}");
			}

			if (!Vertices.Contains(to, EqualityComparer))
			{
				throw new ArgumentException($"Cannot add edge to nonexistant vertex {to}");
			}

			if (Edges.Any(edge => EqualityComparer.Equals(edge.From, from) && EqualityComparer.Equals(edge.To, to)))
			{
				throw new ArgumentException($"Edge from vertex {from} to vertex {to} already exists");
			}

			_edges.Add(new Edge<T>(from, to));
		}

		public void AddEdge(Edge<T> edge)
		{
			AddEdge(edge.From, edge.To);
		}

		public void RemoveEdge(T from, T to)
		{
			if (from == null)
			{
				throw new ArgumentNullException(nameof(from));
			}

			if (to == null)
			{
				throw new ArgumentNullException(nameof(to));
			}

			if (!Vertices.Contains(from, EqualityComparer))
			{
				throw new ArgumentException($"Cannot remove edge from nonexistant vertex {from}");
			}

			if (!Vertices.Contains(to, EqualityComparer))
			{
				throw new ArgumentException($"Cannot remove edge to nonexistant vertex {to}");
			}

			foreach (var edge in Edges)
			{
				if (EqualityComparer.Equals(edge.From, from) && EqualityComparer.Equals(edge.To, to))
				{
					_edges.Remove(edge);
					return;
				}
			}

			throw new ArgumentException($"Cannot remove nonexistant edge from vertex {from} to vertex {to}");
		}

		public void RemoveEdge(Edge<T> edge)
		{
			RemoveEdge(edge.From, edge.To);
		}

		public IImmutableGraph<T> ToImmutableGraph() => new ImmutableAdjacencyListGraph<T>(this);
	}
}