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

            Assert.True(root.IsRoot);
            Assert.False(childA.IsRoot);
            Assert.False(childB.IsRoot);
            Assert.False(childC.IsRoot);
            Assert.False(root.IsLeaf);
            Assert.False(childA.IsLeaf);
            Assert.False(childB.IsLeaf);
            Assert.True(childC.IsLeaf);
            Assert.Equal(new HashSet<Node>(){ childA, childB, childC }, root.Descendants);
            Assert.Equal(new HashSet<Node>() { childB, childC }, childA.Descendants);
            Assert.Equal(new HashSet<Node>() { childC }, childB.Descendants);
            Assert.Empty(childC.Descendants);
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

            Assert.True(root.IsRoot);
            Assert.False(childA.IsRoot);
            Assert.False(childB.IsRoot);
            Assert.False(childC.IsRoot);
            Assert.False(root.IsLeaf);
            Assert.False(childA.IsLeaf);
            Assert.False(childB.IsLeaf);
            Assert.True(childC.IsLeaf);
            Assert.Equal(new HashSet<Node>() { childA, childB, childC }, root.Descendants);
            Assert.Equal(new HashSet<Node>() { childB, childC }, childA.Descendants);
            Assert.Equal(new HashSet<Node>() { childC }, childB.Descendants);
            Assert.Empty(childC.Descendants);
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

            Assert.True(root.IsRoot);
            Assert.False(childA.IsRoot);
            Assert.False(root.IsLeaf);
            Assert.True(childA.IsLeaf);
            Assert.Equal(new HashSet<Node>() { childA }, root.Descendants);
            Assert.Empty(childA.Descendants);
            Assert.Equal(new List<Node>() { root }, root.Path);
            Assert.Equal(new List<Node>() { root, childA }, childA.Path);

            Assert.True(childB.IsRoot);
            Assert.False(childC.IsRoot);
            Assert.False(childB.IsLeaf);
            Assert.True(childC.IsLeaf);
            Assert.Equal(new HashSet<Node>() { childC }, childB.Descendants);
            Assert.Empty(childC.Descendants);
            Assert.Equal(new List<Node>() { childB }, childB.Path);
            Assert.Equal(new List<Node>() { childB, childC }, childC.Path);
        }
    }
}
