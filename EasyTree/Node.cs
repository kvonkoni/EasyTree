using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using EasyTree.Iterators;

namespace EasyTree
{
    /// <summary>
    /// The <c>Node</c> class. Nodes are connected together to form a tree.
    /// </summary>
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
        /// Gets a value indicating whether this is a leaf node.
        /// </summary>
        public bool IsLeaf
        {
            get
            {
                return Children.Count == 0;
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
        /// Occurs when a property has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        private List<Node> path = new List<Node>();

        private readonly List<Node> children = new List<Node>(1000);

        /// <summary>
        /// Initializes a new instance of the <see cref="Node"/> class.
        /// </summary>
        public Node()
        {
            Parent = null;
            IsRoot = true;
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
            Root = this;
            path.Add(this);
            AddParent(parent);
        }

        /// <summary>
        /// Adds a child node to the end of the list of children.
        /// </summary>
        /// <param name="child"></param>
        /// <exception cref="InvalidTreeException">Thrown when the operation would result in an invalid tree structure.</exception>
        public void AddChild(Node child)
        {
            if (path.Contains(child))
                throw new InvalidTreeException("The child node is in its parent's path. A tree cannot contain a loop.");

            children.Add(child);
            NotifyPropertyChanged("Children");
            
            child.Parent = this;
            child.NotifyPropertyChanged("Parent");

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
        /// <exception cref="NodeException">Thrown when the node is not in the children list.</exception>
        public void RemoveChild(Node child)
        {
            if (!children.Contains(child))
                throw new NodeException("Node is not in list of children.");

            child.Parent = null;
            child.NotifyPropertyChanged("Parent");
            
            children.Remove(child);
            NotifyPropertyChanged("Children");

            foreach (Node node in new PreOrderIterator(child))
            {
                node.RedeterminePaths();
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
            return Enumerable.ToArray(new PreOrderIterator(this, false));
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
