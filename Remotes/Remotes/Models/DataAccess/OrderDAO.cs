using Dapper;
using Microsoft.Extensions.Configuration;
using Remotes.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remotes.Models
{
    public class OrderDAO : IOrderService
    {
        private readonly IDaoService<OrderModel> _daoService;
        public OrderDAO(IDaoService<OrderModel> daoService)
        {
            _daoService = daoService;
        }

        public long CreateOrder(OrderModel model)
        {
            return (long)(_daoService.Excute("CreateOrder", model) ?? -1L);
        }
        
        public OrderModel GetOrder(string orderId)
        {
            var parm = new OrderModel { OrderID = orderId, CreateTime = DateTime.Now };
            return _daoService.Query("GetTop1OrderByOrderID", parm);
        }

        public void UpdateOrderState(OrderModel model)
        {
            _daoService.Excute("UpdateOrderStateByOrderID", model);
        }
    }
}
