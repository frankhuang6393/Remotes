using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Remotes.ViewModel;
using Remotes.Models;
using Remotes.Filters;
using Remotes.Services;

namespace Remotes.API
{
    [Route("api/[controller]")]
    [ApiController, TypeFilter(typeof(APILogActionFilter))]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public APIResponseBaseViewModel<APICommonResponseViewModel> POST([FromBody] UserViewModel data)
        {
            var resp = new APIResponseBaseViewModel<APICommonResponseViewModel>();
            try
            {
                _userService.CreateUser(new UserModel()
                {
                    Balance = data.Balance,
                    Currency = data.Currency,
                    UserName = data.UserName,
                });

                resp.Content = new APICommonResponseViewModel()
                {
                    Balance = data.Balance,
                    UserName = data.UserName,
                };
                resp.Success = true;
                resp.APIReturnCode = APIReturnCode.Success;
            }
            catch (Exception ex)
            {
                resp.Success = false;
                resp.APIReturnCode = APIReturnCode.Exception;
                resp.Message = ex.Message;
            }

            return resp;
        }

        [HttpGet]
        public APIResponseBaseViewModel<UserViewModel> GET(string userName)
        {
            var resp = new APIResponseBaseViewModel<UserViewModel>();
            try
            {
                var user = _userService.GetUser(userName);
                if (Tools.CheckAPIParamterWithMessage(APICheckParamterType.UserName, resp, user))
                {
                    resp.Content = new UserViewModel()
                    {
                        Balance = user.Balance,
                        Currency = user.Currency,
                        UserName = user.UserName,
                    };
                    resp.Success = true;
                    resp.APIReturnCode = APIReturnCode.Success;
                }
            }
            catch (Exception ex)
            {
                resp.Success = false;
                resp.APIReturnCode = APIReturnCode.Exception;
                resp.Message = ex.Message;
            }

            return resp;
        }
    }
}
