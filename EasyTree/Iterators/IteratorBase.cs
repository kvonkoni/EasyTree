using System;
using System.Collections;
using System.Collections.Generic;

namespace EasyTree.Iterators
{
    internal abstract class IteratorBase : IEnumerable<Node>
    {
        public delegate void PerformFunction(Node node);

        protected Node _node;

        protected List<Node> _nodelist = new List<Node>();

        public IteratorBase(Node node)
        {
            _node = node;
        }

        public IEnumerator<Node> GetEnumerator()
        {
            return _nodelist.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
