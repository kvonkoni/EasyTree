using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTree.Iterators
{
    public class PostOrderIterator : IteratorBase
    {
        public PostOrderIterator(Node node) : base(node)
        {
            PostOrder(_node);
        }

        public PostOrderIterator(Node node, PerformFunction function) : base(node)
        {
            PostOrder(_node, function);
        }

        private void PostOrder(Node node)
        {
            if (!node.IsLeaf)
            {
                foreach (Node child in node.Children)
                {
                    PostOrder(child);
                }
            }
            _nodelist.Add(node);
        }

        private void PostOrder(Node node, PerformFunction function)
        {
            if (!node.IsLeaf)
            {
                foreach (Node child in node.Children)
                {
                    PostOrder(child);
                }
            }
            _nodelist.Add(node);
            function(node);
        }
    }
}
