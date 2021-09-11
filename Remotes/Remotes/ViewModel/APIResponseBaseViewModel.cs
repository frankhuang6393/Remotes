using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Remotes.ViewModel
{
    public class APIResponseBaseViewModel<T>
    {
        public APIResponseBaseViewModel()
        {
            Success = true;
            APIReturnCode = APIReturnCode.Success;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public T Content { get; set; }

        public APIReturnCode APIReturnCode { get; set; }
    }

    public class APICommonResponseViewModel
    {
        /// <summary>
        /// 結餘
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// 會員名稱
        /// </summary>
        public string UserName { get; set; }
    }

    public enum APIReturnCode
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 發生例外
        /// </summary>
        Exception = 1,

        /// <summary>
        /// Token Secret 錯誤
        /// </summary>
        TokenSecretError = 100,

        /// <summary>
        /// 找不到此玩家
        /// </summary>
        UserIsNotExist = 200,

        /// <summary>
        /// 玩家餘額不足
        /// </summary>
        UserInsufficientBalance = 201,

        /// <summary>
        /// 找不到此下注單
        /// </summary>
        OrderIsNotExist = 300,
    }
}
