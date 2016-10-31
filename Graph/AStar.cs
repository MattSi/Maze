using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.propig.util.Graph
{
    public class AStar
    {
        Graph graph;
        HashSet<int> closedSet;
        MinHeap<VectexScore> openSet;
        int[] cameFrom;
        double[] g_Score;
        double[] h_Score;
        double[] f_Score;

        public AStar(Graph _graph)
        {
            if (_graph == null)
                throw new NullGraphException("Graph is null.");
            graph = _graph;

            closedSet = new HashSet<int>();
            openSet = new MinHeap<VectexScore>();

            g_Score = new double[graph.VerticesNum()];
            h_Score = new double[graph.VerticesNum()];
            f_Score = new double[graph.VerticesNum()];
            cameFrom = new int[graph.VerticesNum()];
        }

        public bool Travel(int start, int goal)
        {
            g_Score[start] = 0;
            h_Score[start] = Distance.GetTaxiCabDistance((SquireGridGraph)graph, start, goal);
            f_Score[start] = h_Score[start];

            openSet.Add(new VectexScore(start, f_Score[start]));

            while (openSet.Count > 0)
            {
                var x = openSet.PopMin();
                if (x.Vertex == goal)
                {
                    return true;
                }
                closedSet.Add(x.Vertex);

                for (Edge e = graph.FirstEdge(x.Vertex); e != null && graph.IsEdge(e); e = graph.NextEdge(e))
                {
                    int neighbor = e.To;
                    if (closedSet.Contains(neighbor))
                        continue;
                }
            }
            return false;
        }
    }
}
