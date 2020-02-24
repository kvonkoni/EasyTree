using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace EasyTree
{
    public class NodeEnum : IEnumerator
    {
        public Node[] _nodes;

        private int position = -1;

        public NodeEnum(Node[] nodes)
        {
            _nodes = nodes;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _nodes.Length);
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
                    return _nodes[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}
