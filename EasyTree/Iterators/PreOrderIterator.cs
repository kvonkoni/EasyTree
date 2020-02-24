using System;
using System.Collections;
using System.Collections.Generic;

namespace EasyTree.Iterators
{
    public class PreOrderIterator : IEnumerable
    {
        private Node _node;

        private List<Node> _nodelist;

        private Node[] _nodearray;

        public PreOrderIterator(Node node)
        {
            _node = node;

            _nodelist = new List<Node>();

            // Insert pre-order search algorithm to populate the list

            _nodearray = _nodelist.ToArray();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public NodeEnum GetEnumerator()
        {
            return new NodeEnum(_nodearray);
        }
    }
}
