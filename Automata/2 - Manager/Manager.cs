using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;

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


        private Timer timer;

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
                var tasks = new List<Task>();

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
                    automationTask.GroupName = group.Name;

                    tasks.Add((Task)automationTask);
                }

                taskGroup.AutomationTasks = tasks.ToArray();
                taskGroups.Add(taskGroup);

            }

        }


        //public void StartGoverner()
        //{

        //    timer = new Timer();

        //    timer.Elapsed += new ElapsedEventHandler(Governer);
        //    timer.Interval = (double)configuration.PollingInSeconds * 1000;
        //    timer.AutoReset = true;
        //    timer.Enabled = true;
        //    timer.Start();

        //}

 



        //    void EventPublish(IMetric publishMetric)
        //    {
        //        if (this.configuration == null) return;

        //        publishMetric.Dimensions = this.Dimensions;

        //        System.Diagnostics.Trace.WriteLine(ConsoleMetricsSink.MetricString(new[] { publishMetric }));
        //        System.Diagnostics.Trace.Flush();

        //        foreach (var group in configuration.Groups)
        //        {
        //            var sink = group.Sink;
        //            foreach (var metricType in group.MetricsTypes)
        //            {
        //                if (publishMetric.GetType() == metricType)
        //                {
        //                    sinks[sink].Publish(publishMetric);
        //                }
        //            }
        //        }
        //    }

        //    public void Publish(IMetric metric)
        //    {

        //        if (metric == null)
        //            return;

        //        eventQueue.Enqueue(metric);
        //        PublishQueue();
        //    }

        //    public void Publish(IMetric[] metrics)
        //    {

        //        if (metrics == null)
        //            return;

        //        foreach (IMetric imetric in metrics)
        //            eventQueue.Enqueue(imetric);

        //        PublishQueue();
        //    }

        //    void PublishQueue()
        //    {
        //        do
        //        {
        //            EventPublish((IMetric)eventQueue.Dequeue());
        //        } while (eventQueue.Count != 0);
        //    }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing) {
                taskGroups.Clear();
                timer.Dispose();
            }
        }

        //public void Publish(TaskGroupConfiguration taskGroup)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Publish(TaskGroupConfiguration[] taskGroups)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

