using System;

namespace com.propig.util.Graph
{
    public class InvalidVertexException : Exception
    {
        public InvalidVertexException(string message) : base(message)
        {

        }
    }
}
