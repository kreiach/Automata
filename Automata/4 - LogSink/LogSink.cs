using System;
using System.Collections.Generic;

namespace Automata.LogSinks
{
    public abstract class LogSink : ILogSink
    {

        public Dictionary<string, string> parameters;
        public LogEntry logEntry;

        public virtual void Write(LogEntry logEntry)
        {

            if (logEntry is null)
                throw new ArgumentNullException("logEntry", "No log entry has been specified.");

            this.logEntry = logEntry;

            this.Write();

        }

        public void Configure(Dictionary<string, string> parameters)
        {
            this.parameters = parameters;
            this.Configure();
        }

        public abstract void Write();
        public abstract void Configure();

    }
}
