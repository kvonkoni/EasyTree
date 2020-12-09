using System;
using System.Collections.Generic;

namespace EasyTree.Iterators
{
    internal class PostOrderIterator<T> : IteratorBase<T> where T: Node
    {
        public PostOrderIterator(T node) : base(node)
        {
            PostOrder(_node);
        }

        public PostOrderIterator(T node, Action<T> action) : base(node)
        {
            PostOrder(_node, action);
        }

        private void PostOrder(T node)
        {
            var stack = new Stack<T>();
            var outputStack = new Stack<T>();
            stack.Push(node);
            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                outputStack.Push(currentNode);
                for (int i=0; i < currentNode.Children.Count; i++)
                {
                    stack.Push((T)currentNode.Children[i]);
                }
            }

            while (outputStack.Count > 0)
            {
                _nodelist.Add(outputStack.Pop());
            }
        }

        private void PostOrder(T node, Action<T> action)
        {
            var stack = new Stack<T>();
            var outputStack = new Stack<T>();
            stack.Push(node);
            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                outputStack.Push(currentNode);
                for (int i = 0; i < currentNode.Children.Count; i++)
                {
                    stack.Push((T)currentNode.Children[i]);
                }
            }

            while (outputStack.Count > 0)
            {
                _nodelist.Add(outputStack.Pop());
                action.Invoke(node);
            }
        }
    }
}
