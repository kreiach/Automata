using System;
using System.Collections.Generic;
using System.Linq;

using Automata.Configuration;
using Automata.Group;
using Automata.LogSinks;
using Automata.Tasks;

namespace Automata.Management
{
    public partial class Manager : IManager, IDisposable
    {

        public static bool Enabled;
        public static bool Running;

        private readonly AutomataConfiguration configuration;
        private static List<TaskGroup> taskGroups = new List<TaskGroup>();

        public Manager(AutomataConfiguration configuration)
        {

            Dictionary<Type, ILogSink> logSinks = new Dictionary<Type, ILogSink>();
            
            this.configuration = configuration;
            if (this.configuration == null)
            {
                throw new ArgumentNullException("Configuration is null");
            }

            if (!this.configuration.HasConfiguration)
            {
                throw new Exception("Automata configuration is incomplete.");
            }

            foreach (var group in this.configuration.TaskGroups)
            {
                
                var taskGroup = new TaskGroup();
                taskGroup.Name = group.Name;

                var tasks = new List<ITask>();

                if (!logSinks.ContainsKey(group.LogSink)) 
                {
                    var logSink = (ILogSink)Activator.CreateInstance(group.LogSink);                    
                    Dictionary<string, string> logParameters = this.configuration.LogSinks.Where(s => s.LogSink == group.LogSink).FirstOrDefault()?.Parameters;

                    if (logParameters != null)
                        logSink.Configure(logParameters);

                    logSinks.Add(group.LogSink, logSink);

                }

                taskGroup.LogSink = logSinks[group.LogSink];

                // create task here, and assign it the logsink from the dictionary
                foreach (var task in group.Tasks)
                {

                    var automationTask = (ITask)Activator.CreateInstance(task.Item2);
                    automationTask.LogSink = taskGroup.LogSink;

                    Dictionary<string, string> taskParameters = task.Item3;

                    if (taskParameters != null)
                    { 
                        automationTask.Configure(taskParameters);
                    }

                    automationTask.Name = task.Item1;
                    automationTask.GroupName = taskGroup.Name;

                    tasks.Add(automationTask);
                }

                taskGroup.AutomationTasks = tasks.ToArray();
                taskGroups.Add(taskGroup);

            }

        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing) {
                taskGroups.Clear();
            }
        }

    }
}

