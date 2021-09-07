using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Remotes.Services
{
    public interface IOrderService
    {
        public long CreateOrder(Models.OrderModel model);

        public Models.OrderModel GetOrder(string orderId);

        public void UpdateOrderState(Models.OrderModel model);
    }
}
