using System;
using System.Collections.Generic;

namespace EasyTree.Iterators
{
    internal class PreOrderIterator : IteratorBase, IEnumerable<Node>
    {
        public PreOrderIterator(Node node, bool includeRoot = true) : base(node)
        {
            PreOrder(_node, includeRoot);
        }

        public PreOrderIterator(Node node, PerformFunction function, bool includeRoot = true) : base(node)
        {
            PreOrder(_node, function, includeRoot);
        }

        private void PreOrder(Node node, bool includeRoot)
        {
            var stack = new Stack<Node>();

            if (includeRoot)
            {
                stack.Push(node);
                while (stack.Count > 0)
                {
                    var currentNode = stack.Pop();
                    _nodelist.Add(currentNode);

                    for (int i = currentNode.Children.Count - 1; i >= 0; i--)
                    {
                        stack.Push(currentNode.Children[i]);
                    }
                }
            }
            else
            {
                for (int i = node.Children.Count - 1; i >= 0; i--)
                {
                    stack.Push(node.Children[i]);
                }
                while (stack.Count > 0)
                {
                    var currentNode = stack.Pop();
                    _nodelist.Add(currentNode);
                    for (int i = currentNode.Children.Count - 1; i >= 0; i--)
                    {
                        stack.Push(currentNode.Children[i]);
                    }
                }
            }
        }

        private void PreOrder(Node node, PerformFunction function, bool includeRoot)
        {
            var stack = new Stack<Node>();

            if (includeRoot)
            {
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
            else
            {
                for (int i = node.Children.Count - 1; i >= 0; i--)
                {
                    stack.Push(node.Children[i]);
                }
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
}
