using System;
using System.Linq;
using System.Xml;

namespace Automata.Configuration
{
    public class AutomataConfiguration
    {

        public ITaskGroupConfiguration[] TaskGroups;
        public ILogSinkConfiguration[] LogSinks;

        private int? pollingInSeconds = null;
        public int? PollingInSeconds
        {
            get 
            {
                return this.pollingInSeconds;
            }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("PollingInSeconds", "Value must be greater that 0.");
                }
                this.pollingInSeconds = value;
            }
        }

        public bool HasConfiguration
        {
            get
            {
                return this.PollingInSeconds != null && this.TaskGroups != null ? (this.TaskGroups.Any() ? (this.LogSinks != null ? this.LogSinks.Any() : false) : false) : false;
            }
        }

    }

}
