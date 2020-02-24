using System;
using System.Collections.Generic;
using Xunit;

namespace SimpleTree.XUnitTests
{
    public class UnitTests
    {
        [Fact]
        public void TestConstructor()
        {
            var root = new Node("root");
            var childA = new Node("childA", root);
            var childB = new Node("childB", childA);
            var childC = new Node("childC", childB);

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

            // Making sure the lists of descendants are correct
            Assert.Equal(new HashSet<Node>(){ childA, childB, childC }, root.Descendants);
            Assert.Equal(new HashSet<Node>() { childB, childC }, childA.Descendants);
            Assert.Equal(new HashSet<Node>() { childC }, childB.Descendants);
            Assert.Empty(childC.Descendants);
            
            // Making sure all paths are correct
            Assert.Equal(new List<Node>() { root }, root.Path);
            Assert.Equal(new List<Node>() { root, childA }, childA.Path);
            Assert.Equal(new List<Node>() { root, childA, childB }, childB.Path);
            Assert.Equal(new List<Node>() { root, childA, childB, childC }, childC.Path);
        }

        [Fact]
        public void TestAddChild()
        {
            var root = new Node("root");
            var childA = new Node("childA");
            var childB = new Node("childB");
            var childC = new Node("childC");

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
        public void TestRemoveChild()
        {
            var root = new Node("root");
            var childA = new Node("childA", root);
            
            var childB = new Node("childB", childA);
            var childC = new Node("childC", childB);
            
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
    }
}
