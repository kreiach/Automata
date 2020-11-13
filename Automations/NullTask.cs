using System;
using System.Collections.Generic;

using Automata.Tasks;

namespace Automations
{
    public class NullTask : Task
    {

        public override DateTime NextProcessTimeUTC { get; protected set; }

        protected override void Configure()
        {
            ;
        }


        // true if the process ran successfully
        public override bool ProcessTask()
        {
            return true;
        }

    }
}
