using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;

namespace DMTec.TestUIFramework.Logger
{
    /// <summary>
    /// 
    /// </summary>
    public class GlobalLogger
    {
        private static GlobalLogger _instance = new GlobalLogger();
        private static ILogger _logger = LogManager.GetLogger("GlobalLogger", typeof(GlobalLogger));

        private GlobalLogger() { }

        public GlobalLogger Logger { get { return _instance; } }

        private void InitLogger()
        {
            //
        }

        public void Trace(string msg)
        {
            _logger.Trace(msg);
        }

        public void Info(string msg)
        {
            _logger.Info(msg);
        }

        public void Debug(string msg)
        {
            _logger.Debug(msg);
        }

        public void Warn(string msg)
        {
            _logger.Warn(msg);
        }

        public void Error(string msg)
        {
            _logger.Error(msg);
        }

        public void Fatal(string msg)
        {
            _logger.Fatal(msg);
        }



    }
}
