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
        private int ROW = 10;
        private int COL = 10;

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

            ITravel dfs = new Dfs(g, preVisit);
            dfs.Travel(16);
            Assert.AreEqual("123", stringBuilder.ToString());
            Assert.AreEqual(12495000, g.EdgeNum());
            
        }

        private bool preVisit(Graph g, int v)
        {
            stringBuilder.Append(v.ToString() + ",");
            return true;
        }
    }

    
}
