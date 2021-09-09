using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [Description("This user name is not exist")]
        UserName,
        /// <summary>
        /// 下注單單號是否存在
        /// </summary>
        [Description("This order ID is not exist")]
        OrderID,
    }
}
