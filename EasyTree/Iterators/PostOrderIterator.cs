﻿using System;
using System.Collections.Generic;

namespace EasyTree.Iterators
{
    internal class PostOrderIterator : IteratorBase, IEnumerable<Node>
    {
        public PostOrderIterator(Node node, bool includeRoot = true) : base(node)
        {
            PostOrder(_node, includeRoot);
        }

        public PostOrderIterator(Node node, PerformFunction function, bool includeRoot = true) : base(node)
        {
            PostOrder(_node, function, includeRoot);
        }

        private void PostOrder(Node node, bool includeRoot)
        {
            var stack = new Stack<Node>();
            var outputStack = new Stack<Node>();

            if (includeRoot)
            {
                stack.Push(node);
                while (stack.Count > 0)
                {
                    var currentNode = stack.Pop();
                    outputStack.Push(currentNode);
                    
                    for (int i = 0; i < currentNode.Children.Count; i++)
                        stack.Push(currentNode.Children[i]);
                }
            }
            else
            {
                for (int i = 0; i < node.Children.Count; i++)
                    stack.Push(node.Children[i]);
                
                while (stack.Count > 0)
                {
                    var currentNode = stack.Pop();
                    outputStack.Push(currentNode);
                    
                    for (int i = 0; i < currentNode.Children.Count; i++)
                        stack.Push(currentNode.Children[i]);
                }
            }

            while (outputStack.Count > 0)
                _nodelist.Add(outputStack.Pop());
        }

        private void PostOrder(Node node, PerformFunction function, bool includeRoot)
        {
            var stack = new Stack<Node>();
            var outputStack = new Stack<Node>();

            if (includeRoot)
            {
                stack.Push(node);
                while (stack.Count > 0)
                {
                    var currentNode = stack.Pop();
                    outputStack.Push(currentNode);
                    
                    for (int i = 0; i < currentNode.Children.Count; i++)
                        stack.Push(currentNode.Children[i]);
                }
            }
            else
            {
                for (int i = 0; i < node.Children.Count; i++)
                    stack.Push(node.Children[i]);

                while (stack.Count > 0)
                {
                    var currentNode = stack.Pop();
                    outputStack.Push(currentNode);

                    for (int i = 0; i < currentNode.Children.Count; i++)
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
