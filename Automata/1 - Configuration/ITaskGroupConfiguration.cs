using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Automata.Configuration
{

    public interface ITaskGroupConfiguration
    {
        String Name
        {
            get;
            set;
        }

        Type LogSink
        {
            get; set;
        }

        Tuple<string, Type, Dictionary<string, string>>[] Tasks
        {
            get; set;
        }

        bool HasConfiguration();
    }

}
