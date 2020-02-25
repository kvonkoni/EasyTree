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
    }
}
