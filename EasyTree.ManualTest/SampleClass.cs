using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTree.ManualTest
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
