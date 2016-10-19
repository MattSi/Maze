using System;

namespace com.propig.util.Graph
{
    public class GraphL : Graph
    {
        private LList<ListUnit>[] graList;

        public GraphL(int numVert, bool isDirected = true) : base(numVert, isDirected)
        {
            graList = new LList<ListUnit>[numVert];
            for (int i = 0; i < numVert; i++)
            {
                graList[i] = new LList<ListUnit>();
            }
        }

        public override Edge FirstEdge(int oneVertex)
        {
            var myEdge = new Edge {From = oneVertex, To = -1};
            var temp = graList[oneVertex].Head;
            if (temp.Next != null)
            {
                myEdge.To = temp.Next.Element.Vertex;
                myEdge.Weight = temp.Next.Element.Weight;
            }
            return IsEdge(myEdge) ? myEdge : null;
        }

        public override Edge NextEdge(Edge preEdge)
        {
            var myEdge = new Edge {From = preEdge.From, To = -1};
            var temp = graList[preEdge.From].Head;
            while (temp.Next != null && temp.Next.Element.Vertex <= preEdge.To)
            {
                temp = temp.Next;
            }
            if (temp.Next == null)
            {
                return IsEdge(myEdge) ? myEdge : null;
            }
            myEdge.To = temp.Next.Element.Vertex;
            myEdge.Weight = temp.Next.Element.Weight;
            return IsEdge(myEdge) ? myEdge : null;
        }

        private bool _setEdge(int fromVertex, int toVertex, int weight)
        {
            var temp = graList[fromVertex].Head;
            while (temp.Next != null && temp.Next.Element.Vertex < toVertex)
            {
                temp = temp.Next;
            }
            if (temp.Next == null)
            {
                temp.Next = new LinkItem<ListUnit>(new ListUnit())
                {
                    Element =
                    {
                        Vertex = toVertex,
                        Weight = weight
                    }
                };
                InDegree[toVertex]++;
                return true;
            }
            if (temp.Next.Element.Vertex == toVertex)
            {
                temp.Next.Element.Weight = weight;
                return false;
            }
            if (temp.Next.Element.Vertex > toVertex)
            {
                var other = temp.Next;
                temp.Next = new LinkItem<ListUnit>(new ListUnit())
                {
                    Element =
                    {
                        Vertex = toVertex,
                        Weight = weight
                    },
                    Next = other
                };
                InDegree[toVertex]++;
                return true;
            }
            return false;
        }

        public override void SetEdge(int fromVertex, int toVertex, int weight)
        {
            if (_setEdge(fromVertex, toVertex, weight))
            {
                NumEdge++;
            }
            if (!IsDirected)
            {
                _setEdge(toVertex, fromVertex, weight);
            }
        }


        private bool _delEdge(int fromVertex, int toVertex)
        {
            var temp = graList[fromVertex].Head;
            while (temp.Next != null && temp.Next.Element.Vertex < toVertex)
            {
                temp = temp.Next;
            }
            if (temp.Next == null)
            {
                return false;
            }
            if (temp.Next.Element.Vertex > toVertex)
            {
                return false;
            }
            if (temp.Next.Element.Vertex == toVertex)
            {
                var other = temp.Next.Next;
                temp.Next = other;
                InDegree[toVertex]--;
                return true;
            }
            return false;
        }
        public override void DelEdge(int fromVertex, int toVertex)
        {
            if (_delEdge(fromVertex, toVertex))
            {
                NumEdge--;
            }
            if (!IsDirected)
            {
                _delEdge(toVertex, fromVertex);
            }
        }

        public override bool IsConnected(int fromVertex, int toVertex)
        {
            var temp = graList[fromVertex].Head;
            while (temp.Next != null && temp.Next.Element.Vertex < toVertex)
            {
                temp = temp.Next;
            }

            if (temp.Next == null)
            {
                return false;
            }
            else if (temp.Next.Element.Vertex > toVertex)
            {
                return false;
            }
            return true;
        }
    }
}
