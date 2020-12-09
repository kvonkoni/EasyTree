using System;
using System.Collections.Generic;

namespace EasyTree.Iterators
{
    internal class PreOrderIterator<T> : IteratorBase<T> where T : Node
    {
        public PreOrderIterator(T node) : base(node)
        {
            PreOrder(_node);
        }

        public PreOrderIterator(T node, Action<T> action) : base(node)
        {
            PreOrder(_node, action);
        }

        private void PreOrder(T node)
        {
            var stack = new Stack<T>();
            stack.Push(node);
            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                _nodelist.Add(currentNode);
                for (int i=currentNode.Children.Count-1; i >= 0; i--)
                {
                    stack.Push((T)currentNode.Children[i]);
                }
            }
        }

        private void PreOrder(T node, Action<T> function)
        {
            var stack = new Stack<T>();
            stack.Push(node);
            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                _nodelist.Add(currentNode);
                function(currentNode);
                for (int i = currentNode.Children.Count - 1; i >= 0; i--)
                {
                    stack.Push((T)currentNode.Children[i]);
                }
            }
        }
    }
}
