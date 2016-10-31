using System.Collections.Generic;

namespace com.propig.util.Graph
{
    public delegate bool BfsVisit(Graph g, int v);
    public class Bfs : ITravel
    {
        private readonly Graph _graph;
        private readonly BfsVisit _visit;
        private readonly Queue<int> _q;

        public Bfs(Graph g, BfsVisit bfsVisit = null)
        {
            _graph = g;
            _visit = bfsVisit;
            _q=new Queue<int>();
        }

        public void Travel(int vertex)
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

            _visit(_graph, vertex);
            _graph.Mark[vertex] = VisitedMark.Visited;
            _q.Enqueue(vertex);
            while (_q.Count > 0)
            {
                int v = _q.Dequeue();
                for (Edge e = _graph.FirstEdge(v); e != null && _graph.IsEdge(e); e = _graph.NextEdge(e))
                {
                    if (_graph.Mark[_graph.ToVertex(e)] == VisitedMark.Unvisited)
                    {
                        _visit(_graph, _graph.ToVertex(e));
                        _graph.Mark[_graph.ToVertex(e)] = VisitedMark.Visited;
                        _q.Enqueue(_graph.ToVertex(e));
                    }
                }
            }
        }
    }
}
