using Automata.LogSinks;
using Automata.Tasks;

namespace Automata.Group
{

    public interface ITaskGroup
    {

        string Name
        {
            get; set;
        }

        ILogSink LogSink
        {
            get; set;
        }

        ITask[] AutomationTasks
        {
            get; set;
        }

        bool ProcessReady();
    }

}
