namespace Graphs
{
	public class Edge<T>
	{
		public Edge(T from, T to)
		{
			From = from;
			To = to;
		}

		public T From { get; private set; }
		public T To { get; private set; }
	}
}
