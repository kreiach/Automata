using System.Collections.Generic;
using Automata.Tasks;

namespace Automations
{
    public class NullTask : Task
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
            return true;
        }

    }
}
