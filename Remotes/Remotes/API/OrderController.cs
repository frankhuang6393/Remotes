using Remotes.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Remotes.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Remotes.Filters;
using Remotes.Services;
using Microsoft.AspNetCore.Authorization;

namespace Remotes.API
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController, TypeFilter(typeof(APIActionCustomFilter))]
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
            var user = _userService.GetUser(data.UserName);
            if (Tools.CheckAPIParamterWithMessage(APICheckParamterType.UserName, resp, user))
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
                        State = OrderState.Bet,
                    });

                    user.Balance -= data.Amount;
                    _userService.UpdateBalance(user);
                }
                else
                {
                    resp.APIReturnCode = APIReturnCode.UserInsufficientBalance;
                    resp.Message = "This user is insufficient balance";
                }

                resp.Content = new PlaceOrderResponseViewModel()
                {
                    Balance = user.Balance,
                    UserName = user.UserName,
                    OrderID = data.OrderID.ToString("N"),
                };
            }

            return resp;
        }

        [HttpPatch]
        public APIResponseBaseViewModel<APICommonResponseViewModel> PATCH([FromBody] OrderResultViewModel data)
        {
            var resp = new APIResponseBaseViewModel<APICommonResponseViewModel>();
            var isCheckPass = true;
            var user = _userService.GetUser(data.UserName);
            var order = _orderService.GetOrder(data.OrderID);
            isCheckPass = Tools.CheckAPIParamterWithMessage(APICheckParamterType.UserName, resp, user)
                    && Tools.CheckAPIParamterWithMessage(APICheckParamterType.OrderID, resp, order);

            if (isCheckPass && order.State == OrderState.Bet)
            {
                switch (data.State)
                {
                    case OrderState.Won:
                        user.Balance += (order.Amount + data.Amount);
                        break;
                    case OrderState.Draw:
                        user.Balance += order.Amount;
                        break;
                    case OrderState.Lose:
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
                resp.APIReturnCode = APIReturnCode.Success;
            }

            return resp;
        }
    }
}
