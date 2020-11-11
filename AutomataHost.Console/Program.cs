using Automata.Configuration;
using Automata.Management;

namespace AutomationConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            AutomataConfiguration configuration = (AutomataConfiguration)System.Configuration.ConfigurationManager.GetSection("automata");

            Manager manager = new Manager(configuration);

            manager.Governer();
        }
    }
}
