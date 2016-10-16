using System;
using System.Collections.Generic;

namespace com.propig.util.Graph
{
    public delegate bool BfsVisit(Graph g, int v);
    public class Bfs : ITravel
    {
        private Graph graph;
        private BfsVisit visit;
        private Queue<int> q;

        public Bfs(Graph g, BfsVisit bfsVisit = null)
        {
            this.graph = g;
            this.visit = bfsVisit;
            q=new Queue<int>();
        }

        public void Travel(int vertex)
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

            visit(graph, vertex);
            graph.Mark[vertex] = VisitedMark.Visited;
            q.Enqueue(vertex);
            while (q.Count > 0)
            {
                int v = q.Dequeue();
                for (Edge e = graph.FirstEdge(v); e != null && graph.IsEdge(e); e = graph.NextEdge(e))
                {
                    if (graph.Mark[graph.ToVertex(e)] == VisitedMark.Unvisited)
                    {
                        visit(graph, graph.ToVertex(e));
                        graph.Mark[graph.ToVertex(e)] = VisitedMark.Visited;
                        q.Enqueue(graph.ToVertex(e));
                    }
                }
            }
        }
    }
}
