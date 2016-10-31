using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.propig.util.Graph
{
    public class NullGraphException : Exception
    {
        public NullGraphException(string message) : base(message)
        {

        }
    }
}
