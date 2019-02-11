using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graphs.Test
{
    [TestClass]
    public class ImmutableGraphShould
	{
		[TestMethod]
		public void DetectCycles()
		{
			IMutableGraph<int> sut = new MutableAdjacencyListGraph<int>();
			sut.AddVertex(1);
			sut.AddVertex(2);
			sut.AddVertex(3);
			sut.AddVertex(4);
			sut.AddVertex(5);
			sut.AddVertex(6);
			sut.AddVertex(7);
			sut.AddVertex(8);
			sut.AddVertex(9);

			sut.AddEdge(1, 2);
			sut.AddEdge(2, 3);
			sut.AddEdge(2, 4);
			sut.AddEdge(4, 7);
			sut.AddEdge(1, 3);
			sut.AddEdge(3, 7);
			sut.AddEdge(3, 5);
			sut.AddEdge(6, 7);
			sut.AddEdge(4, 3);
			sut.AddEdge(3, 6);

			Assert.IsFalse(sut.ToImmutableGraph().HasCycle());

			sut.AddEdge(6, 3);

			Assert.IsTrue(sut.ToImmutableGraph().HasCycle());
		}

		[TestMethod]
		public void CorrectlyCountCycles()
		{
			IMutableGraph<int> sut = new MutableAdjacencyListGraph<int>();
			sut.AddVertex(1);
			sut.AddVertex(2);
			sut.AddVertex(3);
			sut.AddVertex(4);
			sut.AddVertex(5);
			sut.AddVertex(6);
			sut.AddVertex(7);
			sut.AddVertex(8);

			sut.AddEdge(1, 2);
			sut.AddEdge(1, 4);
			sut.AddEdge(2, 3);
			sut.AddEdge(2, 4);
			sut.AddEdge(2, 5);
			sut.AddEdge(2, 7);
			sut.AddEdge(3, 1);
			sut.AddEdge(3, 8);
			sut.AddEdge(4, 5);
			sut.AddEdge(4, 6);
			sut.AddEdge(5, 7);
			sut.AddEdge(6, 3);
			sut.AddEdge(7, 3);
			sut.AddEdge(8, 1);

			var cycles = sut.ToImmutableGraph().GetCycles();

			Assert.AreEqual(14, cycles.Count());
		}

		[TestMethod, ExpectedException(typeof(InvalidCastException))]
		public void NotSuccessfullyCastToMutableGraph()
		{
			IMutableGraph<int> sut = new MutableAdjacencyListGraph<int>();
			sut.AddVertex(1);
			sut.AddVertex(2);
			sut.AddVertex(3);
			sut.AddVertex(4);

			sut.AddEdge(1, 2);
			sut.AddEdge(1, 4);
			sut.AddEdge(2, 3);
			sut.AddEdge(2, 4);

			var immutable = sut.ToImmutableGraph();
			var mutable = (MutableAdjacencyListGraph<int>)immutable;
		}

		[TestMethod, ExpectedException(typeof(InvalidCastException))]
		public void NotSuccessfullyCastVertexEnumberableToMutableCollection()
		{
			IMutableGraph<int> sut = new MutableAdjacencyListGraph<int>();
			sut.AddVertex(1);
			sut.AddVertex(2);
			sut.AddVertex(3);
			sut.AddVertex(4);

			sut.AddEdge(1, 2);
			sut.AddEdge(1, 4);
			sut.AddEdge(2, 3);
			sut.AddEdge(2, 4);

			var immutable = sut.ToImmutableGraph();
			var verts = (MutableAdjacencyListGraph<int>)immutable.Vertices;
		}

		[TestMethod, ExpectedException(typeof(InvalidCastException))]
		public void NotSuccessfullyCastEdgeEnumberableToMutableCollection()
		{
			IMutableGraph<int> sut = new MutableAdjacencyListGraph<int>();
			sut.AddVertex(1);
			sut.AddVertex(2);
			sut.AddVertex(3);
			sut.AddVertex(4);

			sut.AddEdge(1, 2);
			sut.AddEdge(1, 4);
			sut.AddEdge(2, 3);
			sut.AddEdge(2, 4);

			var immutable = sut.ToImmutableGraph();
			var edges = (MutableAdjacencyListGraph<Edge<int>>)immutable.Edges;
		}
	}
}
