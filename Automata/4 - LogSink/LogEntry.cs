using System;

namespace Automata.LogSinks
{

    public class LogEntry
    {

        public LogEntryType EntryType { get; private set; }
        public string Message { get; private set; }
        public DateTime TimestampUTC { get; private set; }
        
        private static readonly object _lock = new object();
        
        public static LogEntry Entry(string message, LogEntryType entryType = LogEntryType.Information)
        {

            lock (_lock)
            {

                if (message is null)
                {
                    throw new ArgumentNullException("Message", "No message has been specified.");
                }

                return new LogEntry()
                {
                    EntryType = entryType,
                    Message = message,
                    TimestampUTC = DateTime.UtcNow
                };

            }

        }

    }

}
