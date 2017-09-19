using System;
using System.Collections.Generic;

namespace com.propig.util.Graph
{
    public class MinHeap<T> where T : IComparable<T>
    {
        private readonly IList<T> _elements;

        public MinHeap()
        {
            _elements = new List<T>();
        }

        public int Count
        {
            get
            {
                return _elements.Count;
            }
        }

        public void Add(T item)
        {
            _elements.Add(item);
            Heapify();
        }

        public void Delete(T item)
        {
            int i = _elements.IndexOf(item);
            int last = _elements.Count - 1;
            _elements[i] = _elements[last];
            _elements.RemoveAt(last);
            Heapify();
        }

        public T PopMin()
        {
            if (_elements.Count > 0)
            {
                T item = _elements[0];
                Delete(item);
                return item;
            }

            return default(T);
        }

        private void SwapElements(int firstIndex, int secondIndex)
        {
            T tmp = _elements[firstIndex];
            _elements[firstIndex] = _elements[secondIndex];
            _elements[secondIndex] = tmp;
        }

        public void Heapify()
        {
            for (int i = _elements.Count - 1; i > 0; i--)
            {
                int parentPosition = (i + 1) / 2 - 1;
                parentPosition = parentPosition >= 0 ? parentPosition : 0;

                if (_elements[parentPosition].CompareTo(_elements[i]) > 0)
                {
                    SwapElements(parentPosition, i);
                }
            }
        }
    }


    public class VectexScore : IComparable<VectexScore>
    {
        readonly int _v;
        readonly double _score;

        public VectexScore(int v, double score)
        {
            _v = v;
            _score = score;
        }

        public int Vertex
        {
            get
            {
                return _v;
            }
        }

        public double Score
        {
            get
            {
                return _score;
            }
        }

        public int CompareTo(VectexScore other)
        {
            if (other == null)
                return 1;
            return _score.CompareTo(other._score);
        }
    }

}
