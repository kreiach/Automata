using Automata.LogSinks;
using Automata.Tasks;

namespace Automata.Group
{

    class TaskGroup : ITaskGroup
    {
        public string Name
        {
            get; set;
        }

        public ILogSink LogSink { 
            get; set; 
        }
        
        public ITask[] AutomationTasks { 
            get; set; 
        }

        // Indicate to the manager that the process is ready to run again
        public bool ProcessReady()
        {

            foreach (var task in AutomationTasks)
            {
                if (task.ProcessReady())
                {
                    return true;
                }
            }

            return false;

        }

    }

}
