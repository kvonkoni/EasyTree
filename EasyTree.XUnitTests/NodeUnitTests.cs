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
            var childB = new Node();
            var childC = new Node();

            root.AddChild(childA);
            childA.AddChild(childB);
            childB.AddChild(childC);

            // Making sure only 'root' is a root
            Assert.True(root.IsRoot);
            Assert.False(childA.IsRoot);
            Assert.False(childB.IsRoot);
            Assert.False(childC.IsRoot);

            // Making sure each node's root is 'root'
            Assert.Equal(root, root.Root);
            Assert.Equal(root, childA.Root);
            Assert.Equal(root, childB.Root);
            Assert.Equal(root, childC.Root);

            // Making sure only 'childC' is a leaf
            Assert.False(root.IsLeaf);
            Assert.False(childA.IsLeaf);
            Assert.False(childB.IsLeaf);
            Assert.True(childC.IsLeaf);

            // Making sure each node's set of descendants is correct
            Assert.Equal(new HashSet<Node>() { childA, childB, childC }, root.Descendants);
            Assert.Equal(new HashSet<Node>() { childB, childC }, childA.Descendants);
            Assert.Equal(new HashSet<Node>() { childC }, childB.Descendants);
            Assert.Empty(childC.Descendants);

            // Making sure each node's path list is correct
            Assert.Equal(new List<Node>() { root }, root.Path);
            Assert.Equal(new List<Node>() { root, childA }, childA.Path);
            Assert.Equal(new List<Node>() { root, childA, childB }, childB.Path);
            Assert.Equal(new List<Node>() { root, childA, childB, childC }, childC.Path);
        }

        [Fact]
        public void Test_AddChild_Self_TreeStructureExceptionThrown()
        {
            var node = new Node();

            Assert.Throws<TreeStructureException>(() => node.AddChild(node));
        }

        [Fact]
        public void Test_AddChild_Loop_TreeStructureExceptionThrown()
        {
            var nodeA = new Node();
            var nodeB = new Node();

            nodeA.AddChild(nodeB);

            Assert.Throws<TreeStructureException>(() => nodeB.AddChild(nodeA));
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
            Assert.Equal(new HashSet<Node>() { childA }, root.Descendants);
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
            Assert.Equal(new HashSet<Node>() { childC }, childB.Descendants);
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
