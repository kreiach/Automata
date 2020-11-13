using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading;

namespace Automata.Management
{

    public partial class Manager
    {

        private bool shutdownRequested = false;
        public void RequestStop()
        {
            shutdownRequested = true;
        }

        private bool pauseRequested = false;
        public void RequestPause()
        {
            pauseRequested = true;
        }

        public void UnPause()
        {
            pauseRequested = false;
        }

        private GovernerStatus _governerStatus;
        public string Status
        {
            get
            {
                return Enum.GetName(typeof(GovernerStatus), _governerStatus);
            }
        } 

        private enum GovernerStatus
        {
            Running,
            Stopped,
            Paused
        }

        public bool IsActive
        {
            get
            {
                return _governerStatus != GovernerStatus.Stopped;
            }
        }

        List<Worker> Workers = new List<Worker>();

        public void Governer() 
        {

            // start up the governer thread
            var governerThread = new Thread(() =>
            {

                _governerStatus = GovernerStatus.Running;

                while (!shutdownRequested || (shutdownRequested && Workers.Count > 0))
                {

                    Console.Write($"\tGOVERNER: ShutdownRequested: {shutdownRequested}\tWorkers: {Workers.Count}\r\n");

                    if (pauseRequested)
                    {
                        _governerStatus = GovernerStatus.Paused;
                    }
                    else
                    {
                        _governerStatus = GovernerStatus.Running;

                        foreach (var group in taskGroups)
                        {

                            if (!Workers.Where(x => x.Name == group.Name).Any())
                            {

                                if (!shutdownRequested)
                                {

                                    if (group.ProcessReady())
                                    {
                                        var worker = new Worker(group);
                                        Workers.Add(worker);
                                        Thread thread = new Thread(new ThreadStart(worker.ProcessTaskGroup));
                                        thread.Start();
                                    }

                                }
                            
                            } 
                            else
                            {

                                var worker = Workers.Where(x => x.Name == group.Name).FirstOrDefault();
                                if (!worker.Active)
                                {
                                    Workers.Remove(worker);
                                }

                            }

                            

                        }
                    }

                    Thread.Sleep((int)configuration.PollingInSeconds * 1000);
                }

                _governerStatus = GovernerStatus.Stopped;

            });

            governerThread.Start();
 
        }

        private class Worker
        {

            public string Name 
            {
                get 
                {
                    return Group.Name;
                }
            }

            public Group.TaskGroup Group;
            public bool Active = true;

            public Worker(Group.TaskGroup group) 
            {
                Group = group;
            }

            public void ProcessTaskGroup()
            {
                foreach (var task in Group.AutomationTasks)
                {
                    if (task.ProcessReady())
                    {

                        task.Process();

                    }
                }

                Active = false;

                Thread.CurrentThread.Interrupt();
            }
        }

    }

}
