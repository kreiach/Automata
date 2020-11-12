using System.Collections.Generic;

using Automata.LogSinks;

namespace Automata.Tasks
{
    public interface ITask
    {

        string Name 
        { 
            get; set; 
        }

        string GroupName
        {
            get; set;
        }

        ILogSink LogSink 
        { 
            get; set; 
        }

        void Configure(Dictionary<string, string> parameters);
        bool HasConfiguration();
        bool ProcessReady();
        bool ProcessTask();
        bool Process();
    }
}
