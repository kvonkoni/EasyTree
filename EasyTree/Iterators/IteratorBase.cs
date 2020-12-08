using System;
using System.Collections;
using System.Collections.Generic;

namespace EasyTree.Iterators
{
    internal abstract class IteratorBase : IEnumerable
    {
        public delegate void PerformFunction(Node node);

        protected Node _node;

        protected List<Node> _nodelist;

        public IteratorBase(Node node)
        {
            _node = node;
            _nodelist = new List<Node>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public NodeEnum GetEnumerator()
        {
            return new NodeEnum(_nodelist);
        }
    }
}
