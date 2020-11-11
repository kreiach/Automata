using System.Collections.Generic;

using Automata.LogSinks;

namespace Automata.LogSinks
{
    public interface ILogSink
    {
        void Write(LogEntry logEntry);

        void Configure(Dictionary<string, string> parameters);
    }
}
