using System;
using System.Collections.Generic;

namespace EasyTree.Iterators
{
    internal class LevelOrderIterator<T> : IteratorBase<T> where T : Node
    {
        public LevelOrderIterator(T node) : base(node)
        {
            LevelOrder(_node);
        }

        public LevelOrderIterator(T node, Action<T> action) : base(node)
        {
            LevelOrder(_node, action);
        }

        private void LevelOrder(T node)
        {
            var queue = new Queue<T>();
            _nodelist.Add(node);
            queue.Enqueue(node);
            while (queue.Count > 0)
            {
                foreach(T currentNode in queue.Dequeue().Children)
                {
                    _nodelist.Add(currentNode);
                    queue.Enqueue(currentNode);
                }
            }
        }

        private void LevelOrder(T node, Action<T> action)
        {
            var queue = new Queue<T>();
            _nodelist.Add(node);
            action.Invoke(node);
            queue.Enqueue(node);
            while (queue.Count > 0)
            {
                foreach (T currentNode in queue.Dequeue().Children)
                {
                    _nodelist.Add(currentNode);
                    action.Invoke(node);
                    queue.Enqueue(currentNode);
                }
            }
        }
    }
}
