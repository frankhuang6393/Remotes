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
    /// 遊戲商
    /// </summary>
    public class GameProviderModel
    {
        [Key]
        public long ID { get; set; }
        /// <summary>
        /// 遊戲商名稱
        /// </summary>
        public string Name { get; set; }
    }
}
