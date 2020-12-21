using System.Collections.Generic;
using Xunit;

namespace EasyTree.XUnitTests
{
    public class NodeUnitTests
    {
        [Fact]
        public void Test_AddChild_ValidChildren_RootLeafDescendantPathPropertiesCorrect()
        {
            var root = new Node();
            var childA = new Node();
            var childAchildA = new Node();
            var childAchildB = new Node();
            var childBchildA = new Node();

            root.AddChild(childA);
            childA.AddChild(childAchildA);
            childA.AddChild(childAchildB);
            childAchildB.AddChild(childBchildA);

            // Making sure only 'root' is a root
            Assert.True(root.IsRoot);
            Assert.False(childA.IsRoot);
            Assert.False(childAchildA.IsRoot);
            Assert.False(childAchildB.IsRoot);
            Assert.False(childBchildA.IsRoot);

            // Making sure each node's root is 'root'
            Assert.Equal(root, root.Root);
            Assert.Equal(root, childA.Root);
            Assert.Equal(root, childAchildA.Root);
            Assert.Equal(root, childAchildB.Root);
            Assert.Equal(root, childBchildA.Root);

            // Making sure only 'childAchildA' and 'childC' are leaves
            Assert.False(root.IsLeaf);
            Assert.False(childA.IsLeaf);
            Assert.True(childAchildA.IsLeaf);
            Assert.False(childAchildB.IsLeaf);
            Assert.True(childBchildA.IsLeaf);

            // Making sure each node's set of descendants is correct
            Assert.True(new HashSet<Node>() { childA, childAchildA, childAchildB, childBchildA }.SetEquals(root.Descendants));
            Assert.True(new HashSet<Node>() { childAchildA, childAchildB, childBchildA }.SetEquals(childA.Descendants));
            Assert.Empty(childAchildA.Descendants);
            Assert.True(new HashSet<Node>() { childBchildA }.SetEquals(childAchildB.Descendants));
            Assert.Empty(childBchildA.Descendants);

            // Making sure each node's path list is correct
            Assert.Equal(new List<Node> { root }, root.Path);
            Assert.Equal(new List<Node> { root, childA }, childA.Path);
            Assert.Equal(new List<Node> { root, childA, childAchildA }, childAchildA.Path);
            Assert.Equal(new List<Node> { root, childA, childAchildB }, childAchildB.Path);
            Assert.Equal(new List<Node> { root, childA, childAchildB, childBchildA }, childBchildA.Path);
        }

        [Fact]
        public void Test_CombineTwoTrees_ValidSubTrees_RootLeafDescendantPathPropertiesCorrect()
        {
            var tree1 = new Node();
            var child1A = new Node();
            var child1B = new Node();
            var child1C = new Node();

            tree1.AddChild(child1A);
            child1A.AddChild(child1B);
            child1B.AddChild(child1C);

            var tree2 = new Node();
            var child2A = new Node();
            var child2B = new Node();
            var child2AchildA = new Node();
            var child2AchildB = new Node();

            tree2.AddChild(child2A);
            tree2.AddChild(child2B);
            child2A.AddChild(child2AchildA);
            child2A.AddChild(child2AchildB);

            var root = new Node();
            root.AddChild(tree1);
            root.AddChild(tree2);

            // Making sure only 'root' is a root
            Assert.True(root.IsRoot);

            Assert.False(tree1.IsRoot);
            Assert.False(child1A.IsRoot);
            Assert.False(child1B.IsRoot);
            Assert.False(child1C.IsRoot);

            Assert.False(tree2.IsRoot);
            Assert.False(child2A.IsRoot);
            Assert.False(child2B.IsRoot);
            Assert.False(child2AchildA.IsRoot);
            Assert.False(child2AchildB.IsRoot);

            // Making sure each node's root is 'root'
            Assert.Equal(root, root.Root);
            
            Assert.Equal(root, tree1.Root);
            Assert.Equal(root, child1A.Root);
            Assert.Equal(root, child1B.Root);
            Assert.Equal(root, child1C.Root);

            Assert.Equal(root, tree2.Root);
            Assert.Equal(root, child2A.Root);
            Assert.Equal(root, child2B.Root);
            Assert.Equal(root, child2AchildA.Root);
            Assert.Equal(root, child2AchildB.Root);

            // Making sure only 'child1C', 'child2B',
            // 'child2AchildA', 'child2AchildB' are leaves
            Assert.False(root.IsLeaf);
            
            Assert.False(tree1.IsLeaf);
            Assert.False(child1A.IsLeaf);
            Assert.False(child1B.IsLeaf);
            Assert.True(child1C.IsLeaf);

            Assert.False(tree2.IsLeaf);
            Assert.False(child2A.IsLeaf);
            Assert.True(child2B.IsLeaf);
            Assert.True(child2AchildA.IsLeaf);
            Assert.True(child2AchildB.IsLeaf);

            // Making sure each node's set of descendants is correct
            Assert.True(new HashSet<Node> { tree1, child1A, child1B, child1C, tree2, child2A, child2B, child2AchildA, child2AchildB }.SetEquals(root.Descendants));
            
            Assert.True(new HashSet<Node> { child1A, child1B, child1C }.SetEquals(tree1.Descendants));
            Assert.True(new HashSet<Node> { child1B, child1C }.SetEquals(child1A.Descendants));
            Assert.True(new HashSet<Node> { child1C }.SetEquals(child1B.Descendants));
            Assert.Empty(child1C.Descendants);

            Assert.True(new HashSet<Node> { child2A, child2B, child2AchildA, child2AchildB }.SetEquals(tree2.Descendants));
            Assert.True(new HashSet<Node> { child2AchildA, child2AchildB }.SetEquals(child2A.Descendants));
            Assert.Empty(child2B.Descendants);
            Assert.Empty(child2AchildA.Descendants);
            Assert.Empty(child2AchildB.Descendants);

            // Making sure each node's path list is correct
            Assert.Equal(new List<Node> { root }, root.Path);

            Assert.Equal(new List<Node> { root, tree1 }, tree1.Path);
            Assert.Equal(new List<Node> { root, tree1, child1A }, child1A.Path);
            Assert.Equal(new List<Node> { root, tree1, child1A, child1B }, child1B.Path);
            Assert.Equal(new List<Node> { root, tree1, child1A, child1B, child1C }, child1C.Path);

            Assert.Equal(new List<Node> { root, tree2 }, tree2.Path);
            Assert.Equal(new List<Node> { root, tree2, child2A }, child2A.Path);
            Assert.Equal(new List<Node> { root, tree2, child2B }, child2B.Path);
            Assert.Equal(new List<Node> { root, tree2, child2A, child2AchildA }, child2AchildA.Path);
            Assert.Equal(new List<Node> { root, tree2, child2A, child2AchildB }, child2AchildB.Path);
        }

        [Fact]
        public void Test_AddChild_Self_TreeStructureExceptionThrown()
        {
            var node = new Node();

            Assert.Throws<InvalidTreeException>(() => node.AddChild(node));
        }

        [Fact]
        public void Test_AddChild_Loop_TreeStructureExceptionThrown()
        {
            var nodeA = new Node();
            var nodeB = new Node();

            nodeA.AddChild(nodeB);

            Assert.Throws<InvalidTreeException>(() => nodeB.AddChild(nodeA));
        }

        [Fact]
        public void Test_RemoveChild_RootLeafDescendantPathPropertiesCorrect()
        {
            var root = new Node();
            var childA = new Node(root);

            var childB = new Node(childA);
            var childC = new Node(childB);

            childA.RemoveChild(childB);

            // Making sure only 'root' is a root
            Assert.True(root.IsRoot);
            Assert.False(childA.IsRoot);

            // Making sure only 'childA' is a leaf
            Assert.False(root.IsLeaf);
            Assert.True(childA.IsLeaf);

            // Making sure each node's root is 'root'
            Assert.Equal(root, root.Root);
            Assert.Equal(root, childA.Root);

            // Making sure each node's set of descendants is correct
            Assert.True(new HashSet<Node>() { childA }.SetEquals(root.Descendants));
            Assert.Empty(childA.Descendants);

            // Making sure each node's path list is correct
            Assert.Equal(new List<Node>() { root }, root.Path);
            Assert.Equal(new List<Node>() { root, childA }, childA.Path);

            // Making sure only 'childB' is a root
            Assert.True(childB.IsRoot);
            Assert.False(childC.IsRoot);

            // Making sure only 'childC' is a leaf
            Assert.False(childB.IsLeaf);
            Assert.True(childC.IsLeaf);

            // Making sure each node's root is 'childB'
            Assert.Equal(childB, childB.Root);
            Assert.Equal(childB, childC.Root);

            // Making sure each node's set of descendants is correct
            Assert.True(new HashSet<Node>() { childC }.SetEquals(childB.Descendants));
            Assert.Empty(childC.Descendants);

            // Making sure each node's path list is correct
            Assert.Equal(new List<Node>() { childB }, childB.Path);
            Assert.Equal(new List<Node>() { childB, childC }, childC.Path);
        }

        [Fact]
        public void Test_GetPreOrderIterator_ValidTree_ReturnExpectedIterator()
        {
            var root = new Node();
            var childA = new Node(root);
            var gChild1 = new Node(childA);
            var gChild2 = new Node(childA);
            var gGChildX = new Node(gChild2);
            var gGGChildM = new Node(gGChildX);
            var gGChildY = new Node(gChild2);
            var childB = new Node(root);
            var childC = new Node(root);
            var gChild3 = new Node(childC);
            var gChild4 = new Node(childC);

            var preOrder = new List<Node>();
            foreach (Node node in root.GetPreOrderIterator())
            {
                preOrder.Add(node);
            }

            var expected = new List<Node>()
            {
                root, childA, gChild1, gChild2, gGChildX, gGGChildM, gGChildY, childB, childC, gChild3, gChild4
            };

            Assert.Equal(expected, preOrder);
        }

        [Fact]
        public void Test_GetPostOrderIterator_ValidTree_ReturnExpectedIterator()
        {
            var root = new Node();
            var childA = new Node(root);
            var gChild1 = new Node(childA);
            var gChild2 = new Node(childA);
            var gGChildX = new Node(gChild2);
            var gGGChildM = new Node(gGChildX);
            var gGChildY = new Node(gChild2);
            var childB = new Node(root);
            var childC = new Node(root);
            var gChild3 = new Node(childC);
            var gChild4 = new Node(childC);

            var postOrder = new List<Node>();
            foreach (Node node in root.GetPostOrderIterator())
            {
                postOrder.Add(node);
            }

            var expected = new List<Node>()
            {
                gChild1, gGGChildM, gGChildX, gGChildY, gChild2, childA, childB, gChild3, gChild4, childC, root
            };

            Assert.Equal(expected, postOrder);
        }

        [Fact]
        public void Test_GetLevelOrderIterator_ValidTree_ReturnExpectedIterator()
        {
            var root = new Node();
            var childA = new Node(root);
            var gChild1 = new Node(childA);
            var gChild2 = new Node(childA);
            var gGChildX = new Node(gChild2);
            var gGGChildM = new Node(gGChildX);
            var gGChildY = new Node(gChild2);
            var childB = new Node(root);
            var childC = new Node(root);
            var gChild3 = new Node(childC);
            var gChild4 = new Node(childC);

            var levelOrder = new List<Node>();
            foreach (Node node in root.GetLevelOrderIterator())
            {
                levelOrder.Add(node);
            }

            var expected = new List<Node>()
            {
                root, childA, childB, childC, gChild1, gChild2, gChild3, gChild4, gGChildX, gGChildY, gGGChildM
            };

            Assert.Equal(expected, levelOrder);
        }
    }
}
