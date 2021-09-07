using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Remotes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Remotes.Filters
{
    public class APILogActionFilter : IActionFilter
    {
        private readonly ILogService _logService;

        public APILogActionFilter(ILogService logService)
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
                    jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(dictQuery);
                    break;

                default:
                    if (actionExecutingContext.ActionArguments.Count() > 0)
                    {
                        var arg = actionExecutingContext.ActionArguments[actionExecutingContext.ActionArguments.Keys.FirstOrDefault()];
                        jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(arg);
                    }
                    break;
            }

            WriteLog(true, context.TraceIdentifier, context.Request.Path, context.Request.Method, jsonData);
        }
        public void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            var context = actionExecutedContext.HttpContext;
            var respDatas = actionExecutedContext.Result.GetType().GetProperty("Value").GetValue(actionExecutedContext.Result);
            var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(respDatas);

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
            Tools.WriteAPILogFile($"{title} TraceID:{traceId} Data:{jsonData}", eLogType.Debug);
        }
    }
}
