using System;

namespace EasyTree
{
    /// <summary>
    /// The exception that is thrown when the tree structure is invalid.
    /// </summary>
    public class InvalidTreeException : EasyTreeException
    {
        public InvalidTreeException(string message) : base(message)
        {
        }
    }
}
