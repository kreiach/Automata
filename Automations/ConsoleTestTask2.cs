using System;
using System.Collections.Generic;
using System.Threading;
using Automata.LogSinks;
using Automata.Tasks;

namespace Automations
{

    // This class is used for testing with the AutomataHost.Console project
 
    public class ConsoleTestTask2 : Task
    {

        public override DateTime NextProcessTimeUTC { get; protected set; }

        protected override void Configure()
        {
            // check for parameters here 
            // set this.IsConfigured = false if configuration failed;
        }

        // true if the process ran successfully
        public override bool ProcessTask()
        {
            string num = string.Empty;

            if (parameters.ContainsKey("num"))
            {
                num = parameters?["num"];
            }

            Random random = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            int randomNumber = random.Next(5, 20);

            LogSink.Write(LogEntry.Entry($"\t\tTASK: Processing {Name}\t{randomNumber}\t{num}\r\n"));

            Thread.Sleep(3000);

            NextProcessTimeUTC = DateTime.UtcNow.AddSeconds(randomNumber);
            return true;

        }

    }
}
