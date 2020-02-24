using NLog;
using System;
using System.Collections.Generic;

namespace EasyTree
{
    public class Node
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();

        private bool _isLeaf;
        
        public string Id { get; protected set; }

        public bool IsRoot { get; protected set; }

        public bool IsLeaf
        {
            get
            {
                return _isLeaf;
            }
            protected set
            {
                if (_isLeaf != value)
                {
                    _isLeaf = value;
                    if (_isLeaf)
                    {
                        _log.Trace($"{this} is now a leaf");
                        AddLeaf(this);
                    }
                    else
                    {
                        _log.Trace($"{this} is no longer a leaf");
                        RemoveLeaf(this);
                    }
                }
            }
        }

        public Node Root { get; protected set; }

        public Node Parent { get; protected set; }

        public List<Node> Path { get; protected set; } = new List<Node>();

        public List<Node> Children { get; protected set; } = new List<Node>();

        public HashSet<Node> Leaves { get; protected set; } = new HashSet<Node>();

        public HashSet<Node> Descendants { get; protected set; } = new HashSet<Node>();

        public Node(string id)
        {
            Id = id;
            Parent = null;
            IsRoot = true;
            IsLeaf = true;
            Root = this;
            Path.Add(this);
        }

        public Node(string id, Node parent)
        {
            Id = id;
            Parent = parent;
            IsRoot = false;
            IsLeaf = true;
            Root = Parent.Root;
            Path.Add(this);
            AddParent(parent);
        }

        public void AddChild(Node child)
        {
            _log.Debug($"Adding {this} as parent to {child}");
            
            Children.Add(child);
            child.Parent = this;

            HashSet<Node>.Enumerator dEnum = child.Descendants.GetEnumerator();
            while (dEnum.MoveNext())
            {
                AddDescendant(dEnum.Current);
            }
            AddDescendant(child);
            
            if (IsLeaf)
            {
                _log.Trace($"Setting {this}.IsLeaf to \"false\"");
                IsLeaf = false;
            }

            if (child.IsRoot)
            {
                _log.Trace($"Setting {child}.IsRoot to \"false\"");
                child.IsRoot = false;
            }

            child.RedeterminePaths();
        }

        public void RemoveChild(Node child)
        {
            HashSet<Node>.Enumerator lEnum = child.Leaves.GetEnumerator();
            while (lEnum.MoveNext())
            {
                RemoveLeaf(lEnum.Current);
            }

            HashSet<Node>.Enumerator dEnum = child.Descendants.GetEnumerator();
            while (dEnum.MoveNext())
            {
                RemoveDescendant(dEnum.Current);
            }
            RemoveDescendant(child);

            child.Parent = null;
            Children.Remove(child);
            child.RedeterminePaths();
            
            if (Children.Count == 0)
            {
                IsLeaf = true;
            }
            child.IsRoot = true;
        }

        public void AddParent(Node parent)
        {
            parent.AddChild(this);
        }

        public void RemoveParent()
        {
            Parent.RemoveChild(this);
        }

        protected void AddDescendant(Node descendant)
        {
            _log.Debug($"Adding {descendant} to {this}'s list of descentants");
            Descendants.Add(descendant);
            if (Parent != null)
            {
                Parent.AddDescendant(descendant);
            }
        }

        protected void RemoveDescendant(Node descendant)
        {
            _log.Debug($"Removing {descendant} from {this}'s list of descentants");
            Descendants.Remove(descendant);
            if (Parent != null)
            {
                Parent.RemoveDescendant(descendant);
            }
        }

        protected void AddLeaf(Node leaf)
        {
            _log.Debug($"Adding {leaf} to {this}'s list of leaves");
            Leaves.Add(leaf);
            if (Parent != null)
            {
                Parent.AddLeaf(leaf);
            }
        }

        protected void RemoveLeaf(Node leaf)
        {
            _log.Debug($"Removing {leaf} from {this}'s list of leaves");
            Leaves.Remove(leaf);
            if (Parent != null)
            {
                Parent.RemoveLeaf(leaf);
            }
        }

        protected void RedeterminePaths()
        {
            _log.Debug($"Re-determining {this}'s paths");
            if (Parent != null)
            {
                Root = Parent.Root;
                Path = new List<Node>(Parent.Path)
                {
                    this
                };
            }
            else
            {
                Root = this;
                Path = new List<Node>() { this };
            }
            if (!IsLeaf)
            {
                foreach (Node child in Children)
                {
                    child.RedeterminePaths();
                }
            }
        }

        public override string ToString() => Id;

        public void PrintPretty(string indent, bool last)
        {
            Console.Write(indent);
            if (last)
            {
                Console.Write("\\-");
                indent += "  ";
            }
            else
            {
                Console.Write("|-");
                indent += "| ";
            }
            Console.WriteLine(Id);

            List<Node> cList = new List<Node>(Children);
            for (int i = 0; i < Children.Count; i++)
            {
                cList[i].PrintPretty(indent, i == Children.Count - 1);
            }
        }

        public void PrintPath()
        {
            for (int i=0; i < Path.Count; i++)
            {
                Console.Write($"---> {Path[i]} ");
            }
            Console.WriteLine("");
        }
    }
}
