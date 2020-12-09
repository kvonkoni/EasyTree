using System;
using System.Collections;
using System.Collections.Generic;

namespace EasyTree.Iterators
{
    internal class NodeEnum<T> : IEnumerator where T : Node
    {
        public List<T> _nodelist;

        private int position = -1;

        public NodeEnum(List<T> nodelist)
        {
            _nodelist = nodelist;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _nodelist.Count);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public T Current
        {
            get
            {
                try
                {
                    return _nodelist[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
