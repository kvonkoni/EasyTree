# SimpleTree

SimpleTree is a project to create a base class for tree-like objects in C#. Inheriting from the Node class provides APIs for creating, managing, and navigating a tree.

## Usage

### Creating a Tree

The basis of the tree is an instance of the Node class. Every node in the tree can be used to create its own subtree, with corresponding methods and properties.

The Node constructor has two overloads:
```
public Node(string id);
public Node(string id, Node parent);
```

You can construct a tree by creating each node and passing in its parent as parameter. The class also features the following methods:

```
// Adds a child node to an existing node
public void AddChild(Node child);

// Removes a child node
public void AddChild(Node child);

// Adds a parent to an existing root node
public void AddParent(Node parent);

// Removes a parent and makes the node a root
public void RemoveParent(Node parent);
```

### Features

Node objects also feature the following convenient read-only properties:
```
// The node's parent
public Node Parent;

// The node's root
public Node Root;

// The full path from the node back to the root
public List<Node> Path;

// List of the node's children
public HashSet<Node> Children;

// List of the node's leaves
public HashSet<Node> Leaves;

// List of the node's descendants
public HashSet<Node> Descendants;

// True if the node is a leaf
public bool IsLeaf;

// True if the node is a root
public bool IsRoot;
```

Node objects also allow you to pretty print the tree to the console using the following method:
```
public void PrintPretty();
```

You can inherit from this class to create your own tree-like class.

## Usage

You can either use the Node class directly or inherit from it to create your own custom class of tree-like objects that makes use of the above APIs.

You can find a minimum working example of a custom class here:

```
using SimpleTree;

namespace CustomNamespace
{
    public class SampleClass : Node
    {
        // Insert custom properties here
        
        public SampleClass(string id) : base(id)
        {
            // Insert custom constructor here
        }

        public SampleClass(string id, SampleClass parent) : base(id, parent)
        {
            // Insert custom constructor overload here
        }

        // Insert custom methods here
    }
}
```

## Contributing

Feel free to contribute by creating an issue or submitting a PR.

## Author

[Kier von Konigslow](https://github.com/kvonkoni)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details