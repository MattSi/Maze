namespace com.propig.util.Graph
{
    public class Edge
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Weight { get; set; }

        public Edge()
        {
            From = -1;
            To = -1;
            Weight = 0;
        }

        public Edge(int f, int t, int w)
        {
            From = f;
            To = t;
            Weight = w;
        }
    }
}
