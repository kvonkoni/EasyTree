using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EasyTree.Iterators
{
    public class NodeEnum : IEnumerator
    {
        public List<Node> _nodelist;

        private int position = -1;

        public NodeEnum(List<Node> nodelist)
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

        public Node Current
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
