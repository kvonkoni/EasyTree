using System;

namespace EasyTree
{
    /// <summary>
    /// Represents errors that occur within the EasyTree class.
    /// </summary>
    public class EasyTreeException : Exception
    {
        public EasyTreeException(string message) : base(message)
        {
        }
    }
}
