namespace com.propig.util.Graph
{
    public abstract class Graph : IGraph
    {
        protected int NumVertex;
        protected int NumEdge;
        public VisitedMark[] Mark;
        protected int[] InDegree;
        protected string[] VertexName;

        public bool IsDirected { get; private set; }
        protected Graph(int numVert, bool isDirected = true)
        {
            NumVertex = numVert;
            NumEdge = 0;
            Mark = new VisitedMark[numVert];
            InDegree = new int[numVert];
            VertexName = new string[numVert];
            IsDirected = isDirected;

            for (int i = 0; i < numVert; i++)
            {
                Mark[i] = VisitedMark.Unvisited;
                InDegree[i] = 0;
                VertexName[i] = string.Empty;
            }
        }

        public string GetVertexName(int vertex)
        {
            return VertexName[vertex];
        }

        public void SetVertexName(int vertex, string name)
        {
            if (vertex >= 0 && vertex < NumVertex)
            {
                VertexName[vertex] = name;
            }
        }

        public int VerticesNum()
        {
            return NumVertex;
        }

        public int EdgeNum()
        {
            return NumEdge;
        }

        public bool IsEdge(Edge oneEdge)
        {
            if (oneEdge.Weight > 0 && oneEdge.To >= 0)
            {
                return true;
            }
            return false;
        }

        public int FromVertex(Edge oneEdge)
        {
            return oneEdge.From;
        }

        public int ToVertex(Edge oneEdge)
        {
            return oneEdge.To;
        }

        public int Weight(Edge oneEdge)
        {
            return oneEdge.Weight;
        }
        public abstract Edge FirstEdge(int oneVertex);
        public abstract Edge NextEdge(Edge preEdge);
        public abstract void SetEdge(int fromVertex, int toVertex, int weight);
        public abstract void DelEdge(int fromVertex, int toVertex);
        public abstract bool IsConnected(int fromVertex, int toVertex);

        public int GetDegree(int oneVertex)
        {
            int degree = 0;
            for (Edge e = FirstEdge(oneVertex); e != null; e = NextEdge(e), degree++)
            {
            }

            return degree;
        }
    }
}
