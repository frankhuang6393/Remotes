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
    public class OrderController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;

        public OrderController(IUserService userService, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }

        [HttpPost]
        public APIResponseBaseViewModel<PlaceOrderResponseViewModel> POST([FromBody] PlaceOrderViewModel data)
        {
            var resp = new APIResponseBaseViewModel<PlaceOrderResponseViewModel>();
            try
            {
                var user = _userService.GetUser(data.UserName);
                if (user == null)
                {
                    resp.APIReturnCode = eAPIReturnCode.UserIsNotExist;
                    resp.Message = "This UserName is not exist";
                }
                else
                {
                    if (user.Balance >= data.Amount)
                    {
                        _orderService.CreateOrder(new OrderModel()
                        {
                            UserID = user.ID,
                            GameProviderID = data.GameProviderID,
                            OrderID = data.OrderID.ToString("N"),
                            Amount = data.Amount,
                            RefNo = data.RefNo,
                            CreateTime = DateTime.Now,
                            State = eOrderState.Bet,
                        });

                        user.Balance -= data.Amount;
                        _userService.UpdateBalance(user);
                        resp.Success = true;
                        resp.APIReturnCode = eAPIReturnCode.Success;
                    }
                    else
                    {
                        resp.APIReturnCode = eAPIReturnCode.UserInsufficientBalance;
                        resp.Message = "This User is insufficient balance";
                    }

                    resp.Content = new PlaceOrderResponseViewModel()
                    {
                        Balance = user.Balance,
                        UserName = user.UserName,
                        OrderID = data.OrderID.ToString("N"),
                    };
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

        [HttpPatch]
        public APIResponseBaseViewModel<APICommonResponseViewModel> PATCH([FromBody] OrderResultViewModel data)
        {
            APIResponseBaseViewModel<APICommonResponseViewModel> resp = new APIResponseBaseViewModel<APICommonResponseViewModel>();
            try
            {
                var isCheckPass = true;
                var user = _userService.GetUser(data.UserName);
                var order = _orderService.GetOrder(data.OrderID);
                if (user == null)
                {
                    resp.APIReturnCode = eAPIReturnCode.UserIsNotExist;
                    resp.Message = "This UserName is not exist";
                    isCheckPass = false;
                }
                if (order == null)
                {
                    resp.APIReturnCode = eAPIReturnCode.OrderIsNotExist;
                    resp.Message = "This OrderID is not exist";
                    isCheckPass = false;
                }
                if (isCheckPass && order.State == eOrderState.Bet)
                {
                    switch (data.State)
                    {
                        case eOrderState.Won:
                            user.Balance += (order.Amount + data.Amount);
                            break;
                        case eOrderState.Draw:
                            user.Balance += order.Amount;
                            break;
                        case eOrderState.Lose:
                        default:
                            break;
                    }

                    order.State = data.State;
                    order.ModifiedTime = DateTime.Now;

                    _userService.UpdateBalance(user);
                    _orderService.UpdateOrderState(order);

                    resp.Content = new APICommonResponseViewModel()
                    {
                        Balance = user.Balance,
                        UserName = user.UserName,
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
