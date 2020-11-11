using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Xml;

namespace Automata.Configuration
{
    public class ConfigurationHandler : IConfigurationSectionHandler
    { 

        public object Create(object parent, object configContext, XmlNode section)
        {
            if (section == null)
            {
                throw (new ArgumentNullException(nameof(section), "Parameter cannot be null."));
            }

            return ReadConfiguration(section);
        }

        public AutomataConfiguration ReadConfiguration(XmlNode section)
        {
            if (section == null)
            {
                throw (new ArgumentNullException(nameof(section), "Parameter cannot be null"));
            }

            var automataConfiguration = new AutomataConfiguration();

            var param = section.SelectSingleNode("param");
            if (param is null)
            {
                automataConfiguration.PollingInSeconds = 60;
            } 
            else
            {
                if (int.TryParse(param.Attributes?["value"]?.Value, out int ps))
                {
                    automataConfiguration.PollingInSeconds = ps;
                } 
                else
                {
                    throw (new ArgumentNullException("PollingInSeconds", "PollingInSeconds is missing in the configuration."));
                }
            }

            /* process the logSinks */
            var logSinkConfigurations = new List<LogSinkConfiguration>();

            foreach (XmlNode logSink in section.SelectNodes("logsinks/logsink"))
            {

                // try to resolve the logSink type
                var logSinkType = Type.GetType(logSink.Attributes?["type"]?.Value);

                if (logSinkType != null)
                {
                    string logSinkName = logSink.Attributes?["name"]?.Value;

                    var parameters = new Dictionary<string, string>();
                    foreach (XmlNode logSinkParameter in logSink.SelectNodes("param"))
                    {
                        parameters.Add(logSinkParameter.Attributes?["name"]?.Value, logSinkParameter.Attributes?["value"]?.Value);
                    }

                    var logSinkConfiguration = new LogSinkConfiguration
                    {
                        Name = logSinkName,
                        LogSink = logSinkType,
                        Parameters = parameters
                    };

                    logSinkConfigurations.Add(logSinkConfiguration);
                }

            }

            if (logSinkConfigurations.Any())
            {
                automataConfiguration.LogSinks = logSinkConfigurations.ToArray();
            }

            // process the automation groups
            var taskGroups = new List<TaskGroupConfiguration>();
            foreach (XmlNode tasksGroups in section.SelectNodes("taskgroups/taskgroup"))
            {
                string groupLogSinkName = tasksGroups.Attributes?["logsink"]?.Value;
                bool taskGroupActive = tasksGroups.Attributes?["active"]?.Value == "true";

                if (taskGroupActive)
                {

                    LogSinkConfiguration logSinkConfig = logSinkConfigurations.Where(c => c.Name == groupLogSinkName).FirstOrDefault();
                    if (logSinkConfig is null)
                    {
                        throw new ArgumentException("logsink", "Parameter is not defined.");
                    }

                    Type groupLogSink = logSinkConfig.LogSink;

                    Type taskType;
                    string taskName;

                    var tasks = new List<Tuple<string, Type, Dictionary<string, string>>>();
                    foreach (XmlNode task in tasksGroups.SelectNodes("task"))
                    {
                        bool taskActive = task.Attributes?["active"]?.Value == "true";
                        
                        if (taskActive)
                        {
                            taskType = Type.GetType(task.Attributes?["type"]?.Value);
                            if (taskType is null)
                            {
                                throw new ArgumentException("type", "Parameter is missing");
                            }
                            
                            taskName = task.Attributes?["name"]?.Value;

                            var parameters = new Dictionary<string, string>();
                            foreach (XmlNode taskParameter in task.SelectNodes("param"))
                            {
                                parameters.Add(taskParameter.Attributes?["name"]?.Value, taskParameter.Attributes?["value"]?.Value);
                            }

                            tasks.Add(Tuple.Create(taskName, taskType, parameters));

                        }
                    }

                    if (tasks.Any())
                    {
                        var tasksGroup = new TaskGroupConfiguration
                        {
                            LogSink = groupLogSink,
                            Tasks = tasks.ToArray()
                        };
                        taskGroups.Add(tasksGroup);
                    }
                    
                }
            }

            if (taskGroups.Any())
            {
                automataConfiguration.TaskGroups = taskGroups.ToArray();
            }

            return automataConfiguration;
        }

    }

}
