using Automata.LogSinks;
using Automata.Tasks;

namespace Automata.Group
{

    class TaskGroup : ITaskGroup
    {

        public ILogSink LogSink { 
            get; set; 
        }
        
        public ITask[] AutomationTasks { 
            get; set; 
        }

    }

}
