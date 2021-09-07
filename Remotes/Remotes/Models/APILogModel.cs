using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remotes.Models
{
    public class APILogModel
    {
        [NotMapped]
        public const string TableName = "APILog";
        [Key]
        public long ID { get; set; }
        /// <summary>
        /// 追蹤ID(相同的ID為Request與Response一組)
        /// </summary>
        public string TraceID { get; set; }
        /// <summary>
        /// 交易API名稱
        /// </summary>
        public string TransName { get; set; }
        /// <summary>
        /// Http Method
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// Request 資料
        /// </summary>
        public string RequestData { get; set; }
        /// <summary>
        /// Response 資料
        /// </summary>
        public string ResponseData { get; set; }
        /// <summary>
        /// Request 時間
        /// </summary>
        public DateTime RequestTime { get; set; }
        /// <summary>
        /// Response 時間
        /// </summary>
        public DateTime? ResponseTime { get; set; }
    }
}
