using System.Collections.Generic;

namespace Graphs
{
	/// <inheritdoc />
	/// <summary>
	/// An immutable graph data structure that can contain vertices of type <typeparamref name="T" />
	/// Use this when you want thread safety, or just don't need the mutability
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IImmutableGraph<T> : IEnumerable<T>
	{
		IEnumerable<T> Vertices { get; }

		IEnumerable<Edge<T>> Edges { get; }
		IEqualityComparer<T> EqualityComparer { get; }

		/// <summary>
		/// Returns true if the graph has at least one cycle, otherwise false
		/// </summary>
		/// <returns>true if the graph has at least one cycle, otherwise false</returns>
		bool HasCycle();

		/// <summary>
		/// Returns an <see cref="IEnumerable&lt;IMutableGraph&lt;T&gt;&gt;"/> containing all distinct subgraphs of the graph that are cycles
		/// </summary>
		/// <remarks>
		/// Warning: if graph is significantly complex, this operation may take a significant amount of time
		/// </remarks>
		/// <returns></returns>
		IEnumerable<IImmutableGraph<T>> GetCycles();
	}
}