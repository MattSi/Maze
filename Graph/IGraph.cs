namespace com.propig.util.Graph
{
    public interface IGraph
    {
        int VerticesNum();
        int EdgeNum();
        Edge FirstEdge(int oneVertex);
        Edge NextEdge(Edge preEdge);
        void SetEdge(int fromVertex, int toVertex, int weight);
        void DelEdge(int fromVertex, int toVertex);
        bool IsEdge(Edge oneEdge);
        int FromVertex(Edge oneEdge);
        int ToVertex(Edge oneEdge);
        int Weight(Edge oneEdge);
        bool IsConnected(int fromVertex, int toVertex);
        int GetDegree(int oneVertex);

    }
}
