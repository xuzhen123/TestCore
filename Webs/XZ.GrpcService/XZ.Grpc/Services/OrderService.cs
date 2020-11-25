using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcServices;

namespace XZ.Grpc.Services
{
    public class OrderService : CreateOrder.CreateOrderBase
    {
        public override Task<OrderReply> SayCreateOrder(OrderRequest request, ServerCallContext context)
        {
            //throw new Exception("不好意思  出错了");
            //TODO:添加真实的创建订单业务逻辑代码
            return Task.FromResult(new OrderReply { IsCreated = true });
        }
    }
}
