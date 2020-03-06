using EasyTree.Iterators;
using NLog;
using System;

namespace EasyTree.ManualTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new NLog.Config.LoggingConfiguration();

            // Targets where to log to: File and Console
            //var logfile = new NLog.Targets.FileTarget("logfile") { FileName = "file.txt" };
            var logconsole = new NLog.Targets.ConsoleTarget("logconsole");

            // Rules for mapping loggers to targets            
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, logconsole);
            //config.AddRule(LogLevel.Debug, LogLevel.Fatal, logfile);

            // Apply config           
            LogManager.Configuration = config;

            var parent = new Node("parent");
            
            var childA = new Node("childA", parent);
            var childB = new Node("childB", parent);
            var childC = new Node("childC", parent);
            var grandchildX = new Node("childX", childB);
            var grandchildY = new Node("childY", childB);
            var grandchildZ = new Node("childZ", childC);

            grandchildX.PrintPath();
            childA.PrintPath();

            parent.PrintPretty("", true);

            var sampleRoot = new SampleClass("sampleRoot");
            var sampleChildA = new SampleClass("sampleChildA", sampleRoot);
            var sampleGChild1 = new SampleClass("sampleGChild1", sampleChildA);
            var sampleGChild2 = new SampleClass("sampleGChild2", sampleChildA);
            var sampleGGChildX = new SampleClass("sampleGGChildX", sampleGChild2);
            var sampleGGGChildM = new SampleClass("sampleGGGChildM", sampleGGChildX);
            var sampleGGChildY = new SampleClass("sampleGGChildY", sampleGChild2);
            var sampleChildB = new SampleClass("sampleChildB", sampleRoot);
            var sampleChildC = new SampleClass("sampleChildC", sampleRoot);
            var sampleGChild3 = new SampleClass("sampleGChild3", sampleChildC);
            var sampleGChild4 = new SampleClass("sampleGChild4", sampleChildC);

            Console.WriteLine("");
            Console.WriteLine("Sample tree:");
            sampleRoot.PrintPretty("", true);

            Console.WriteLine("");
            Console.WriteLine("Using pre-order depth-first iterator:");
            foreach (Node node in new PreOrderIterator(sampleRoot))
            {
                Console.Write($"---> {node} ");
            }
            Console.WriteLine("");

            Console.WriteLine("");
            Console.WriteLine("Using post-order depth-first iterator:");
            foreach (Node node in new PostOrderIterator(sampleRoot))
            {
                Console.Write($"---> {node} ");
            }
            Console.WriteLine("");

            Console.WriteLine("");
            Console.WriteLine("Using level-order breadth-first iterator:");
            foreach (Node node in new LevelOrderIterator(sampleRoot))
            {
                Console.Write($"---> {node} ");
            }
            Console.WriteLine("");
        }
    }
}
