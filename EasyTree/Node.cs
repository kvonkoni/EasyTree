using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EasyTree.Iterators;

namespace EasyTree
{
    public class Node : INotifyPropertyChanged
    {
        public Node Root { get; private set; }

        public Node Parent { get; private set; }

        public bool IsRoot { get; private set; }

        public bool IsLeaf
        {
            get
            {
                return _isLeaf;
            }
            private set
            {
                if (_isLeaf != value)
                {
                    _isLeaf = value;
                    if (_isLeaf)
                    {
                        AddLeaf(this);
                    }
                    else
                    {
                        RemoveLeaf(this);
                    }
                    NotifyPropertyChanged("IsLeaf");
                }
            }
        }

        public IReadOnlyList<Node> Path => path;

        public IReadOnlyList<Node> Children => children;

        public IReadOnlyCollection<Node> Leaves => leaves;

        public IReadOnlyCollection<Node> Descendants => descendants;

        public event PropertyChangedEventHandler PropertyChanged;

        private List<Node> path = new List<Node>();

        private List<Node> children = new List<Node>();

        private HashSet<Node> leaves = new HashSet<Node>();

        private HashSet<Node> descendants = new HashSet<Node>();

        private bool _isLeaf;

        public Node()
        {
            Parent = null;
            IsRoot = true;
            IsLeaf = true;
            Root = this;
            path.Add(this);
        }

        public Node(Node parent)
        {
            Parent = parent;
            IsRoot = false;
            IsLeaf = true;
            Root = this;
            path.Add(this);
            AddParent(parent);
        }

        public void AddChild(Node child)
        {
            if (path.Contains(child))
                throw new TreeStructureException("The child node is in its parent's path. A tree cannot contain a loop.");

            children.Add(child);
            NotifyPropertyChanged("Children");
            
            child.Parent = this;
            child.NotifyPropertyChanged("Parent");

            HashSet<Node>.Enumerator dEnum = child.descendants.GetEnumerator();
            while (dEnum.MoveNext())
            {
                AddDescendant(dEnum.Current);
            }
            AddDescendant(child);
            
            if (IsLeaf)
            {
                IsLeaf = false;
            }

            if (child.IsRoot)
            {
                child.IsRoot = false;
                child.NotifyPropertyChanged("IsRoot");
            }
            
            foreach (Node node in new PreOrderIterator(child))
            {
                node.RedeterminePaths();
            }
        }

        public void RemoveChild(Node child)
        {
            HashSet<Node>.Enumerator lEnum = child.leaves.GetEnumerator();
            while (lEnum.MoveNext())
            {
                RemoveLeaf(lEnum.Current);
            }

            HashSet<Node>.Enumerator dEnum = child.descendants.GetEnumerator();
            while (dEnum.MoveNext())
            {
                RemoveDescendant(dEnum.Current);
            }
            RemoveDescendant(child);

            child.Parent = null;
            child.NotifyPropertyChanged("Parent");
            
            children.Remove(child);
            NotifyPropertyChanged("Children");

            foreach (Node node in new PreOrderIterator(child))
            {
                node.RedeterminePaths();
            }
            
            if (children.Count == 0)
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

        public IEnumerable<Node> GetPreOrderIterator()
        {
            foreach (var node in new PreOrderIterator(this))
                yield return node;
        }

        public IEnumerable<Node> GetPostOrderIterator()
        {
            foreach (var node in new PostOrderIterator(this))
                yield return node;
        }

        public IEnumerable<Node> GetLevelOrderIterator()
        {
            foreach (var node in new LevelOrderIterator(this))
                yield return node;
        }

        private void AddDescendant(Node descendant)
        {
            descendants.Add(descendant);
            NotifyPropertyChanged("Descendants");

            if (Parent != null)
            {
                Parent.AddDescendant(descendant);
            }
        }

        private void RemoveDescendant(Node descendant)
        {
            descendants.Remove(descendant);
            NotifyPropertyChanged("Descendants");
            
            if (Parent != null)
            {
                Parent.RemoveDescendant(descendant);
            }
        }

        private void AddLeaf(Node leaf)
        {
            leaves.Add(leaf);
            NotifyPropertyChanged("Leaves");
            
            if (Parent != null)
            {
                Parent.AddLeaf(leaf);
            }
        }

        private void RemoveLeaf(Node leaf)
        {
            leaves.Remove(leaf);
            NotifyPropertyChanged("Leaves");
            
            if (Parent != null)
            {
                Parent.RemoveLeaf(leaf);
            }

        }

        private void RedeterminePaths()
        {
            if (Parent != null)
            {
                Root = Parent.Root;
                path = new List<Node>(Parent.path)
                {
                    this
                };
            }
            else
            {
                Root = this;
                path = new List<Node>()
                {
                    this
                };
            }

            NotifyPropertyChanged("Root");
            NotifyPropertyChanged("Path");
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
