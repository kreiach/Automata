using System;
using System.Collections.Generic;
using System.Text;

namespace Automata.LogSinks
{

    public enum LogEntryType
    {
        Error = 1,
        Warning = 2,
        Information = 4,
        SuccessAudit = 8,
        FailureAudit = 16
    }

}
