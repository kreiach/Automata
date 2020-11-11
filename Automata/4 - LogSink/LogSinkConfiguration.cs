using System;
using System.Collections.Generic;

namespace Automata.Configuration
{

    class LogSinkConfiguration : ILogSinkConfiguration
    {

        public string Name
        {
            get; set;
        }

        public Type LogSink
        {
            get; set;
        }

        public Dictionary<string, string> Parameters
        {
            get; set;
        }

    }

}
