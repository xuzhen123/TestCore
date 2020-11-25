using Grpc.Core;
using Grpc.Core.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XZ.Grpc.Interceptors
{
    public class ExceptionInterceptior : Interceptor
    {
        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            try
            {
                return base.UnaryServerHandler(request, context, continuation);
            }
            catch (Exception ex)
            {
                var data = new Metadata();

                data.Add("ExceptionMessage", ex.Message);

                throw new RpcException(new Status(StatusCode.Unknown, "Unknown"), data);
            }
        }
    }
}
