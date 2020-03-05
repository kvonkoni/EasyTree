using System;
using System.Collections.Generic;
using System.Text;

namespace EasyTree.ManualTest
{
    public class SampleClass : Node
    {
        public string SampleName;
        // Insert custom properties here

        public SampleClass(string sampleName) : base()
        {
            SampleName = sampleName;
            // Insert custom constructor here
        }

        public SampleClass(string sampleName, SampleClass parent) : base(parent)
        {
            SampleName = sampleName;
            // Insert custom constructor overload here
        }

        public override string ToString() => SampleName;

        // Insert custom methods here
    }
}
