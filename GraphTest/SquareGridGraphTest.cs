using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.propig.util.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTest
{
    [TestClass]
    public class SquareGridGraphTest
    {
        private int ROW = 40;
        private int COL = 80;

        private StringBuilder stringBuilder = new StringBuilder();
        [TestMethod]
        public void Test_SGG_Creation()
        {

            var g = new SquireGridGraph(ROW, COL, false);
            

            for (int i = 0; i < g.VerticesNum(); i++)
            {
                int row, col;
                g.GetRowAndColFromVertex(i, out row, out col);

                g.SetNeighbor(row, col, Direction.East, 1);
                g.SetNeighbor(row, col, Direction.South, 1);
                g.SetNeighbor(row, col, Direction.West, 1);
                g.SetNeighbor(row, col, Direction.North, 1);
            }

            ITravel dfs = new Bfs(g, preVisit);
            dfs.Travel(16);
            Assert.AreEqual(6280, g.EdgeNum());
            
        }

        private bool preVisit(Graph g, int v)
        {
            stringBuilder.Append(v.ToString() + ",");
            return true;
        }


        [TestMethod]
        public void Test_Squire_Graph_neighbor()
        {
            var g = new SquireGridGraph(ROW, COL, false);

            int v;

            v = g.GetNeighborVertex(17, Direction.West);
            Assert.AreEqual(16, v);

            v = g.GetNeighborVertex(17, Direction.North);
            Assert.AreEqual(-1, v);

            v = g.GetNeighborVertex(17, Direction.East);
            Assert.AreEqual(18, v);

            v = g.GetNeighborVertex(17, Direction.South);
            Assert.AreEqual(97, v);
        }


        [TestMethod]
        public void Test_Graph_DisjSet()
        {
            var g = new SquireGridGraph(ROW, COL, false);
            DisjSet ds = new DisjSet(g.VerticesNum());

            for (int i = 0; i < g.VerticesNum(); i++)
            {
                int row, col, v;

                g.GetRowAndColFromVertex(i, out row, out col);
                g.SetNeighbor(row, col, Direction.East, 1);
                if ((v = g.GetNeighborVertex(i, Direction.East)) != -1)
                {
                    ds.UnionSets(i, v);
                }

                g.SetNeighbor(row, col, Direction.South, 1);
                if ((v = g.GetNeighborVertex(i, Direction.South)) != -1)
                {
                    ds.UnionSets(i, v);
                }

                g.SetNeighbor(row, col, Direction.West, 1);
                if ((v = g.GetNeighborVertex(i, Direction.West)) != -1)
                {
                    ds.UnionSets(i, v);
                }

                g.SetNeighbor(row, col, Direction.North, 1);
                if ((v = g.GetNeighborVertex(i, Direction.North)) != -1)
                {
                    ds.UnionSets(i, v);
                }
            }

            Assert.AreEqual(1, ds.GetConnectedComponentNumber());
        }
    }

    
}
