using log4net;
using log4net.Config;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Remotes
{
    public class Tools
    {
        private static ILog log = LogManager.GetLogger(typeof(Tools));
        public static string GetNewSN()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static void WriteAPILogFile(string message, eLogType type)
        {
            //讀取設定檔
            XmlConfigurator.Configure(new FileInfo("D:\\log\\log4netconfig.xml"));
            switch (type)
            {   
                case eLogType.Debug: log.Debug(message); break;
                case eLogType.Info: log.Info(message); break;
                case eLogType.Warn: log.Warn(message); break;
                case eLogType.Error: log.Error(message); break;
                case eLogType.Fatal: log.Fatal(message); break;
                default: break;
            }
        }
    }

    public enum eLogType
    {
        Debug,
        Info,
        Warn,
        Error,
        Fatal,
    }
}
