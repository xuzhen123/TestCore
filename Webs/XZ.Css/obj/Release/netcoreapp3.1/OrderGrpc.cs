// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Order.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace GrpcServices {
  /// <summary>
  /// The greeting service definition.
  /// </summary>
  public static partial class CreateOrder
  {
    static readonly string __ServiceName = "GrpcServices.CreateOrder";

    static readonly grpc::Marshaller<global::GrpcServices.OrderRequest> __Marshaller_GrpcServices_OrderRequest = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::GrpcServices.OrderRequest.Parser.ParseFrom);
    static readonly grpc::Marshaller<global::GrpcServices.OrderReply> __Marshaller_GrpcServices_OrderReply = grpc::Marshallers.Create((arg) => global::Google.Protobuf.MessageExtensions.ToByteArray(arg), global::GrpcServices.OrderReply.Parser.ParseFrom);

    static readonly grpc::Method<global::GrpcServices.OrderRequest, global::GrpcServices.OrderReply> __Method_SayCreateOrder = new grpc::Method<global::GrpcServices.OrderRequest, global::GrpcServices.OrderReply>(
        grpc::MethodType.Unary,
        __ServiceName,
        "SayCreateOrder",
        __Marshaller_GrpcServices_OrderRequest,
        __Marshaller_GrpcServices_OrderReply);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::GrpcServices.OrderReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of CreateOrder</summary>
    [grpc::BindServiceMethod(typeof(CreateOrder), "BindService")]
    public abstract partial class CreateOrderBase
    {
      /// <summary>
      /// Sends a greeting
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::GrpcServices.OrderReply> SayCreateOrder(global::GrpcServices.OrderRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for CreateOrder</summary>
    public partial class CreateOrderClient : grpc::ClientBase<CreateOrderClient>
    {
      /// <summary>Creates a new client for CreateOrder</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public CreateOrderClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for CreateOrder that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public CreateOrderClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected CreateOrderClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected CreateOrderClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      /// Sends a greeting
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::GrpcServices.OrderReply SayCreateOrder(global::GrpcServices.OrderRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SayCreateOrder(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Sends a greeting
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::GrpcServices.OrderReply SayCreateOrder(global::GrpcServices.OrderRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_SayCreateOrder, null, options, request);
      }
      /// <summary>
      /// Sends a greeting
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::GrpcServices.OrderReply> SayCreateOrderAsync(global::GrpcServices.OrderRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return SayCreateOrderAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      /// Sends a greeting
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::GrpcServices.OrderReply> SayCreateOrderAsync(global::GrpcServices.OrderRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_SayCreateOrder, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override CreateOrderClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new CreateOrderClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(CreateOrderBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_SayCreateOrder, serviceImpl.SayCreateOrder).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, CreateOrderBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_SayCreateOrder, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::GrpcServices.OrderRequest, global::GrpcServices.OrderReply>(serviceImpl.SayCreateOrder));
    }

  }
}
#endregion
