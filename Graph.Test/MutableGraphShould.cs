using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Graphs.Test
{
	[TestClass]
    public class MutableGraphShould
	{
		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void NotAllowDuplicateVertices()
		{
			IMutableGraph<int> sut = new MutableAdjacencyListGraph<int>();
			sut.AddVertex(1);
			sut.AddVertex(2);
			sut.AddVertex(3);
			sut.AddVertex(2);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void NotAllowDuplicateEdges()
		{
			IMutableGraph<int> sut = new MutableAdjacencyListGraph<int>();
			sut.AddVertex(1);
			sut.AddVertex(2);
			sut.AddVertex(3);

			sut.AddEdge(1, 2);
			sut.AddEdge(1, 3);
			sut.AddEdge(2, 2);
			sut.AddEdge(1, 2);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void NotRemoveNonexistantVertices()
		{
			IMutableGraph<int> sut = new MutableAdjacencyListGraph<int>();
			sut.AddVertex(1);
			sut.AddVertex(2);
			sut.AddVertex(3);

			sut.AddEdge(1, 2);
			sut.AddEdge(1, 3);
			sut.AddEdge(2, 2);

			sut.RemoveVertex(4);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void NotRemoveNonexistantEdges()
		{
			IMutableGraph<int> sut = new MutableAdjacencyListGraph<int>();
			sut.AddVertex(1);
			sut.AddVertex(2);
			sut.AddVertex(3);

			sut.AddEdge(1, 2);
			sut.AddEdge(1, 3);
			sut.AddEdge(2, 2);

			sut.RemoveEdge(3,1);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void NotAddEdgesToNonexistantVertices()
		{
			IMutableGraph<int> sut = new MutableAdjacencyListGraph<int>();
			sut.AddVertex(1);
			sut.AddVertex(2);
			sut.AddVertex(3);

			sut.AddEdge(1, 2);
			sut.AddEdge(1, 3);
			sut.AddEdge(2, 2);

			sut.AddEdge(3, 4);
		}

		[TestMethod, ExpectedException(typeof(ArgumentException))]
		public void NotAddEdgesFromNonexistantVertices()
		{
			IMutableGraph<int> sut = new MutableAdjacencyListGraph<int>();
			sut.AddVertex(1);
			sut.AddVertex(2);
			sut.AddVertex(3);

			sut.AddEdge(1, 2);
			sut.AddEdge(1, 3);
			sut.AddEdge(2, 2);

			sut.AddEdge(4, 3);
		}

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void NotAddNullVertices()
		{
			IMutableGraph<int?> sut = new MutableAdjacencyListGraph<int?>();
			sut.AddVertex(1);
			sut.AddVertex(2);
			sut.AddVertex(3);

			sut.AddVertex(null);
		}

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void NotAddEdgesToNullVertices()
		{
			IMutableGraph<int?> sut = new MutableAdjacencyListGraph<int?>();
			sut.AddVertex(1);
			sut.AddVertex(2);
			sut.AddVertex(3);

			sut.AddEdge(1, null);
		}

		[TestMethod, ExpectedException(typeof(ArgumentNullException))]
		public void NotAddEdgesFromNullVertices()
		{
			IMutableGraph<int?> sut = new MutableAdjacencyListGraph<int?>();
			sut.AddVertex(1);
			sut.AddVertex(2);
			sut.AddVertex(3);

			sut.AddEdge(null, 1);
		}
	}
}
