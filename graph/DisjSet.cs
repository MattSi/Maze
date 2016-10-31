namespace com.propig.util.Graph
{
    public class DisjSet 
    {
        readonly int[] _mS;
        int _numConnectedComponent;
        public DisjSet(int vNumber)
        {
            _mS = new int[vNumber];
            for (int i = 0; i < vNumber; i++)
            {
                _mS[i] = i;
            }
            _numConnectedComponent = vNumber;
        }

        public int GetConnectedComponentNumber()
        {
            return _numConnectedComponent;
        }

        public int Find(int v)
        {
            if(v != _mS[v])
            {
                _mS[v] = Find(_mS[v]);
            }

            return _mS[v];
        }

        public void UnionSets(int root1, int root2)
        {
            int a = Find(root1);
            int b = Find(root2);
            if (a != b)
            {
                _mS[b] = a;
                _numConnectedComponent--;
            }
        }
    }
}
