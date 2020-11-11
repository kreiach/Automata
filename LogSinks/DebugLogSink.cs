using Automata.LogSinks;
using System;
using System.Diagnostics;

namespace LogSinks
{

    /* writes the logEntry to the specified EventLog */

    public class DebugWindow : LogSink
    {

        public override void Write()
        {
            Debug.Write($"{logEntry.TimestampUTC}\t{Enum.GetName(typeof(LogEntryType), logEntry.EntryType)}\t{logEntry.Message}");
        }

        public override void Configure()
        {
            ; // Nothing to configure for a debug/immediate sink
        }

    }

}
