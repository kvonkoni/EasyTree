using NLog;
using System;

namespace GeneralTree.ManualTest
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

            foreach (Node leaf in parent.Leaves)
            {
                Console.WriteLine($"{leaf} is parent's leaf");
            }

            foreach (Node leaf in childB.Leaves)
            {
                Console.WriteLine($"{leaf} is childB's leaf");
            }

            grandchildX.PrintPath();
            childA.PrintPath();

            parent.PrintPretty("", true);
        }
    }
}
