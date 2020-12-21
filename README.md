# EasyTree

EasyTree is a project to create a base class for tree-like objects in C#. Inheriting from the Node class provides APIs for creating, managing, and navigating a tree.

## Usage

### Creating a Tree

The basis of the tree is an instance of the Node class in the EasyTree namespace. Every node in the tree can be used to create its own subtree, with corresponding methods and properties.

The Node constructor has two overloads:
```cs
new Node();
new Node(Node parent);
```

You can construct a tree by creating each node and passing in its parent as parameter. The class also features the following parent/child management methods:

```cs
// Adds a child node to an existing node
void AddChild(Node child)

// Removes a child node
void RemoveChild(Node child)

// Adds a parent to an existing node
void AddParent(Node parent)

// Removes a parent and makes the node a root
void RemoveParent(Node parent)
```

### Properties

The Node class features the following convenient properties.

```cs
// The node's parent
Node Parent { get; }

// The node's root
Node Root { get; }

// The full path from the node back to the root
IReadOnlyList<Node> Path { get; }

// List of the node's children in chronological order (the order you added them)
IReadOnlyList<Node> Children { get; }

// True if the node is a leaf
bool IsLeaf { get; }

// True if the node is a root
bool IsRoot { get; }
```

### Searching your Tree

You can iterate through the descendants of any node by using any of the following methods, which return IEnumerator<Node>.

```cs
// Iterate through the tree with root myNode using a depth-first pre-order search
foreach (Node element in myNode.GetPreOrderIterator())
{
    // Insert code
}

// Iterate through the tree with root myNode using a depth-first post-order search
foreach (Node element in myNode.GetPostOrderIterator())
{
    // Insert code
}

// Iterate through the tree with root myNode using a breadth-first level-order search
foreach (Node element in myNode.GetLevelOrderIterator())
{
    // Insert code
}
```

### Other Methods

In addition to the methods above for creating and iterating through a tree, the Node class has a number of methods to make navigating the tree easier.

```cs
// Gets a collection of the descendants of the current node.
IReadOnlyCollection<Node> GetDescendants()

// Gets a collection of the tree's leaves, with the current node as root.
IReadOnlyCollection<Node> GetLeaves()
```

## Usage

### Creating a custom class

You can either use the Node class directly or inherit from it to create your own custom class of tree-like objects that makes use of the above APIs.

You can find a minimum working example of a custom class here:

```cs
using EasyTree;

namespace CustomNameSpace
{
    public class SampleClass : Node
    {
        // Insert custom properties here

        public SampleClass(...) : base()
        {
            // Insert custom constructor here
        }

        public SampleClass(..., SampleClass parent) : base(parent)
        {
            // Insert custom constructor overload here
        }
    }
}
```

## Contributing

Feel free to contribute by creating an issue or submitting a PR.

## Author

[Kier von Konigslow](https://github.com/kvonkoni)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details