using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Automata.Configuration
{

    public class TaskGroupConfiguration : ITaskGroupConfiguration
    {

        public Type LogSink
        {
            get; set;
        }

        public Tuple<string, Type, Dictionary<string, string>>[] Tasks
        {
            get; set;
        }

        public bool HasConfiguration()
        {

            return Tasks.All(x => x.Item2 != null);

        }

    }

}
