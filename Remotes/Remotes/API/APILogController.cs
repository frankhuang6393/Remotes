using Remotes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
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
    [ApiController, TypeFilter(typeof(APIActionCustomFilter))]
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
            var user = _userService.GetUser(userName);
            if (Tools.CheckAPIParamterWithMessage(APICheckParamterType.UserName, resp, user))
            {
                var allLogs = _logService.GetAllAPILog();
                //過濾使用者以及API名稱
                var relTransName = new string[]
                {
                    Url.RouteUrl("api_route", new { Controller = "User" }),
                    Url.RouteUrl("api_route", new { Controller = "Order" }),
                };
                allLogs = allLogs.Where(r => relTransName.Contains(r.TransName)).OrderByDescending(r => r.RequestTime);

                resp.Content = new APILogViewModel()
                {
                    Balance = user.Balance,
                    UserName = user.UserName,
                    Currency = user.Currency,
                    APILogDatas = GetAPIViewLogDatas(allLogs),
                };
            }

            return resp;
        }

        private List<APILogData> GetAPIViewLogDatas(IEnumerable<APILogModel> logs)
        {
            var apiLogDatas = new List<APILogData>();
            foreach (var item in logs)
            {
                dynamic reqItem = null, respItem = null;
                bool success = false;
                string userName = null, orderId = null;
                decimal amount = 0, balance = 0;
                long gameProviderID = 0;

                respItem = JsonSerializer.Deserialize<APIResponseBaseViewModel<object>>(item.ResponseData);
                success = respItem != null ? respItem.Success : false;
                if (success)
                {
                    //註冊
                    if (item.TransName == Url.RouteUrl("api_route", new { Controller = "User" })
                            && item.Method == "POST")
                    {
                        reqItem = JsonSerializer.Deserialize<UserViewModel>(item.RequestData);
                        respItem = JsonSerializer.Deserialize<APIResponseBaseViewModel<APICommonResponseViewModel>>(item.ResponseData);
                        amount = reqItem.Balance;
                    }
                    //下注相關
                    else if (item.TransName == Url.RouteUrl("api_route", new { Controller = "Order" }))
                    {
                        switch (item.Method)
                        {
                            case "POST":
                                reqItem = JsonSerializer.Deserialize<PlaceOrderViewModel>(item.RequestData);
                                respItem = JsonSerializer.Deserialize<APIResponseBaseViewModel<PlaceOrderResponseViewModel>>(item.ResponseData);
                                orderId = reqItem.OrderID.ToString("N");
                                break;
                            case "PATCH":
                                reqItem = JsonSerializer.Deserialize<OrderResultViewModel>(item.RequestData);
                                respItem = JsonSerializer.Deserialize<APIResponseBaseViewModel<APICommonResponseViewModel>>(item.ResponseData);
                                orderId = reqItem.OrderID;
                                break;
                            default: 
                                continue;
                        }

                        amount = reqItem != null ? reqItem.Amount : 0;
                        gameProviderID = reqItem != null ? reqItem.GameProviderID : 0;
                    }
                    else
                    {
                        continue;
                    }
                    userName = respItem != null ? respItem.Content.UserName : null;
                    balance = respItem != null ? respItem.Content.Balance : 0;

                    apiLogDatas.Add(new APILogData()
                    {
                        APIName = $"[{item.Method}] {item.TransName}",
                        UserName = userName,
                        Amount = amount,
                        Balance = balance,
                        CreateTime = item.RequestTime.ToString("yyyy/MM/dd HH:mm:ss"),
                        GameProviderID = gameProviderID,
                        OrderID = orderId,
                    });
                }
            }

            return apiLogDatas;
        }
    }
}
