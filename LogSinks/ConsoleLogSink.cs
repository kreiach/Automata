using Automata.LogSinks;
using System;

namespace LogSinks
{

    /* writes the logEntry directory to the console */

    public class ConsoleWindow : LogSink
    {

        public override void Write()
        {

            Console.WriteLine($"{logEntry.TimestampUTC}\t{Enum.GetName(typeof(LogEntryType), logEntry.EntryType)}\t{logEntry.Message}");

        }

        public override void Configure()
        {
            ; // Nothing to configure for a console sink
        }

    }

}
