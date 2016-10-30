using System;

namespace com.propig.util.Graph
{
    public class GraphM : Graph
    {
        private int[][] matrix;
        public GraphM(int numVert, bool isDirected = true) : base(numVert, isDirected)
        {
            int i, j;
            matrix = new int[numVert][];
            for (i = 0; i < numVert; i++)
            {
                matrix[i]=new int[numVert];
            }
            for (i = 0; i < numVert; i++)
            {
                for (j = 0; j < numVert; j++)
                {
                    matrix[i][j] = 0;
                }
            }
        }

        public override Edge FirstEdge(int oneVertex)
        {
            var myEdge = new Edge {From = oneVertex, To = -1};
            for (var i = 0; i < NumVertex; i++)
            {
                if (matrix[oneVertex][i] != 0)
                {
                    myEdge.To = i;
                    myEdge.Weight = matrix[oneVertex][i];
                    break;
                }
                
            }
            return IsEdge(myEdge) ? myEdge : null;
        }

        public override Edge NextEdge(Edge preEdge)
        {
            var myEdge = new Edge {From = preEdge.From, To = -1};
            for (int i = preEdge.To + 1; i < NumVertex; i++)
            {
                if (matrix[preEdge.From][i] != 0)
                {
                    myEdge.To = i;
                    myEdge.Weight = matrix[preEdge.From][i];
                    break;
                }
                
            }
            return IsEdge(myEdge) ? myEdge : null;
        }

        public override void SetEdge(int fromVertex, int toVertex, int weight)
        {
            if (matrix[fromVertex][toVertex] <= 0)
            {
                NumEdge++;
                InDegree[toVertex]++;
            }
            matrix[fromVertex][toVertex] = weight;
            if (!IsDirected)
            {
                matrix[toVertex][fromVertex] = weight;
            }
        }

        public override void DelEdge(int fromVertex, int toVertex)
        {
            if (matrix[fromVertex][toVertex] > 0)
            {
                NumEdge--;
                InDegree[toVertex]--;
            }
            matrix[fromVertex][toVertex] = 0;
            if (!IsDirected)
            {
                matrix[fromVertex][toVertex] = 0;
            }
        }

        public override bool IsConnected(int fromVertex, int toVertex)
        {
            return matrix[fromVertex][toVertex] > 0;
        }

    }
}
