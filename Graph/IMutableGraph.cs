using System;

namespace Graphs
{
	/// <inheritdoc />
	/// <summary>
	/// A graph data structure that can contain vertices of type <typeparamref name="T"/>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IMutableGraph<T> : IImmutableGraph<T>
	{
		/// <summary>
		/// Adds a vertex to the graph with no edges.
		/// Throws ArgumentException if an identical vertex is already in the graph
		/// </summary>
		/// <param name="vertex"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		void AddVertex(T vertex);

		/// <summary>
		/// Removes the vertex from the graph, if present in the graph, else throws ArgumentException
		/// </summary>
		/// <param name="vertex"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		void RemoveVertex(T vertex);

		/// <summary>
		/// Adds an edge.
		/// Throws ArgumentException if either parameter is not a vertex in the graph
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		void AddEdge(T from, T to);

		/// <summary>
		/// Adds an edge.
		/// Throws ArgumentException if either parameter is not a vertex in the graph
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		void AddEdge(Edge<T> edge);

		/// <summary>
		/// Removes an edge, if present in the graph, else throws ArgumentException.
		/// Throws ArgumentException if either parameter is not a vertex in the graph
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		void RemoveEdge(T from, T to);

		/// <summary>
		/// Removes an edge, if present in the graph, else throws ArgumentException.
		/// Throws ArgumentException if either parameter is not a vertex in the graph
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <exception cref="ArgumentNullException"></exception>
		/// <exception cref="ArgumentException"></exception>
		void RemoveEdge(Edge<T> edge);

		/// <summary>
		/// Returns a clone of the graph in immutable form
		/// </summary>
		/// <returns>IImmutableGraph&lt;T&gt;</returns>
		IImmutableGraph<T> ToImmutableGraph();
	}
}