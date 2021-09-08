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

        public static void WriteAPILogFile(string message, LogType type)
        {
            //讀取設定檔
            XmlConfigurator.Configure(new FileInfo("D:\\log\\log4netconfig.xml"));
            switch (type)
            {   
                case LogType.Debug: log.Debug(message); break;
                case LogType.Info: log.Info(message); break;
                case LogType.Warn: log.Warn(message); break;
                case LogType.Error: log.Error(message); break;
                case LogType.Fatal: log.Fatal(message); break;
                default: break;
            }
        }

        public static bool CheckAPIParamterWithMessage<T>(APICheckParamterType type, ViewModel.APIResponseBaseViewModel<T> resp, object parm)
        {
            bool result = true;
            if (parm == null)
            {
                switch (type)
                {
                    case APICheckParamterType.UserName:
                        resp.Message = "This user name is not exist";
                        resp.APIReturnCode = ViewModel.APIReturnCode.UserIsNotExist;
                        break;
                    case APICheckParamterType.OrderID:
                        resp.Message = "This order ID is not exist";
                        resp.APIReturnCode = ViewModel.APIReturnCode.OrderIsNotExist;
                        break;
                    default:
                        break;
                }
                result = false;
            }

            return result;
        }
    }
}
