using Remotes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Remotes.ViewModel
{
    public class PlaceOrderViewModel
    {
        /// <summary>
        /// 遊戲商傳入的OrderID
        /// </summary>
        public Guid OrderID { get; set; }
        /// <summary>
        /// 遊戲商ID
        /// </summary>
        public long GameProviderID { get; set; }
        /// <summary>
        /// 會員名稱
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 金額
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 子單號ID
        /// </summary>
        public long? RefNo { get; set; }
    }

    public class PlaceOrderResponseViewModel : APICommonResponseViewModel
    {
        public string OrderID { get; set; }
    }

    public class OrderResultViewModel
    {
        /// <summary>
        /// 下注ID
        /// </summary>
        public string OrderID { get; set; }
        /// <summary>
        /// 遊戲商ID
        /// </summary>
        public long GameProviderID { get; set; }
        /// <summary>
        /// 會員名稱
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 金額
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 子單號ID
        /// </summary>
        public long? RefNo { get; set; }
        /// <summary>
        /// 下注狀態 1:Won, 2:Lose, 3:Draw
        /// </summary>
        public OrderState State { get; set; }
    }
}
