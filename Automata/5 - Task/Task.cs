using Automata.LogSinks;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Automata.Tasks
{
    public abstract class Task : ITask
    {

        public string Name { get; set; }
        private DateTime NextProcessTimeUTC { get; set; }

        public abstract void Configure(Dictionary<string, string> parameters);
        public abstract bool ProcessTask();

        public ILogSink LogSink
        {
            get; set;
        }

        // Indicate to the manager that the process is ready to run again
        public bool ProcessReady()
        {
            if (!HasConfiguration())
            {
                return false;
            }

            return NextProcessTimeUTC <= DateTime.UtcNow;
        }

        // true if the automation task has all the required properties set
        public virtual bool HasConfiguration()
        {
            return LogSink != null;
        }

        // true if the process ran successfully
        public bool Process()
        {

            if (HasConfiguration() && ProcessReady())
            {
                LogSink.Write(LogEntry.Entry("Process()"));
                return ProcessTask();
            }

            return false;
        }

    }

}
