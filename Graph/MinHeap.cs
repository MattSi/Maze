using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.propig.util.Graph
{
    public class MinHeap<T> where T : IComparable<T>
    {
        IList<T> elements;

        public MinHeap()
        {
            elements = new List<T>();
        }


        public int Count
        {
            get
            {
                return elements.Count;
            }
        }

        public void Add(T Item)
        {
            elements.Add(Item);
            Heapify();
        }

        public void Delete(T Item)
        {
            int i = elements.IndexOf(Item);
            int last = elements.Count - 1;
            elements[i] = elements[last];
            elements.RemoveAt(last);
            Heapify();
        }

        public T PopMin()
        {
            if (elements.Count > 0)
            {
                T item = elements[0];
                Delete(item);
                return item;
            }

            return default(T);
        }

        private void SwapElements(int firstIndex, int secondIndex)
        {
            T tmp = elements[firstIndex];
            elements[firstIndex] = elements[secondIndex];
            elements[secondIndex] = tmp;
        }


        public void Heapify()
        {
            for (int i = elements.Count - 1; i > 0; i--)
            {
                int parentPosition = (i + 1) / 2 - 1;
                parentPosition = parentPosition >= 0 ? parentPosition : 0;

                if (elements[parentPosition].CompareTo(elements[i]) > 0)
                {
                    SwapElements(parentPosition, i);
                }
            }
        }
    }


    public class VectexScore : IComparable<VectexScore>
    {
        int v;
        double score;

        public VectexScore(int v, double score)
        {
            this.v = v;
            this.score = score;
        }

        public int Vertex
        {
            get
            {
                return v;
            }
        }

        public double Score
        {
            get
            {
                return score;
            }
        }

        public int CompareTo(VectexScore other)
        {
            if (other == null)
                return 1;
            return score.CompareTo(other.score);
        }
    }

}
