using System;
using System.Collections;
using System.Collections.Generic;

namespace EasyTree.Iterators
{
    internal abstract class IteratorBase<T> : IEnumerable where T : Node
    {
        protected T _node;

        protected List<T> _nodelist = new List<T>();

        public IteratorBase(T node)
        {
            _node = node;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public NodeEnum<T> GetEnumerator()
        {
            return new NodeEnum<T>(_nodelist);
        }
    }
}
