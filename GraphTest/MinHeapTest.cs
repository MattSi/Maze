using com.propig.util.Graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTest
{
    [TestClass]
    public class MinHeapTest
    {
        [TestMethod]
        public void Test_Create_Min_Heap()
        {
            MinHeap<VectexScore> mh = new MinHeap<VectexScore>();

            Assert.IsTrue(mh.Count == 0);

            mh.Add(new VectexScore(12, 13.0d));
            mh.Add(new VectexScore(11, 3.0d));
            mh.Add(new VectexScore(2, 5.0d));

            var min = mh.PopMin();
            Assert.IsTrue(min.Score == 3.0d);


            min = mh.PopMin();
            Assert.IsTrue(min.Score == 5.0d);

            min = mh.PopMin();
            Assert.IsTrue(min.Score == 13.0d);

        }
    }
}