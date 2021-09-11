﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Remotes.ViewModel;
using Remotes.Models;
using Remotes.Filters;
using Remotes.Services;
using Microsoft.AspNetCore.Authorization;

namespace Remotes.API
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController, TypeFilter(typeof(APIActionCustomFilter))]
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

            return resp;
        }

        [HttpGet]
        public APIResponseBaseViewModel<UserViewModel> GET(string userName)
        {
            var resp = new APIResponseBaseViewModel<UserViewModel>();
            var user = _userService.GetUser(userName);
            if (Tools.CheckAPIParamterWithMessage(APICheckParamterType.UserName, resp, user))
            {
                resp.Content = new UserViewModel()
                {
                    Balance = user.Balance,
                    Currency = user.Currency,
                    UserName = user.UserName,
                };
            }

            return resp;
        }
    }
}
