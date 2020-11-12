using System;
using System.Collections.Generic;

using Automata.LogSinks;

namespace Automata.Tasks
{
    public abstract class Task : ITask
    {
        private string _name;
        public string Name 
        {
            get
            {
                return $"{GroupName}.{_name}";
            }
            set
            {
                _name = value;
            }
        }

        public string GroupName { get; set; }

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
                return ProcessTask();
            }

            return false;
        }

    }

}
