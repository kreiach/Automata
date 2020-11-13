using Automata.LogSinks;
using System;
using System.Diagnostics;

namespace LogSinks
{

    /* writes the logEntry to the specified EventLog */

    public class SystemEventLog : LogSink
    {

        private EventLog eventLog;

        public SystemEventLog() 
        {

        }

        // added for invoking test cases without needing parameters
        public SystemEventLog(EventLog eventLog)
        {

            if (eventLog is null)
                throw new ArgumentNullException("eventLog", "No event log has been specified.");

            if (string.IsNullOrWhiteSpace(eventLog.Log))
                throw new ArgumentNullException("No event log has been specified.");

            if (!string.IsNullOrWhiteSpace(eventLog.Source) && !EventLog.SourceExists(eventLog.Source))
                throw new Exception($"The eventLog.Source ({eventLog.Source}) does not exist.");

            this.eventLog = eventLog;
        }

        private static readonly object _lock = new object();

        public override void Write()
        {

            lock (_lock)
            {

                if (this.eventLog.Log is null)
                {
                    throw new ArgumentNullException("Log", "No event log has been specified.");
                }

                if (logEntry is null)
                {
                    throw new ArgumentNullException("logEntry", "No log entry has been specified.");
                }

                eventLog.WriteEntry(logEntry.Message, (EventLogEntryType)logEntry.EntryType);

            }

        }

        public override void Configure()
        {

            this.eventLog = new EventLog();

            if (parameters.ContainsKey("Source"))
            {
                this.eventLog.Source = parameters?["Source"];
            }

            if (parameters.ContainsKey("Log"))
            {
                this.eventLog.Log = parameters["Log"];
            }

        }

    }

}
