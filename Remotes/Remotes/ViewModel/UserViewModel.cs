using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Remotes.ViewModel
{
    public class UserViewModel
    {
        /// <summary>
        /// 結餘
        /// </summary>
        public decimal Balance { get; set; }

        /// <summary>
        /// 會員名稱
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 幣別
        /// </summary>
        public string Currency { get; set; }
    }
}
