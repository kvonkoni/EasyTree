using System;
using System.Collections;
using System.Collections.Generic;

namespace EasyTree.Iterators
{
    public class PreOrderIterator : IteratorBase
    {
        public PreOrderIterator(Node node) : base(node)
        {
            PreOrder(_node);
        }

        public PreOrderIterator(Node node, PerformFunction function) : base(node)
        {
            PreOrder(_node, function);
        }

        private void PreOrder(Node node)
        {
            _nodelist.Add(node);
            if (!node.IsLeaf)
            {
                foreach (Node child in node.Children)
                {
                    PreOrder(child);
                }
            }
        }

        private void PreOrder(Node node, PerformFunction function)
        {
            _nodelist.Add(node);
            function(node);
            if (!node.IsLeaf)
            {
                foreach (Node child in node.Children)
                {
                    PreOrder(child);
                }
            }
        }
    }
}
