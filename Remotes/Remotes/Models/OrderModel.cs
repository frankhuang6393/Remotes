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
    /// 下注訂單
    /// </summary>
    public class OrderModel
    {
        [NotMapped]
        public const string TableName = "Order";

        [Key]
        public long ID { get; set; }
        /// <summary>
        /// 遊戲商傳入ID
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 遊戲商ID
        /// </summary>
        public long GameProviderID { get; set; }
        /// <summary>
        /// 會員ID
        /// </summary>
        public long UserID { get; set; }
        /// <summary>
        /// 金額
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 子單號ID
        /// </summary>
        public long? RefNo { get; set; }
        /// <summary>
        /// 下注狀態
        /// </summary>
        public eOrderState State { get; set; }
        /// <summary>
        /// 建立時間
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改時間
        /// </summary>
        public DateTime? ModifiedTime { get; set; }
    }

    public enum eOrderState
    {
        /// <summary>
        /// 已下注
        /// </summary>
        Bet = 0,
        /// <summary>
        /// 勝利
        /// </summary>
        Won,
        /// <summary>
        /// 失敗
        /// </summary>
        Lose,
        /// <summary>
        /// 平局
        /// </summary>
        Draw,
    }
}
