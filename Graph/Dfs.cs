using System;

namespace com.propig.util.Graph
{
    public delegate bool PreVisit(Graph g, int v);

    public delegate bool PostVisit(Graph g, int v);

    public class Dfs : ITravel
    {
        private readonly Graph _graph;

        private readonly PreVisit _preVisit;
        private readonly PostVisit _postVisit;

        public Dfs(Graph g, PreVisit preVisit = null, PostVisit postVisit = null)
        {
            _graph = g;
            _preVisit = preVisit;
            _postVisit = postVisit;
        }

        public void Travel(int vertex = 0)
        {
            if (_graph == null)
                return;
            if (vertex < 0 || vertex > _graph.VerticesNum())
            {
                throw new InvalidVertexException("vertex is invalid.");
            }

            for (int i = 0; i < _graph.VerticesNum(); i++)
            {
                _graph.Mark[i] = VisitedMark.Unvisited;
            }

            do_Travel(vertex);
        }

        private void do_Travel(int vertex)
        {
            _graph.Mark[vertex] = VisitedMark.Visited;
            if (_preVisit != null)
            {
                _preVisit(_graph, vertex);
            }

            for (Edge e = _graph.FirstEdge(vertex); e != null && _graph.IsEdge(e); e = _graph.NextEdge(e))
            {
                if (_graph.Mark[_graph.ToVertex(e)] == VisitedMark.Unvisited)
                {
                    do_Travel(_graph.ToVertex(e));
                }
            }

            if (_postVisit != null)
            {
                _postVisit(_graph, vertex);
            }
        }
    }
}
