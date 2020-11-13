using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

        public Dictionary<string, string> parameters;

        public string GroupName { get; set; }

        public abstract DateTime NextProcessTimeUTC { get; protected set; }

        protected bool IsConfigured = true;
        public void Configure(Dictionary<string, string> parameters)
        {
            this.parameters = parameters;
            this.Configure();
        }

        protected abstract void Configure();

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
        public bool HasConfiguration()
        {
            return LogSink != null && IsConfigured;
        }

        private static readonly object _lock = new object();

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
