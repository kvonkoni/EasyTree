using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using EasyTree.Iterators;

namespace EasyTree
{
    public class Node : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the tree's root node.
        /// </summary>
        public Node Root { get; private set; }

        /// <summary>
        /// Gets the parent node.
        /// </summary>
        public Node Parent { get; private set; }

        /// <summary>
        /// Gets a value indicating whether this is a root node.
        /// </summary>
        public bool IsRoot { get; private set; }

        /// <summary>
        /// Gets a value indicating wherther this is a leaf node.
        /// </summary>
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

        /// <summary>
        /// Gets an ordered list representing the path from the root.
        /// </summary>
        public IReadOnlyList<Node> Path => path;

        /// <summary>
        /// Gets an ordered list of all the child nodes.
        /// </summary>
        public IReadOnlyList<Node> Children => children;

        /// <summary>
        /// Gets an unordered set collection all the leaf nodes.
        /// </summary>
        public IReadOnlyCollection<Node> Leaves => leaves;

        /// <summary>
        /// Gets an unordered collection of all the descendant nodes.
        /// </summary>
        public IReadOnlyCollection<Node> Descendants => descendants;

        /// <summary>
        /// Occurs when a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private List<Node> path = new List<Node>();

        private List<Node> children = new List<Node>();

        private HashSet<Node> leaves = new HashSet<Node>();

        private HashSet<Node> descendants = new HashSet<Node>();

        private bool _isLeaf;

        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        public Node()
        {
            Parent = null;
            IsRoot = true;
            IsLeaf = true;
            Root = this;
            path.Add(this);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        public Node(Node parent)
        {
            Parent = parent;
            IsRoot = false;
            IsLeaf = true;
            Root = this;
            path.Add(this);
            AddParent(parent);
        }

        /// <summary>
        /// Adds a child node to the end of the list of children.
        /// </summary>
        /// <param name="child"></param>
        public void AddChild(Node child)
        {
            if (path.Contains(child))
                throw new InvalidTreeException("The child node is in its parent's path. A tree cannot contain a loop.");

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

        /// <summary>
        /// Removes a child node from the list of children.
        /// </summary>
        /// <param name="child"></param>
        public void RemoveChild(Node child)
        {
            if (!children.Contains(child))
                throw new NodeException("Node is not in list of children.");

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

        /// <summary>
        /// Adds a parent node.
        /// </summary>
        /// <param name="parent"></param>
        public void AddParent(Node parent)
        {
            parent.AddChild(this);
        }

        /// <summary>
        /// Removes a parent node.
        /// </summary>
        public void RemoveParent()
        {
            Parent.RemoveChild(this);
        }

        /// <summary>
        /// Returns a read-only collection of all the current node's descendants.
        /// </summary>
        public IReadOnlyCollection<Node> GetDescendants()
        {
            return Enumerable.ToArray(new PreOrderIterator(this));
        }

        /// <summary>
        /// Returns a read-only collection of all the current node's leaves.
        /// </summary>
        public IReadOnlyCollection<Node> GetLeaves()
        {
            return Enumerable.ToList(new PreOrderIterator(this)).FindAll(x => x.IsLeaf);
        }

        /// <summary>
        /// Gets a pre-order iterator of all nodes in the tree.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Node> GetPreOrderIterator()
        {
            return new PreOrderIterator(this);
        }

        /// <summary>
        /// Gets a post order iterator of all nodes in the tree.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Node> GetPostOrderIterator()
        {
            return new PostOrderIterator(this);
        }

        /// <summary>
        /// Gets a level-order iterator of all nodes in the tree.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Node> GetLevelOrderIterator()
        {
            return new LevelOrderIterator(this);
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
