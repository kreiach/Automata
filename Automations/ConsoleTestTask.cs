using System.Collections.Generic;

using Automata.LogSinks;
using Automata.Tasks;

namespace Automations
{

    // This class is used for testing with the AutomataHost.Console project
 
    public class ConsoleTestTask : Task
    {

        public override void Configure(Dictionary<string, string> parameters)
        {
            ;
        }

        // true if the automation task has all the required properties set
        public override bool HasConfiguration()
        {
            return base.HasConfiguration();
        }

        // true if the process ran successfully
        public override bool ProcessTask()
        {
            LogSink.Write(LogEntry.Entry($"Processing {Name}"));
            return true;
        }

    }
}
