using System.ComponentModel.Design;
using System.Diagnostics.Eventing.Reader;
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

        public GovernerStatus Status
        {
            get;
            private set;
        }

        public enum GovernerStatus
        {
            Stopped,
            Running,
            Paused
        }

        public void Governer() 
        {

            // start up the governer thread
            var governerThread = new Thread(() =>
            {

                Status = GovernerStatus.Running;

                int activeThreads = 1;

                // TODO: Response to stop event from host application
                while (!shutdownRequested && activeThreads > 0)
                {

                    if (pauseRequested)
                    {
                        Status = GovernerStatus.Paused;
                    }
                    else
                    {
                        Status = GovernerStatus.Running;

                        foreach (var group in taskGroups)
                        {
                            // TODO: start up a new thread per task group

                            foreach (var task in group.AutomationTasks)
                            {
                                task.Process();
                            }

                        }
                    }

                    Thread.Sleep((int)configuration.PollingInSeconds * 1000);
                }

                Status = GovernerStatus.Stopped;

            });

            governerThread.Start();
 
        }

    }

}
