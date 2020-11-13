using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System;

using Automata.Configuration;

namespace Automata.Tests
{

    [TestClass]
    public class ConfigurationHandlerTests
    {

        [TestMethod]
        public void With_AutomataConfigurationHander_throws_exception_when_section_is_null()
        {

            var automataConfigurationHandler = new ConfigurationHandler();

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                object x = automataConfigurationHandler.Create(null, null, null);
            });
           
        }

        [TestMethod]
        public void With_AutomataConfigurationHander_contains_one_logsink_configuration()
        {

            string section = @"<configuration><automata><logsinks><logsink name=""Null"" type=""LogSinks.Null, LogSinks"" /></logsinks></automata></configuration>";

            var xdoc = new XmlDocument();
            xdoc.LoadXml(section);
            var node = xdoc.DocumentElement.SelectSingleNode("automata");

            var automataConfigurationHandler = new ConfigurationHandler();

            var x = (Configuration.AutomataConfiguration)automataConfigurationHandler.Create(null, null, node);

            Assert.AreEqual(x.LogSinks.Length, 1);

        }

        [TestMethod]
        public void With_AutomataConfigurationHander_contains_one_logsink_configuration_with_name()
        {

            string section = @"<configuration><automata><logsinks><logsink name=""Null"" type=""LogSinks.Null, LogSinks"" /></logsinks></automata></configuration>";

            var xdoc = new XmlDocument();
            xdoc.LoadXml(section);
            var node = xdoc.DocumentElement.SelectSingleNode("automata");

            var automataConfigurationHandler = new ConfigurationHandler();

            var x = (Configuration.AutomataConfiguration)automataConfigurationHandler.Create(null, null, node);

            Assert.AreEqual(x.LogSinks[0].Name, "Null");

        }

        [TestMethod]
        public void With_AutomataConfigurationHander_contains_one_logsink_configuration_with_type()
        {

            string section = @"<configuration><automata><logsinks><logsink name=""Null"" type=""LogSinks.Null, LogSinks"" /></logsinks></automata></configuration>";

            var xdoc = new XmlDocument();
            xdoc.LoadXml(section);
            var node = xdoc.DocumentElement.SelectSingleNode("automata");

            var automataConfigurationHandler = new ConfigurationHandler();

            var x = (Configuration.AutomataConfiguration)automataConfigurationHandler.Create(null, null, node);

            Assert.AreEqual(x.LogSinks[0].LogSink, Type.GetType("LogSinks.Null, LogSinks"));

        }

        [TestMethod]
        public void With_AutomataConfigurationHander_contains_one_logsink_configuration_with_additional_parameters()
        {

            string section = @"<configuration><automata><logsinks><logsink name=""Event"" type=""LogSinks.SystemEventLog, LogSinks""><param name=""Source"" values=""eventsource"" /></logsink></logsinks></automata></configuration>";

            var xdoc = new XmlDocument();
            xdoc.LoadXml(section);
            var node = xdoc.DocumentElement.SelectSingleNode("automata");

            var automataConfigurationHandler = new ConfigurationHandler();

            var x = (Configuration.AutomataConfiguration)automataConfigurationHandler.Create(null, null, node);

            Assert.AreEqual(x.LogSinks[0].Parameters.Count, 1);

        }

        [TestMethod]
        public void With_AutomataConfigurationHander_contains_one_taskgroup()
        {

            string section = @"<configuration><automata><taskgroups><taskgroup name=""Accounting"" logsink=""Null"" active=""true""><task name=""null1"" type=""Automations.NullTask, Automations"" active=""true""/></taskgroup></taskgroups><logsinks><logsink name=""Null"" type=""LogSinks.Null, LogSinks"" /></logsinks></automata></configuration>";

            var xdoc = new XmlDocument();
            xdoc.LoadXml(section);
            var node = xdoc.DocumentElement.SelectSingleNode("automata");

            var automataConfigurationHandler = new ConfigurationHandler();

            var x = (Configuration.AutomataConfiguration)automataConfigurationHandler.Create(null, null, node);

            Assert.AreEqual(x.TaskGroups.Length, 1);

        }

        [TestMethod]
        public void With_AutomataConfigurationHander_taskgroup_referencing_an_invalid_logsink_creates_exception()
        {

            string section = @"<configuration><automata><taskgroups><taskgroup name=""Accounting"" logsink=""Null"" active=""true""><task name=""null1"" type=""Automata.AutomationTasks.NullTask, Automata"" active=""true""/></taskgroup></taskgroups><logsinks><logsink name=""NotNull"" type=""LogSinks.Null, LogSinks"" /></logsinks></automata></configuration>";

            var xdoc = new XmlDocument();
            xdoc.LoadXml(section);
            var node = xdoc.DocumentElement.SelectSingleNode("automata");

            var automataConfigurationHandler = new ConfigurationHandler();

            Assert.ThrowsException<ArgumentException>(() =>
            {
                var x = (Configuration.AutomataConfiguration)automataConfigurationHandler.Create(null, null, node);
            });

        }

        [TestMethod]
        public void With_AutomataConfigurationHander_taskgroup_does_not_have_a_unique_name()
        {

            string section = @"<configuration><automata><taskgroups><taskgroup name=""Accounting"" logsink=""Null"" active=""true""><task name=""null1"" type=""Automations.NullTask, Automations"" active=""true""/></taskgroup><taskgroup name=""Accounting"" logsink=""Null"" active=""true""><task name=""null1"" type=""Automations.NullTask, Automations"" active=""true""/></taskgroup></taskgroups><logsinks><logsink name=""Null"" type=""LogSinks.Null, LogSinks"" /></logsinks></automata></configuration>";

            var xdoc = new XmlDocument();
            xdoc.LoadXml(section);
            var node = xdoc.DocumentElement.SelectSingleNode("automata");

            var automataConfigurationHandler = new ConfigurationHandler();

            Assert.ThrowsException<ArgumentException>(() =>
            {
                var x = (Configuration.AutomataConfiguration)automataConfigurationHandler.Create(null, null, node);
            });

        }

        [TestMethod]
        public void With_AutomataConfigurationHander_taskgroup_has_a_task()
        {

            string section = @"<configuration><automata><taskgroups><taskgroup name=""Accounting"" logsink=""Null"" active=""true""><task name=""null1"" type=""Automations.NullTask, Automations"" active=""true""/></taskgroup></taskgroups><logsinks><logsink name=""Null"" type=""LogSinks.Null, LogSinks"" /></logsinks></automata></configuration>";

            var xdoc = new XmlDocument();
            xdoc.LoadXml(section);
            var node = xdoc.DocumentElement.SelectSingleNode("automata");

            var automataConfigurationHandler = new ConfigurationHandler();

            var x = (Configuration.AutomataConfiguration)automataConfigurationHandler.Create(null, null, node);

            Assert.AreEqual(x.TaskGroups[0].Tasks.Length, 1);

        }

        [TestMethod]
        public void With_AutomataConfigurationHander_throw_exception_is_task_does_not_have_a_type()
        {

            string section = @"<configuration><automata><taskgroups><taskgroup name=""Accounting"" logsink=""Null"" active=""true""><task name=""null1"" type=""Automata.AutomationTasks.MisspelledAutomationTask, Automata"" active=""true""/></taskgroup></taskgroups><logsinks><logsink name=""Null"" type=""LogSinks.Null, LogSinks"" /></logsinks></automata></configuration>";

            var xdoc = new XmlDocument();
            xdoc.LoadXml(section);
            var node = xdoc.DocumentElement.SelectSingleNode("automata");

            var automataConfigurationHandler = new ConfigurationHandler();

            Assert.ThrowsException<ArgumentException>(() =>
            {
                var x = (Configuration.AutomataConfiguration)automataConfigurationHandler.Create(null, null, node);
            });

        }

    }

}
