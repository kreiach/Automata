using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

using Automata.LogSinks;
using LogSinks;

namespace Automata.Tests
{
    [TestClass]
    public class LogSinkTests
    {
        [TestMethod]
        public void With_LogEntry_default_type_is_information()
        {

            LogEntry logEntry = LogEntry.Entry("This is a test");

            Assert.IsTrue(logEntry.EntryType == LogEntryType.Information);
        }

        [TestMethod]
        public void With_LogEntry_Entry_throw_exception_when_message_is_null()
        {

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                LogEntry logEntry = LogEntry.Entry(null);
            });
            
        }

        [TestMethod]
        public void With_DebugLogSink_Write_throw_exception_when_logEntry_is_null()
        {

            var debugLogSink = new DebugWindow();

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                debugLogSink.Write(null);
            });
        }

        [TestMethod]
        public void With_DebugLogSink_Write_does_not_throw_an_exception()
        {

            var debugLogSink = new DebugWindow();

            try
            {
                debugLogSink.Write(LogEntry.Entry("Hello World"));
                Assert.IsTrue(true);
            } catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void With_NullLogSink_Write_throw_exception_when_logEntry_is_null()
        {

            var nullLogSink = new Null();

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                nullLogSink.Write(null);
            });
        }

        [TestMethod]
        public void With_NullLogSink_Write_does_not_throw_an_exception()
        {

            var nullLogSink = new ConsoleWindow();

            try
            {
                nullLogSink.Write(LogEntry.Entry("Hello World"));
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        [TestMethod]
        public void With_ConsoleLogSink_Write_throw_exception_when_logEntry_is_null()
        {

            var consoleLogSink = new ConsoleWindow();

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                consoleLogSink.Write(null);
            });
        }

        [TestMethod]
        public void With_ConsoleLogSink_Write_does_not_throw_an_exception()
        {

            var consoleLogSink = new ConsoleWindow();

            try
            {
                consoleLogSink.Write(LogEntry.Entry("Hello World"));
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        public void With_EventLogSink_throw_exception_when_no_eventlog_is_not_specified()
        {

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                var eventLogSink = new SystemEventLog(null);
            });
        }

        [TestMethod]
        public void With_EventLogSink_throw_exception_when_EventLog_Source_is_not_specified()
        {

            var eventLog = new EventLog();

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                var eventLogSink = new SystemEventLog(eventLog);
            });
        }

        [TestMethod]
        public void With_EventLogSink_throw_exception_when_EventLog_Log_is_not_specified()
        {

            var eventLog = new EventLog();
            eventLog.Source = "test";

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                var eventLogSink = new SystemEventLog(eventLog);
            });
        }

        [TestMethod]
        public void With_EventLogSink_throw_exception_when_EventLog_Source_does_not_exist()
        {

            var eventLog = new EventLog();
            eventLog.Source = "test";
            eventLog.Log = "test";

            Assert.ThrowsException<Exception>(() =>
            {
                var eventLogSink = new SystemEventLog(eventLog);
            });
        }

        [TestMethod]
        public void With_EventLogSink_does_not_throw_exception_when_EventLog_Source_exists()
        {

            // this will probably fail the first time executed on a system, as it takes time to register the new event source
            if (!EventLog.SourceExists("AutomataTestsSource"))
            {
                EventLog.CreateEventSource("AutomataTestsSource", "AutomataTestsLog");
            }

            var eventLog = new EventLog()
            {
                Source = "AutomataTestsSource",
                Log = "AutomataTestsLog"
            };

            try
            {
                var eventLogSink = new SystemEventLog(eventLog);
                Assert.IsTrue(true);
            } catch (Exception ex)
            {
                Assert.Fail(ex.ToString());
            }
        }

        [TestMethod]
        public void With_EventLogSink_Write_throw_exception_when_no_logEntry_is_null()
        {
            var eventLog = new EventLog()
            {
                Source = "AutomataTestsSource",
                Log = "AutomataTestsLog"
            };

            var eventLogSink = new SystemEventLog(eventLog);

            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                eventLogSink.Write(null);
            });
        }

        [TestMethod]
        public void With_EventLogSink_Write_does_not_throw_an_exception()
        {

            var eventLog = new EventLog()
            {
                Source = "AutomataTestsSource",
                Log = "AutomataTestsLog"
            };

            var eventLogSink = new SystemEventLog(eventLog);

            try
            {
                eventLogSink.Write(LogEntry.Entry("Hello World"));
                Assert.IsTrue(true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }


    }
}
