using System.Collections.Generic;

namespace com.propig.util.Graph
{
    public class AStar
    {
        private readonly Graph _graph;
        private readonly HashSet<int> _closedSet;
        private readonly MinHeap<VectexScore> _openSet;
        private readonly ISet<int> _openSetV;
        private readonly int[] _cameFrom;
        private readonly double[] _gScore;
        private readonly double[] _hScore;
        private readonly double[] _fScore;

        public AStar(Graph graph)
        {
            if (graph == null)
                throw new NullGraphException("Graph is null.");
            _graph = graph;

            _closedSet = new HashSet<int>();
            _openSet = new MinHeap<VectexScore>();
            _openSetV = new HashSet<int>();

            _gScore = new double[_graph.VerticesNum()];
            _hScore = new double[_graph.VerticesNum()];
            _fScore = new double[_graph.VerticesNum()];
            _cameFrom = new int[_graph.VerticesNum()];
            for (var i = 0; i < _graph.VerticesNum(); i++)
            {
                _cameFrom[i] = -1;
            }
        }

        public bool Travel(int start, int goal)
        {
            _gScore[start] = 0;
            _hScore[start] = Distance.GetTaxiCabDistance((SquireGridGraph) _graph, start, goal);
            _fScore[start] = _hScore[start];

            _openSet.Add(new VectexScore(start, _fScore[start]));
            _openSetV.Add(start);

            while (_openSet.Count > 0)
            {
                var x = _openSet.PopMin();
                _openSetV.Remove(x.Vertex);

                if (x.Vertex == goal)
                {
                    return true;
                }
                _closedSet.Add(x.Vertex);

                for (Edge e = _graph.FirstEdge(x.Vertex); e != null && _graph.IsEdge(e); e = _graph.NextEdge(e))
                {
                    int neighbor = e.To;
                    bool tentativeBetter = false;
                    double hScore = Distance.GetTaxiCabDistance((SquireGridGraph) _graph, neighbor, goal);

                    if (_closedSet.Contains(neighbor))
                        continue;
                    double tentativeGScore = _gScore[x.Vertex] + 1;

                    if (!_openSetV.Contains(neighbor))
                    {
                        _openSet.Add(new VectexScore(neighbor, tentativeGScore + hScore));
                        _openSetV.Add(neighbor);
                        tentativeBetter = true;
                    }
                    else if (tentativeGScore < _gScore[neighbor])
                    {
                        tentativeBetter = true;
                    }

                    if (tentativeBetter)
                    {
                        _cameFrom[neighbor] = x.Vertex;
                        _gScore[neighbor] = tentativeGScore;
                        _hScore[neighbor] = hScore;
                        _fScore[neighbor] = _gScore[neighbor] + hScore;
                    }
                }
            }
            return false;
        }

        public IList<int> reconstruct_path(int currentNode)
        {
            IList<int> result = new List<int>();
            while (currentNode != -1)
            {
                result.Add(currentNode);
                currentNode = _cameFrom[currentNode];
            }
            return result;
        }
    }
}
