using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Remotes
{
    public enum LogType
    {
        Debug,
        Info,
        Warn,
        Error,
        Fatal,
    }

    public enum APICheckParamterType
    {
        /// <summary>
        /// 會員名稱是否存在
        /// </summary>
        UserName,
        /// <summary>
        /// 下注單單號是否存在
        /// </summary>
        OrderID,
    }
}
