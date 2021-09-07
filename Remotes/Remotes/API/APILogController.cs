using Remotes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Remotes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Remotes.Filters;
using Remotes.Services;

namespace Remotes.API
{
    [Route("api/[controller]")]
    [ApiController, TypeFilter(typeof(APILogActionFilter))]
    public class APILogController : ControllerBase
    {
        private readonly ILogService _logService;
        private readonly IUserService _userService;
        public APILogController(ILogService logService, IUserService userService)
        {
            _logService = logService;
            _userService = userService;
        }

        [HttpGet]
        public APIResponseBaseViewModel<APILogViewModel> GET(string userName = "")
        {
            var resp = new APIResponseBaseViewModel<APILogViewModel>();
            try
            {
                if (string.IsNullOrWhiteSpace(userName))
                {
                    resp.APIReturnCode = eAPIReturnCode.UserIsNotExist;
                    resp.Message = "This UserName is not exist";
                }

                var user = _userService.GetUser(userName);
                if (user == null)
                {
                    resp.APIReturnCode = eAPIReturnCode.UserIsNotExist;
                    resp.Message = "This UserName is not exist";
                }
                else
                {
                    var allLogs = _logService.GetAllAPILog();
                    //過濾使用者以及API名稱
                    var relTransName = new string[] 
                    { 
                        Url.RouteUrl("api_route", new { Controller = "User" }), 
                        Url.RouteUrl("api_route", new { Controller = "Order" }), 
                    };
                    allLogs = allLogs.Where(r => relTransName.Contains(r.TransName)).OrderByDescending(r => r.RequestTime);

                    var aPILogDatas = new List<APILogData>();
                    
                    foreach (var item in allLogs)
                    {
                        //註冊
                        if (item.TransName == Url.RouteUrl("api_route", new { Controller = "User" })
                                && item.Method == "POST")
                        {
                            var reqItem = Newtonsoft.Json.JsonConvert.DeserializeObject<UserViewModel>(item.RequestData);
                            var respItem = Newtonsoft.Json.JsonConvert.DeserializeObject<APIResponseBaseViewModel<APICommonResponseViewModel>>(item.ResponseData);

                            if (respItem.Success)
                            {
                                aPILogDatas.Add(new APILogData()
                                {
                                    APIName = $"[{item.Method}] {item.TransName}",
                                    UserName = reqItem.UserName,
                                    Amount = reqItem.Balance,
                                    Balance = reqItem.Balance,
                                    CreateTime = item.RequestTime.ToString("yyyy/MM/dd HH:mm:ss"),
                                }); 
                            }
                        }
                        //下注相關
                        else if (item.TransName == Url.RouteUrl("api_route", new { Controller = "Order" }))
                        {
                            switch (item.Method)
                            {
                                //下注
                                case "POST":
                                    {
                                        var reqItem = Newtonsoft.Json.JsonConvert.DeserializeObject<PlaceOrderViewModel>(item.RequestData);
                                        var respItem = Newtonsoft.Json.JsonConvert.DeserializeObject<APIResponseBaseViewModel<PlaceOrderResponseViewModel>>(item.ResponseData);

                                        if (respItem.Success)
                                        {
                                            aPILogDatas.Add(new APILogData()
                                            {
                                                APIName = $"[{item.Method}] {item.TransName}",
                                                UserName = respItem.Content.UserName,
                                                Amount = reqItem.Amount,
                                                Balance = respItem.Content.Balance,
                                                CreateTime = item.RequestTime.ToString("yyyy/MM/dd HH:mm:ss"),
                                                GameProviderID = reqItem.GameProviderID,
                                                OrderID = reqItem.OrderID.ToString("N"),
                                            }); 
                                        }
                                    }
                                    break;

                                //下注結果
                                case "PATCH":
                                    {
                                        var reqItem = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderResultViewModel>(item.RequestData);
                                        var respItem = Newtonsoft.Json.JsonConvert.DeserializeObject<APIResponseBaseViewModel<APICommonResponseViewModel>>(item.ResponseData);

                                        if (respItem.Success)
                                        {
                                            aPILogDatas.Add(new APILogData()
                                            {
                                                APIName = $"[{item.Method}] {item.TransName}",
                                                UserName = respItem.Content.UserName,
                                                Amount = reqItem.Amount,
                                                Balance = respItem.Content.Balance,
                                                CreateTime = item.RequestTime.ToString("yyyy/MM/dd HH:mm:ss"),
                                                GameProviderID = reqItem.GameProviderID,
                                                OrderID = reqItem.OrderID,
                                            }); 
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    resp.Content = new APILogViewModel()
                    {
                        Balance = user.Balance,
                        UserName = user.UserName,
                        Currency = user.Currency,
                        APILogDatas = aPILogDatas,
                    };
                    resp.Success = true;
                    resp.APIReturnCode = eAPIReturnCode.Success;
                }
            }
            catch (Exception ex)
            {
                resp.Success = false;
                resp.APIReturnCode = eAPIReturnCode.Exception;
                resp.Message = ex.Message;
            }

            return resp;
        }
    }
}
