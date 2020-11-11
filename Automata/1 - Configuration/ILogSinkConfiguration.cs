using System;
using System.Collections.Generic;
using System.Text;

namespace Automata.Configuration
{

    public interface ILogSinkConfiguration
    {

        string Name
        {
            get; set;
        }

        Type LogSink
        {
            get; set;
        }

        Dictionary<string, string> Parameters
        {
            get; set;
        }

    }

}
