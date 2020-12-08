using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EasyTree.Iterators;

namespace EasyTree
{
    public class Node : INotifyPropertyChanged
    {
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

        public Node Root { get; private set; }

        public Node Parent { get; private set; }

        public List<Node> Path { get; private set; } = new List<Node>();

        public List<Node> Children { get; private set; } = new List<Node>();

        public HashSet<Node> Leaves { get; private set; } = new HashSet<Node>();

        public HashSet<Node> Descendants { get; private set; } = new HashSet<Node>();
        
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isLeaf;

        public Node()
        {
            Parent = null;
            IsRoot = true;
            IsLeaf = true;
            Root = this;
            Path.Add(this);
        }

        public Node(Node parent)
        {
            Parent = parent;
            IsRoot = false;
            IsLeaf = true;
            Root = this;
            Path.Add(this);
            AddParent(parent);
        }

        public void AddChild(Node child)
        {
            if (Path.Contains(child))
                throw new TreeStructureException("The child node is in its parent's path. A tree cannot contain a loop.");

            Children.Add(child);
            NotifyPropertyChanged("Children");
            
            child.Parent = this;
            child.NotifyPropertyChanged("Parent");

            HashSet<Node>.Enumerator dEnum = child.Descendants.GetEnumerator();
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

            NotifyPropertyChanged(null);
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
            child.NotifyPropertyChanged("Parent");
            
            Children.Remove(child);
            NotifyPropertyChanged("Children");

            foreach (Node node in new PreOrderIterator(child))
            {
                node.RedeterminePaths();
            }
            
            if (Children.Count == 0)
            {
                IsLeaf = true;
            }
            child.IsRoot = true;

            NotifyPropertyChanged(null);
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
            Descendants.Add(descendant);
            NotifyPropertyChanged("Descendants");

            if (Parent != null)
            {
                Parent.AddDescendant(descendant);
            }
        }

        private void RemoveDescendant(Node descendant)
        {
            Descendants.Remove(descendant);
            NotifyPropertyChanged("Descendants");
            
            if (Parent != null)
            {
                Parent.RemoveDescendant(descendant);
            }
        }

        private void AddLeaf(Node leaf)
        {
            Leaves.Add(leaf);
            NotifyPropertyChanged("Leaves");
            
            if (Parent != null)
            {
                Parent.AddLeaf(leaf);
            }
        }

        private void RemoveLeaf(Node leaf)
        {
            Leaves.Remove(leaf);
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
                Path = new List<Node>(Parent.Path)
                {
                    this
                };
            }
            else
            {
                Root = this;
                Path = new List<Node>()
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
