using System;
using System.Diagnostics;
using System.Threading;

using Automata.Configuration;
using Automata.Management;

namespace AutomationConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Write($"HOST: Starting Threads({Process.GetCurrentProcess().Threads.Count})\r\n");

            AutomataConfiguration configuration = (AutomataConfiguration)System.Configuration.ConfigurationManager.GetSection("automata");

            Manager manager = new Manager(configuration);

            manager.Governer();




            int i = 0;
            while (manager.IsActive)
            {
                i++;
                if (i==30)
                {
                    manager.RequestPause();
                    Console.Write("HOST: Sent a request to pause...\r\n");
                }
                if (i==60)
                {
                    manager.UnPause();
                    Console.Write("HOST: Sent a request to unpause...\r\n");
                }
                if (i==90)
                {
                    manager.RequestStop();
                    Console.Write("HOST: Sent a request to stop...\r\n");

                }

                Console.Write($"HOST: {i}\t{manager.Status}\tThreads({Process.GetCurrentProcess().Threads.Count})\r\n");

                Thread.Sleep((int)configuration.PollingInSeconds * 1000);
            }

            Console.Write("done.");


            Console.Read();
        }
    }
}
