using Automata.LogSinks;


namespace LogSinks
{

    /* writes the logEntry directory to the console */

    public class Null : LogSink
    {

        public override void Write()
        {

            ; // Do nothing

        }

        public override void Configure()
        {

            ; // Do nothing

        }

    }

}
