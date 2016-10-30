using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.propig.util.Graph
{
    public class DisjSet 
    {
        int[] m_s;
        int _numConnectedComponent;
        public DisjSet(int vNumber)
        {
            m_s = new int[vNumber];
            for (int i = 0; i < vNumber; i++)
            {
                m_s[i] = i;
            }
            _numConnectedComponent = vNumber;
        }

        public int GetConnectedComponentNumber()
        {
            return _numConnectedComponent;
        }

        public int Find(int v)
        {
            if(v != m_s[v])
            {
                m_s[v] = Find(m_s[v]);
            }

            return m_s[v];
        }

        public void UnionSets(int root1, int root2)
        {
            int a = Find(root1);
            int b = Find(root2);
            if (a != b)
            {
                m_s[b] = a;
                _numConnectedComponent--;
            }
        }
    }
}
