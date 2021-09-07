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
    public class OrderDAO : BaseDAO<OrderModel>, IOrderService
    {
        public OrderDAO(IConfiguration configruration) : base(configruration)
        {
        }

        public long CreateOrder(OrderModel model)
        {
            return (long)Excute("CreateOrder", model);
        }
        
        public OrderModel GetOrder(string orderId)
        {
            var parm = new OrderModel { OrderID = orderId, CreateTime = DateTime.Now };
            return Query("GetTop1OrderByOrderID", parm);
        }

        public void UpdateOrderState(OrderModel model)
        {
            Excute("UpdateOrderStateByOrderID", model);
        }
    }
}
