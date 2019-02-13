using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Graphs
{
	public class ImmutableAdjacencyListGraph<T> : IImmutableGraph<T>
	{
		protected readonly HashSet<T> _vertices = new HashSet<T>();
		protected readonly HashSet<Edge<T>> _edges = new HashSet<Edge<T>>();

		public IEnumerable<T> Vertices => ImmutableHashSet<T>.Empty.Union(_vertices);
		public IEnumerable<Edge<T>> Edges => ImmutableHashSet<Edge<T>>.Empty.Union(_edges);
		public IEqualityComparer<T> EqualityComparer { get; }
		private IEqualityComparer<IEnumerable<T>> SetEqualityComparerObj { get; }
		private IEqualityComparer<IEnumerable<Edge<T>>> EdgeSetEqualityComparer { get; }

		protected ImmutableAdjacencyListGraph(IEqualityComparer<T> equalityComparer = null)
		{
			EqualityComparer = equalityComparer ?? new DefaultEqualityComparer();
			SetEqualityComparerObj = new SetEqualityComparer<T>(EqualityComparer);
			EdgeSetEqualityComparer = new SetEqualityComparer<Edge<T>>(new EdgeEqualityComparer(EqualityComparer));
		}

		public ImmutableAdjacencyListGraph(IImmutableGraph<T> originalGraph) : this(originalGraph.EqualityComparer)
		{
			foreach (var vertex in originalGraph.Vertices)
			{
				_vertices.Add(vertex);
			}

			foreach (var edge in originalGraph.Edges)
			{
				_edges.Add(edge);
			}
		}

		public bool HasCycle()
		{
			return _vertices.Any(vertex => HasCycleBeginningAtVertex(vertex, new Stack<T>()));
		}

		private bool HasCycleBeginningAtVertex(T currentVertex, Stack<T> traversedVertices)
		{
			// if the current vertex is in the list of traversed vertices, we have found a cycle
			if (traversedVertices.Contains(currentVertex, EqualityComparer))
			{
				return true;
			}

			// for all the edges originating at the current vertex, check if the vertex the edge is going to is the begining of a cycle
			traversedVertices.Push(currentVertex);
			foreach (var edge in _edges)
			{
				if (EqualityComparer.Equals(edge.From, currentVertex) && HasCycleBeginningAtVertex(edge.To, traversedVertices))
				{
					return true;
				}
			}

			traversedVertices.Pop();

			return false;
		}

		public IEnumerable<IImmutableGraph<T>> GetCycles()
		{
			var cycles = new HashSet<IImmutableGraph<T>>();
			foreach (var vertex in _vertices)
			{
				GetCyclesBeginningAtVertex(vertex, new Stack<T>(), cycles);
			}

			return cycles;
		}

		private void GetCyclesBeginningAtVertex(T currentVertex, Stack<T> traversedVertices, HashSet<IImmutableGraph<T>> cycles)
		{
			// if the current vertex is in the list of traversed vertices, we have found a cycle
			if (traversedVertices.Contains(currentVertex, EqualityComparer))
			{
				// copy the list of traversed vertices, so we don't have to alter the original
				// need to reverse because the enumerator (used in the copy constructor) enumerates the
				// elements of the stack in LIFO order (as one might expect with a stack)
				// meaning the items in the new stack will be in the reverse order of the stack parameter
				// in the constructor
				var traversedVerticesCopy = new Stack<T>(traversedVertices.Reverse());

				// create the subgraph representing the cycle
				var cycle = new MutableAdjacencyListGraph<T>();
				var cycleVertex = traversedVerticesCopy.Pop();
				var firstCycleVertex = cycleVertex;
				cycle.AddVertex(cycleVertex);
				// backtrack along the list of traversed vertices, until we find the current vertex; these vertices made up the cycle
				while (!EqualityComparer.Equals(cycleVertex, currentVertex))
				{
					var prevCycleVertex = cycleVertex;
					cycleVertex = traversedVerticesCopy.Pop();
					cycle.AddVertex(cycleVertex);
					cycle.AddEdge(cycleVertex, prevCycleVertex);
				}
				cycle.AddEdge(firstCycleVertex, cycleVertex);

				// only add the cycle if it is unique
				if (!cycles.Any(identifiedCycle => SetEqualityComparerObj.Equals(identifiedCycle.Vertices, cycle.Vertices) && EdgeSetEqualityComparer.Equals(identifiedCycle.Edges, cycle.Edges)))
				{
					cycles.Add(cycle.ToImmutableGraph());
				}
				return;
			}

			// for all the edges originating at the current vertex, 
			traversedVertices.Push(currentVertex);
			foreach (var edge in _edges.Where(edge => EqualityComparer.Equals(edge.From, currentVertex)))
			{
				GetCyclesBeginningAtVertex(edge.To, traversedVertices, cycles);
			}
			traversedVertices.Pop();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return Vertices.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		private class DefaultEqualityComparer : IEqualityComparer<T>
		{
			public bool Equals(T x, T y)
			{
				if (x == null && y == null)
				{
					return true;
				}

				if (x == null || y == null)
				{
					return false;
				}

				return x.Equals(y);
			}

			public int GetHashCode(T obj)
			{
				return obj.GetHashCode();
			}
		}

		private class EdgeEqualityComparer : IEqualityComparer<Edge<T>>
		{
			private readonly IEqualityComparer<T> _equalityComparer;

			public EdgeEqualityComparer(IEqualityComparer<T> equalityComparer)
			{
				_equalityComparer = equalityComparer;
			}

			public bool Equals(Edge<T> x, Edge<T> y)
			{
				if (x == null && y == null)
				{
					return true;
				}

				if (x == null || y == null)
				{
					return false;
				}

				return _equalityComparer.Equals(x.From, y.From) && _equalityComparer.Equals(x.To, y.To);
			}

			public int GetHashCode(Edge<T> obj)
			{
				throw new NotImplementedException();
			}
		}

		private class SetEqualityComparer<T2> : IEqualityComparer<IEnumerable<T2>>
		{
			private readonly IEqualityComparer<T2> _equalityComparer;

			public SetEqualityComparer(IEqualityComparer<T2> equalityComparer)
			{
				_equalityComparer = equalityComparer;
			}

			public bool Equals(IEnumerable<T2> x, IEnumerable<T2> y)
			{
				if (x.Count() != y.Count())
				{
					return false;
				}

				return x.All(item => y.Contains(item, _equalityComparer));
			}

			public int GetHashCode(IEnumerable<T2> obj)
			{
				return obj.GetHashCode();
			}
		}
	}
}