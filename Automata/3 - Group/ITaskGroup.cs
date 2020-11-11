using Automata.LogSinks;
using Automata.Tasks;

namespace Automata.Group
{

    public interface ITaskGroup
    {

        ILogSink LogSink
        {
            get; set;
        }

        ITask[] AutomationTasks
        {
            get; set;
        }

    }

}
