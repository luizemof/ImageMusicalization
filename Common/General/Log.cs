using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.General
{
    /// <summary>
    /// Classe de log do sistema
    /// </summary>
    public class Log
    {
        public Log(string fileName)
            : this(Environment.CurrentDirectory, fileName, ELogType.Unkown)
        { }

        public Log(string directory, string fileName, ELogType logType)
        {
            _FileName = fileName;
            _Directory = directory;
            _FullName = string.Concat(_Directory, @"\", _FileName);
            _LogType = logType;

            if (!System.IO.Directory.Exists(_Directory))
                System.IO.Directory.CreateDirectory(_Directory);

            if (File.Exists(_FullName))
                File.Delete(_FullName);
        }

        public event EventHandler<LogArgs> Notify;

        private string _FileName;
        private string _Directory;
        private string _FullName;
        private ELogType _LogType;

        public string FileName { get { return _FileName; } }
        public string Directory { get { return _Directory; } }
        public string FullName { get { return _FullName; } }
        public ELogType LogType { get { return _LogType; } }

        public void WriteLog(string logMessage, int level = 0, bool sameLine = false)
        {
            for (int i = 0; i < level; i++)
                logMessage = string.Concat("\t", logMessage);

            string message = string.Format("{0}: {1}", DateTime.Now.ToLongTimeString(), logMessage);

            using (FileStream fs = new FileStream(_FullName, FileMode.Append, FileAccess.Write))
            using (StreamWriter w = new StreamWriter(fs))
            {
                if (sameLine)
                {
                    w.Write(message + " ");
                    System.Console.Write(message + " ");
                }
                else
                {
                    w.WriteLine(message);
                    System.Console.WriteLine(message);
                }

                OnNotify(message, sameLine);
                w.Close();
            }
        }

        private void OnNotify(string message, bool sameLine = false)
        {
            if(this.Notify != null)
                this.Notify.Invoke(this, new LogArgs(message, _LogType, sameLine));
        }

    }
}
