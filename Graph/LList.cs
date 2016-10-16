using System;

namespace com.propig.util.Graph
{
    public class ListUnit
    {
        public int Vertex;
        public int Weight;
    }

    public class LinkItem<TElem>
    {
        public TElem Element { get; set; }
        public LinkItem<TElem> Next;

        public LinkItem(LinkItem<TElem> nextVal = null)
        {
            Next = nextVal;
        }

        public LinkItem(TElem elemval, LinkItem<TElem> nextVal = null)
        {
            Element = elemval;
            Next = nextVal;
        }
    }

    public class LList<TElem> : IDisposable
    {
        public LinkItem<TElem> Head;

        public LList()
        {
            Head = new LinkItem<TElem>();
        }

        private void RemoveAll()
        {
            if (Head != null)
            {
                Head.Next = null;
            }
        }

        public void Dispose()
        {
            RemoveAll();
        }
    }
}
