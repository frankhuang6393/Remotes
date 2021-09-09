using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remotes.Models
{
    /// <summary>
    /// 會員
    /// </summary>
    public class UserModel
    {
        [Key]
        public long ID { get; set; }
        /// <summary>
        /// 會員名稱
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 幣別
        /// </summary>
        public string Currency { get; set; }
        /// <summary>
        /// 結餘
        /// </summary>
        public decimal Balance { get; set; }
    }
}
