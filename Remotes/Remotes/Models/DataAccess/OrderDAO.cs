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
        private string _connectString;
        public OrderDAO(IConfiguration configruration)
        {
            _connectString = configruration.GetConnectionString("DefaultConnectionString");
        }

        public long CreateOrder(OrderModel model)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(_connectString))
            {
                return conn.QueryFirst<long>("CreateOrder", model, commandType: CommandType.StoredProcedure);
            }
        }

        public OrderModel GetOrder(string orderId)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(_connectString))
            {
                var parm = new OrderModel { OrderID = orderId, CreateTime = DateTime.Now };
                return conn.QueryFirstOrDefault<OrderModel>("GetTop1OrderByOrderID", parm, commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateOrderState(OrderModel model)
        {
            using (var conn = new System.Data.SqlClient.SqlConnection(_connectString))
            {
                conn.Execute("UpdateOrderStateByOrderID", model, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
