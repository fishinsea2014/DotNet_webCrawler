using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Framework.Log
{
    public class Logger
    {
        //public void Log()
        //{
        //    XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CfgFiles\\log4net.cfg.xml")));
        //    ILog log = LogManager.GetLogger(typeof(Logger));
        //    log.Warn("");
        //}
        static Logger()
        {
            var p = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CfgFiles\\log4net.cfg.xml");
            var x =XmlConfigurator.Configure(new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CfgFiles\\log4net.cfg.xml")));
            ILog Log = LogManager.GetLogger(typeof(Logger));
            Log.Info("Initiate Logger");
        }

        private ILog loger = null;
        public Logger(Type type)
        {
            loger = LogManager.GetLogger(type);
        }

        /// <summary>
        /// Log
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public void Error(string msg = "Throw Exception", Exception ex = null)
        {
            Console.WriteLine(msg);
            loger.Error(msg, ex);
        }

        /// <summary>
        /// Log
        /// </summary>
        /// <param name="msg"></param>
        public void Warn(string msg)
        {
            Console.WriteLine(msg);
            loger.Warn(msg);
        }

        /// <summary>
        /// Log
        /// </summary>
        /// <param name="msg"></param>
        public void Info(string msg)
        {
            Console.WriteLine(msg);
            loger.Info(msg);
        }

        /// <summary>
        /// Log
        /// </summary>
        /// <param name="msg"></param>
        public void Debug(string msg)
        {
            Console.WriteLine(msg);
            loger.Debug(msg);
        }

    }
}
