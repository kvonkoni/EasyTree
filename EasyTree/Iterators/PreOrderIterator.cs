using System;
using System.Collections;
using System.Collections.Generic;

namespace EasyTree.Iterators
{
    internal class PreOrderIterator : IteratorBase, IEnumerable<Node>
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
            var stack = new Stack<Node>();
            stack.Push(node);
            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                _nodelist.Add(currentNode);
                for (int i=currentNode.Children.Count-1; i >= 0; i--)
                {
                    stack.Push(currentNode.Children[i]);
                }
            }
        }

        private void PreOrder(Node node, PerformFunction function)
        {
            var stack = new Stack<Node>();
            stack.Push(node);
            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                _nodelist.Add(currentNode);
                function(currentNode);
                for (int i = currentNode.Children.Count - 1; i >= 0; i--)
                {
                    stack.Push(currentNode.Children[i]);
                }
            }
        }
    }
}
