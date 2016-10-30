using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.propig.util.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraphTest
{
    [TestClass]
    public class GraphCreation
    {
        private StringBuilder stringBuilder = new StringBuilder();

        
        [TestMethod]
        public void TestGraphCreation()
        {
            Graph g = new GraphM(7);
            g.SetEdge(0, 1, 2);
            g.SetEdge(0, 2, 1);
            g.SetEdge(0, 3, 1);
            g.SetEdge(0, 4, 1);
            g.SetEdge(1, 5, 1);
            g.SetEdge(2, 5, 1);
            g.SetEdge(3, 6, 1);
            g.SetEdge(5, 4, 1);
            g.SetEdge(6, 4, 1);
            g.SetEdge(6, 5, 1);

            var edge = g.FirstEdge(0);
            Assert.IsTrue(g.IsEdge(edge));
            Assert.IsTrue(g.Weight(edge) == 2);
            Assert.AreEqual(g.EdgeNum(), 10);
           
        }

        [TestMethod]
        public void Test_Vertex_Are_Connected()
        {

            GraphL g = new GraphL(7);
            g.SetEdge(0, 1, 2);
            g.SetEdge(0, 2, 1);
            g.SetEdge(0, 3, 1);
            g.SetEdge(0, 4, 1);
            g.SetEdge(5, 1, 1);
            g.SetEdge(5, 2, 1);
            g.SetEdge(3, 6, 1);
            g.SetEdge(5, 4, 1);
            g.SetEdge(6, 4, 1);
            g.SetEdge(6, 5, 1);


            Assert.AreEqual(10, g.EdgeNum());
            Assert.IsTrue(g.IsConnected(0, 3));
            Assert.IsTrue(g .IsConnected(0, 4));
            Assert.IsTrue(g.IsConnected(5, 1));
        }
        [TestMethod]
        public void Test_Dfs_Matrix_Directed()
        {
            Graph g = new GraphM(7);
            g.SetEdge(0, 1, 2);
            g.SetEdge(0, 2, 1);
            g.SetEdge(0, 3, 1);
            g.SetEdge(0, 4, 1);
            g.SetEdge(1, 5, 1);
            g.SetEdge(2, 5, 1);
            g.SetEdge(3, 6, 1);
            g.SetEdge(5, 4, 1);
            g.SetEdge(6, 4, 1);
            g.SetEdge(6, 5, 1);


            ITravel dfs = new Dfs(g, preVisit);
            dfs.Travel(0);
            Assert.AreEqual(10, g.EdgeNum());
            Assert.AreEqual("0154236", stringBuilder.ToString());
        }


        [TestMethod]
        public void Test_Dfs_Matrix_Non_Directed()
        {
            Graph g = new GraphM(7, false);
            g.SetEdge(0, 1, 2);
            g.SetEdge(0, 2, 1);
            g.SetEdge(0, 3, 1);
            g.SetEdge(0, 4, 1);
            g.SetEdge(1, 5, 1);
            g.SetEdge(2, 5, 1);
            g.SetEdge(3, 6, 1);
            g.SetEdge(5, 4, 1);
            g.SetEdge(6, 4, 1);
            g.SetEdge(6, 5, 1);
            


            ITravel dfs = new Dfs(g, preVisit);
            dfs.Travel(0);
            Assert.AreEqual(10, g.EdgeNum());
            Assert.AreEqual("0152463", stringBuilder.ToString());
        }

        [TestMethod]
        public void Test_Dfs_List_Directed()
        {
            Graph g = new GraphL(7);
            g.SetEdge(0, 1, 2);
            g.SetEdge(0, 2, 1);
            g.SetEdge(0, 3, 1);
            g.SetEdge(0, 4, 1);
            g.SetEdge(1, 5, 1);
            g.SetEdge(2, 5, 1);
            g.SetEdge(3, 6, 1);
            g.SetEdge(5, 4, 1);
            g.SetEdge(6, 4, 1);
            g.SetEdge(6, 5, 1);


            ITravel dfs = new Dfs(g, preVisit);
            dfs.Travel(0);
            Assert.AreEqual(10, g.EdgeNum());
            Assert.AreEqual("0154236", stringBuilder.ToString());
        }

        [TestMethod]
        public void Test_Dfs_List_Non_Directed()
        {
            Graph g = new GraphL(7, false);
            g.SetEdge(0, 1, 2);
            g.SetEdge(0, 2, 1);
            g.SetEdge(0, 3, 1);
            g.SetEdge(0, 4, 1);
            g.SetEdge(1, 5, 1);
            g.SetEdge(2, 5, 1);
            g.SetEdge(3, 6, 1);
            g.SetEdge(5, 4, 1);
            g.SetEdge(6, 4, 1);
            g.SetEdge(6, 5, 1);


            ITravel dfs = new Dfs(g, preVisit);
            dfs.Travel(0);
            Assert.AreEqual(10, g.EdgeNum());
            Assert.AreEqual("0152463", stringBuilder.ToString());
        }

        [TestMethod]
        public void Test_Bfs_Matrix_Directed()
        {
            Graph g = new GraphM(7);
            g.SetEdge(0, 1, 2);
            g.SetEdge(0, 2, 1);
            g.SetEdge(0, 3, 1);
            g.SetEdge(0, 4, 1);
            g.SetEdge(1, 5, 1);
            g.SetEdge(2, 5, 1);
            g.SetEdge(3, 6, 1);
            g.SetEdge(5, 4, 1);
            g.SetEdge(6, 4, 1);
            g.SetEdge(6, 5, 1);


            ITravel bfs = new Bfs(g, preVisit);
            bfs.Travel(0);
            Assert.AreEqual(10, g.EdgeNum());
            Assert.AreEqual("0123456", stringBuilder.ToString());
            
        }

        [TestMethod]
        public void Test_Bfs_Matrix_Non_Directed()
        {
            Graph g = new GraphM(7, false);
            g.SetEdge(0, 1, 2);
            g.SetEdge(0, 2, 1);
            g.SetEdge(0, 3, 1);
            g.SetEdge(0, 4, 1);
            g.SetEdge(1, 5, 1);
            g.SetEdge(2, 5, 1);
            g.SetEdge(3, 6, 1);
            g.SetEdge(5, 4, 1);
            g.SetEdge(6, 4, 1);
            g.SetEdge(6, 5, 1);


            ITravel bfs = new Bfs(g, preVisit);
            bfs.Travel(0);
            Assert.AreEqual(10, g.EdgeNum());
            Assert.AreEqual("0123456", stringBuilder.ToString());
        }

        [TestMethod]
        public void Test_Bfs_List_Directed()
        {

            Graph g = new GraphL(7);
            g.SetEdge(0, 1, 2);
            g.SetEdge(0, 2, 1);
            g.SetEdge(0, 3, 1);
            g.SetEdge(0, 4, 1);
            g.SetEdge(5, 1, 1);
            g.SetEdge(5, 2, 1);
            g.SetEdge(3, 6, 1);
            g.SetEdge(5, 4, 1);
            g.SetEdge(6, 4, 1);
            g.SetEdge(6, 5, 1);


            ITravel bfs = new Bfs(g, preVisit);
            bfs.Travel(0);
            Assert.AreEqual(10, g.EdgeNum());
            Assert.AreEqual("0123465", stringBuilder.ToString());
        }

        [TestMethod]
        public void Test_Bfs_List_Non_Directed()
        {

            Graph g = new GraphL(7, false);
            g.SetEdge(0, 1, 2);
            g.SetEdge(0, 2, 1);
            g.SetEdge(0, 3, 1);
            g.SetEdge(0, 4, 1);
            g.SetEdge(5, 1, 1);
            g.SetEdge(5, 2, 1);
            g.SetEdge(3, 6, 1);
            g.SetEdge(5, 4, 1);
            g.SetEdge(6, 4, 1);
            g.SetEdge(6, 5, 1);


            ITravel bfs = new Bfs(g, preVisit);
            bfs.Travel(0);
            Assert.AreEqual(10, g.EdgeNum());
            Assert.AreEqual("0123456", stringBuilder.ToString());
        }

        private bool preVisit(Graph g, int v)
        {
            stringBuilder.Append(v.ToString());
            return true;
        }

        [TestMethod]
        public void Test_Degree_of_Graph_Non_Directed()
        {

            Graph g = new GraphL(7, false);
            g.SetEdge(0, 1, 2);
            g.SetEdge(0, 2, 1);
            g.SetEdge(0, 3, 1);
            g.SetEdge(0, 4, 1);
            g.SetEdge(5, 1, 1);
            g.SetEdge(5, 2, 1);
            g.SetEdge(3, 6, 1);
            g.SetEdge(5, 4, 1);
            g.SetEdge(6, 4, 1);
            g.SetEdge(6, 5, 1);


            Assert.AreEqual(4, g.GetDegree(0));
        }

        [TestMethod]
        public void Test_Degree_of_Graph_Directerd()
        {

            Graph g = new GraphL(7, true);
            g.SetEdge(0, 1, 2);
            g.SetEdge(0, 2, 1);
            g.SetEdge(0, 3, 1);
            g.SetEdge(0, 4, 1);
            g.SetEdge(5, 1, 1);
            g.SetEdge(5, 2, 1);
            g.SetEdge(3, 6, 1);
            g.SetEdge(5, 4, 1);
            g.SetEdge(6, 4, 1);
            g.SetEdge(6, 5, 1);


            Assert.AreEqual(3, g.GetDegree(5));
        }
    }
}
