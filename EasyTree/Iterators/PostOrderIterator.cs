using System;
using System.Collections.Generic;

namespace EasyTree.Iterators
{
    internal class PostOrderIterator : IteratorBase, IEnumerable<Node>
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
            var stack = new Stack<Node>();
            var outputStack = new Stack<Node>();
            stack.Push(node);
            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                outputStack.Push(currentNode);
                for (int i=0; i < currentNode.Children.Count; i++)
                {
                    stack.Push(currentNode.Children[i]);
                }
            }

            while (outputStack.Count > 0)
            {
                _nodelist.Add(outputStack.Pop());
            }
        }

        private void PostOrder(Node node, PerformFunction function)
        {
            var stack = new Stack<Node>();
            var outputStack = new Stack<Node>();
            stack.Push(node);
            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                outputStack.Push(currentNode);
                for (int i = 0; i < currentNode.Children.Count; i++)
                {
                    stack.Push(currentNode.Children[i]);
                }
            }

            while (outputStack.Count > 0)
            {
                _nodelist.Add(outputStack.Pop());
                function(node);
            }
        }
    }
}
