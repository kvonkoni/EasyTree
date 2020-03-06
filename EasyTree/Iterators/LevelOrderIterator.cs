using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTree.Iterators
{
    public class LevelOrderIterator : IteratorBase
    {
        public LevelOrderIterator(Node node) : base(node)
        {
            LevelOrder(_node);
        }

        public LevelOrderIterator(Node node, PerformFunction function) : base(node)
        {
            LevelOrder(_node, function);
        }

        private void LevelOrder(Node node)
        {
            var queue = new Queue<Node>();
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

        private void LevelOrder(Node node, PerformFunction function)
        {
            var queue = new Queue<Node>();
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
