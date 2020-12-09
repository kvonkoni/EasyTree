using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTree
{
    public class NodeException : EasyTreeException
    {
        public NodeException(string message) : base(message)
        {
        }
    }
}
