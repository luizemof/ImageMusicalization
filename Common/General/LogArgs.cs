using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.General
{
    /// <summary>
    /// Argumentos para o evento de Log
    /// </summary>
    public class LogArgs : EventArgs
    {
        public LogArgs(string message, ELogType logType, bool sameLine = false)
        {
            SameLine = sameLine;
            Message = message;
            LogType = logType;
        }

        public string Message { get; set; }
        public ELogType LogType { get; set; }
        public bool SameLine { get; set; }
    }
}
