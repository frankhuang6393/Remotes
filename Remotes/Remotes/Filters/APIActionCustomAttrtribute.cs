using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Remotes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Remotes.Filters
{
    public class APIActionCustomFilter : IActionFilter
    {
        private readonly ILogService _logService;

        public APIActionCustomFilter(ILogService logService)
        {
            _logService = logService;
        }

        public void OnActionExecuting(ActionExecutingContext actionExecutingContext)
        {
            var context = actionExecutingContext.HttpContext;
            var jsonData = string.Empty;

            switch (context.Request.Method.ToUpper())
            {
                case "GET":
                case "DELETE":
                    var dictQuery = context.Request.Query.ToDictionary(k => k.Key, v => v.Value);
                    jsonData = JsonSerializer.Serialize(dictQuery);
                    break;

                default:
                    if (actionExecutingContext.ActionArguments.Count() > 0)
                    {
                        var arg = actionExecutingContext.ActionArguments[actionExecutingContext.ActionArguments.Keys.FirstOrDefault()];
                        jsonData = JsonSerializer.Serialize(arg);
                    }
                    break;
            }

            WriteLog(true, context.TraceIdentifier, context.Request.Path, context.Request.Method, jsonData);
        }
        public void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            var context = actionExecutedContext.HttpContext;
            dynamic respDatas = new ViewModel.APIResponseBaseViewModel<object>();
            if (actionExecutedContext.Exception == null)
            {
                respDatas = actionExecutedContext.Result.GetType().GetProperty("Value").GetValue(actionExecutedContext.Result);
            }
            else
            {
                respDatas.Success = false;
                respDatas.APIReturnCode = ViewModel.APIReturnCode.Exception;
                respDatas.Message = actionExecutedContext.Exception.Message;

                var resultObj = new Microsoft.AspNetCore.Mvc.OkObjectResult(respDatas) { DeclaredType = typeof(ViewModel.APIResponseBaseViewModel<object>) };
                actionExecutedContext.Result = resultObj;
                actionExecutedContext.Exception = null;  //不拿掉會造成回傳500
            }

            var jsonData = JsonSerializer.Serialize(respDatas);
            WriteLog(false, context.TraceIdentifier, context.Request.Path, context.Request.Method, jsonData);
        }

        private void WriteLog(bool isRequest, string traceId, string transName, string method, string jsonData)
        {
            if (isRequest)
            {
                _logService.CreateAPIReqLog(new Models.APILogModel()
                {
                    TraceID = traceId,
                    TransName = transName,
                    Method = method,
                    RequestData = jsonData,
                    RequestTime = DateTime.Now,
                });
            }
            else
            {
                _logService.CreateAPIRespLog(new Models.APILogModel()
                {
                    TraceID = traceId,
                    ResponseData = jsonData,
                    RequestTime = DateTime.Now,
                    ResponseTime = DateTime.Now,
                });
            }

            var title = $"[{(isRequest ? "Request" : "Response")}] [{method}] {transName}";
            Tools.WriteAPILogFile($"{title} TraceID:{traceId} Data:{jsonData}", LogType.Debug);
        }
    }
}
