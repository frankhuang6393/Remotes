using Remotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Remotes.ViewModel
{
    public class APILogViewModel : UserViewModel
    {
        public List<APILogData> APILogDatas { get; set; }
    }

    public class APILogData
    {
        public string APIName { get; set; }
        /// <summary>
        /// 會員名稱
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 金額
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 結餘
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 遊戲提供商ID
        /// </summary>
        public long GameProviderID { get; set; }
        /// <summary>
        /// 訂單ID
        /// </summary>
        public string OrderID { get; set; }

        /// <summary>
        /// 格式 yyyy/MM/dd HH:mm:ss.ttt
        /// </summary>
        public string CreateTime { get; set; }
    } 
}
