using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTree.Iterators
{
    public class LevelOrderIterator : IteratorBase
    {
        private Queue<Node> _queue;

        public LevelOrderIterator(Node node) : base(node)
        {
            _queue = new Queue<Node>();
            LevelOrder(_node);
        }

        public LevelOrderIterator(Node node, PerformFunction function) : base(node)
        {
            _queue = new Queue<Node>();
            LevelOrder(_node, function);
        }

        private void LevelOrder(Node node)
        {
            _nodelist.Add(node);
            _queue.Enqueue(node);
            while (_queue.Count > 0)
            {
                foreach(Node currentNode in _queue.Dequeue().Children)
                {
                    _nodelist.Add(currentNode);
                    _queue.Enqueue(currentNode);
                }
            }
        }

        private void LevelOrder(Node node, PerformFunction function)
        {
            _nodelist.Add(node);
            function(node);
            _queue.Enqueue(node);
            while (_queue.Count > 0)
            {
                foreach (Node currentNode in _queue.Dequeue().Children)
                {
                    _nodelist.Add(currentNode);
                    function(node);
                    _queue.Enqueue(currentNode);
                }
            }
        }
    }
}
