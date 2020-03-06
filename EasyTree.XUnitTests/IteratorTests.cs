using EasyTree.Iterators;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EasyTree.XUnitTests
{
    public class IteratorTests
    {
        [Fact]
        public void TestPreOrderIterator()
        {
            var root = new Node("root");
            var childA = new Node("childA", root);
            var gChild1 = new Node("gChild1", childA);
            var gChild2 = new Node("gChild2", childA);
            var gGChildX = new Node("gGChildX", gChild2);
            var gGGChildM = new Node("gGGChildM", gGChildX);
            var gGChildY = new Node("gGChildY", gChild2);
            var childB = new Node("childB", root);
            var childC = new Node("childC", root);
            var gChild3 = new Node("gChild3", childC);
            var gChild4 = new Node("gChild4", childC);

            var preOrder = new List<Node>();
            foreach (Node node in new PreOrderIterator(root))
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
        public void TestPostOrderIterator()
        {
            var root = new Node("root");
            var childA = new Node("childA", root);
            var gChild1 = new Node("gChild1", childA);
            var gChild2 = new Node("gChild2", childA);
            var gGChildX = new Node("gGChildX", gChild2);
            var gGGChildM = new Node("gGGChildM", gGChildX);
            var gGChildY = new Node("gGChildY", gChild2);
            var childB = new Node("childB", root);
            var childC = new Node("childC", root);
            var gChild3 = new Node("gChild3", childC);
            var gChild4 = new Node("gChild4", childC);

            var postOrder = new List<Node>();
            foreach (Node node in new PostOrderIterator(root))
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
        public void TestLevelOrderIterator()
        {
            var root = new Node("root");
            var childA = new Node("childA", root);
            var gChild1 = new Node("gChild1", childA);
            var gChild2 = new Node("gChild2", childA);
            var gGChildX = new Node("gGChildX", gChild2);
            var gGGChildM = new Node("gGGChildM", gGChildX);
            var gGChildY = new Node("gGChildY", gChild2);
            var childB = new Node("childB", root);
            var childC = new Node("childC", root);
            var gChild3 = new Node("gChild3", childC);
            var gChild4 = new Node("gChild4", childC);

            var levelOrder = new List<Node>();
            foreach (Node node in new LevelOrderIterator(root))
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
