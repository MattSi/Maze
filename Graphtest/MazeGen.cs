using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.propig.util.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphTest
{
    [TestClass]
    public class MazeGen
    {
        private int col = 80;
        private int row = 40;

        [TestMethod]
        public void Test_Maze_generate()
        {
            /**
             * 1. 生成SquareGrid图
             * 
             * */

            var graph = new SquireGridGraph(row, col, false);
            DisjSet ds = new DisjSet(graph.VerticesNum());
            Random r = new Random(Environment.TickCount);
            while (ds.Find(0) != ds.Find(graph.VerticesNum() - 1))
            {
                int v1 = r.Next(0, graph.VerticesNum());
                if (graph.GetDegree(v1) >= 3)
                    continue;
                Direction direction = getRandomDirection();
                int v2 = graph.GetNeighborVertex(v1, direction);
                if (v2 != -1)
                {
                    graph.SetEdge(v1, v2, 1);
                    ds.UnionSets(v1, v2);
                }
            }

            Assert.IsTrue(graph.EdgeNum() > 1);
            Assert.IsTrue(ds.GetConnectedComponentNumber() > 1);
        }

        [TestMethod]
        public void Test_Maze_Travel()
        {
            var graph = new SquireGridGraph(row, col, false);
            DisjSet ds = new DisjSet(graph.VerticesNum());
            Random r = new Random(Environment.TickCount);
            while (ds.Find(0) != ds.Find(graph.VerticesNum() - 1))
            {
                int v1 = r.Next(0, graph.VerticesNum());
                if (graph.GetDegree(v1) >= 3)
                    continue;
                Direction direction = getRandomDirection();
                int v2 = graph.GetNeighborVertex(v1, direction);
                if (v2 != -1)
                {
                    graph.SetEdge(v1, v2, 1);
                    ds.UnionSets(v1, v2);
                }
            }

            AStar aStar = new AStar(graph);
            if (aStar.Travel(0, graph.VerticesNum() - 1))
            {
                IList<int> result = aStar.reconstruct_path(graph.VerticesNum() - 1);

                Assert.IsTrue(result.Count > 0);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }
        private Direction getRandomDirection()
        {
            Random r = new Random(Environment.TickCount);
            int d = r.Next(0, 4);
            var result = Enum.Parse(typeof (Direction), d.ToString());

            return (Direction)result;
        }
    }
}
