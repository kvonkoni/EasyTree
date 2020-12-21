using System;
using System.Collections.Generic;

namespace EasyTree.Iterators
{
    internal class LevelOrderIterator : IteratorBase, IEnumerable<Node>
    {
        public LevelOrderIterator(Node node, bool includeRoot = true) : base(node)
        {
            LevelOrder(_node, includeRoot);
        }

        public LevelOrderIterator(Node node, PerformFunction function, bool includeRoot = true) : base(node)
        {
            LevelOrder(_node, function, includeRoot);
        }

        private void LevelOrder(Node node, bool includeRoot)
        {
            var queue = new Queue<Node>();
            
            if (includeRoot)
                _nodelist.Add(node);
            
            queue.Enqueue(node);
            while (queue.Count > 0)
            {
                foreach(Node currentNode in queue.Dequeue().Children)
                {
                    _nodelist.Add(currentNode);
                    queue.Enqueue(currentNode);
                }
            }
        }

        private void LevelOrder(Node node, PerformFunction function, bool includeRoot)
        {
            var queue = new Queue<Node>();
            
            if (includeRoot)
                _nodelist.Add(node);
            
            function(node);
            queue.Enqueue(node);
            while (queue.Count > 0)
            {
                foreach (Node currentNode in queue.Dequeue().Children)
                {
                    _nodelist.Add(currentNode);
                    function(node);
                    queue.Enqueue(currentNode);
                }
            }
        }
    }
}
