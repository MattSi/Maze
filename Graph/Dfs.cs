using System;

namespace com.propig.util.Graph
{
    public delegate bool PreVisit(Graph g, int v);

    public delegate bool PostVisit(Graph g, int v);

    public class Dfs : ITravel
    {
        private Graph graph;

        private PreVisit preVisit;
        private PostVisit postVisit;

        public Dfs(Graph g, PreVisit preVisit = null, PostVisit postVisit = null)
        {
            this.graph = g;
            this.preVisit = preVisit;
            this.postVisit = postVisit;
        }

        public void Travel(int vertex = 0)
        {
            if (graph == null)
                return;
            if (vertex < 0 || vertex > graph.VerticesNum())
            {
                throw new ArgumentOutOfRangeException("Error Vertex.");
            }

            for (int i = 0; i < graph.VerticesNum(); i++)
            {
                graph.Mark[i] = VisitedMark.Unvisited;
            }

            do_Travel(vertex);
        }

        private void do_Travel(int vertex)
        {
            graph.Mark[vertex] = VisitedMark.Visited;
            if (preVisit != null)
            {
                preVisit(graph, vertex);
            }

            for (Edge e = graph.FirstEdge(vertex); e != null && graph.IsEdge(e); e = graph.NextEdge(e))
            {
                if (graph.Mark[graph.ToVertex(e)] == VisitedMark.Unvisited)
                {
                    do_Travel(graph.ToVertex(e));
                }
            }

            if (postVisit != null)
            {
                postVisit(graph, vertex);
            }
        }
    }
}
