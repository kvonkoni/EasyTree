using System;
using System.Collections.Generic;
using Xunit;

namespace SimpleTree.XUnitTests
{
    public class UnitTests
    {
        [Fact]
        public void TestPath1()
        {
            var root = new Node("root");
            var childA = new Node("childA", root);
            var childB = new Node("childB", childA);
            var childC = new Node("childC", childB);

            Assert.Equal(childC.Path, new List<Node>() { root, childA, childB, childC });
        }
    }
}
