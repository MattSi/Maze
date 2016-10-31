using System;

namespace com.propig.util.Graph
{
    public static class Distance
    {
        public static double GetTaxiCabDistance(Graph g, int v1, int v2)
        {
            int row1, col1, row2, col2;

            var gg = (SquireGridGraph) g;

            gg.GetRowAndColFromVertex(v1, out row1, out col1);
            gg.GetRowAndColFromVertex(v2, out row2, out col2);


            return Convert.ToDouble((row2 - row1) + (col2 - col1));
        }

        public static double GetEuclideanDistance(SquireGridGraph g, int v1, int v2)
        {
            return 0.0d;
        }
    }
}
