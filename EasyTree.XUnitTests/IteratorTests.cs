using EasyTree.Iterators;
using System.Collections.Generic;
using Xunit;

namespace EasyTree.XUnitTests
{
    public class IteratorTests
    {
        [Fact]
        public void TestPreOrderIterator()
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
