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
            Assert.True(new HashSet<Node>(){ childA, childB, childC }.SetEquals(root.Descendants));
            Assert.True(new HashSet<Node>() { childB, childC }.SetEquals(childA.Descendants));
            Assert.True(new HashSet<Node>() { childC }.SetEquals(childB.Descendants));
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
            Assert.True(new HashSet<Node>() { childA, childB, childC }.SetEquals(root.Descendants));
            Assert.True(new HashSet<Node>() { childB, childC }.SetEquals(childA.Descendants));
            Assert.True(new HashSet<Node>() { childC }.SetEquals(childB.Descendants));
            Assert.Equal(new List<Node>() { root }, root.Path);
            Assert.Equal(new List<Node>() { root, childA }, childA.Path);
            Assert.Equal(new List<Node>() { root, childA, childB }, childB.Path);
            Assert.Equal(new List<Node>() { root, childA, childB, childC }, childC.Path);
        }

        [Fact]
        public void TestPathAfterChanges()
        {
            var root = new Node("root");
            var childA = new Node("childA", root);
            var childB = new Node("childB", childA);
            var childC = new Node("childC", childB);
            childB.RemoveChild(childC);

            Assert.Equal(new List<Node>() { root, childA, childB }, childB.Path);
        }
    }
}
