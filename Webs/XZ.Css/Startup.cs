using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using XZ.Main.Repository;
using MediatR;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;
using XZ.Main.Domain;
using XZ.Css.ServiceExtensions;
using XZ.Core.Exceptions;
using XZ.Css.RouteConstraints;
using static GrpcServices.CreateOrder;
using GrpcServices;
using System.Net.Http;
using Polly;
using Polly.CircuitBreaker;
using Polly.Bulkhead;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using XZ.Css.HttpClients;

namespace XZ.Css
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            //服务注册
            services.AddServices();
            //注册数据库上下文
            services.AddDbContext(Configuration);
            //注册集成事件
            services.AddEventBus(Configuration);
            //注册MediatR 来传递领域事件
            services.AddMediatRServices<Merchant>();

            #region 身份认证
            //1.cookie 默认使用cookie方式进行身份认证
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/account/login";//跳转到登录界面
                options.LogoutPath = "/account/logout";//登出
                options.AccessDeniedPath = "/../";//无权限时 跳转到的界面
                options.Cookie.HttpOnly = true;//只允许来自服务端的http请求  移动端（安卓，IOS）无法访问 还可以防止跨站脚本攻击
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);//登录有效时间
            })
            //2. jwt身份认证
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ClockSkew = TimeSpan.FromSeconds(30),//失效的偏离时间（在失效的30秒内 仍然可以使用）
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidAudience = "localhost",//Audience
                    ValidIssuer = "localhost",//Issuer
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))//拿到SecurityKey
                };
            });
            #endregion

            #region 防跨站脚本攻击
            //services.AddAntiforgery(options =>
            //{
            //    options.HeaderName = "X-CSRF-TOKEN";
            //});

            //开启全局的AddAntiforgeryToken验证(意味着 所有的http post请求 header必须携带 X-CSRF-TOKEN)
            //services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
            #endregion

            #region 跨域请求设置
            services.AddCors(options =>
            {
                //options.AddPolicy("abc", builder =>
                //{   //注册一个叫 abc的策略  这个策略允许 来自https://localhost:5001的跨域请求 可以是任何header 允许携带cookie身份认证信息  以及允许访问返回的header列表如：header123 ......
                //    builder.WithOrigins("https://localhost:5001").AllowAnyHeader().AllowCredentials().WithExposedHeaders("header123");
                //});

                //options.AddDefaultPolicy(builder =>
                // {
                //     //设置默认的跨域策略  该默认策略是全局策略  该策略不能携带身份认证信息
                // });

                //options.AddPolicy("wer", builder =>
                //{
                //    //允许任意域名来源 进行跨域请求 同时携带身份认证信息
                //    builder.SetIsOriginAllowed(option => true).AllowCredentials().AllowAnyHeader();
                //});
            });
            #endregion

            #region 异常处理：过滤器  此种异常处理方法 只是针对 MVC,webApi的请求周期里 
            services.AddMvc(mvcOptions =>
            {
                //mvcOptions.Filters.Add<MyExceptionFilter>();
                //mvcOptions.Filters.Add<MyExceptionFilterAttribute>();
            });
            #endregion

            #region web api 路由设置（注：WEB API使用）
            //services.AddRouting(options =>
            //{
            //    options.ConstraintMap.Add("UserLogin", typeof(WebRouteConstraint));
            //});
            #endregion

            #region Http请求的处理方式
            //Http请求处理
            //services.AddHttpClient();
            //services.AddScoped<HttpClientRequest>();

            //代理类的请求方式
            //services.AddHttpClient("name", client =>
            //{
            //    client.BaseAddress = new Uri("https://localhost:5001/api/httptest/get");
            //    client.Timeout = TimeSpan.FromMinutes(10);
            //}).ConfigureHttpMessageHandlerBuilder(builder => builder.Services.GetService<HttpClientRequest>());
            #endregion

            #region 调用GRPC 服务
            //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);//可以不配置证书进行访问
            services.AddGrpcClient<CreateOrder.CreateOrderClient>(options =>
            {
                options.Address = new Uri("https://localhost:5005");

                //Http2是基于https协议  配合上面AppContext的代码
                //options.Address = new Uri("http://localhost:5007");
            }).ConfigurePrimaryHttpMessageHandler(provider =>
            {
                var handler = new SocketsHttpHandler();
                handler.SslOptions.RemoteCertificateValidationCallback = (a, b, c, d) => true;//允许无效，或自签名证书
                return handler;
            }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(10, i => TimeSpan.FromSeconds(2)));//当HTTP 响应码是500或408时 重试10次 每次重试间隔时间为2秒（HTTPclient 内置策略）
            #endregion

            services.AddHttpClient<HttpClientApi1>();

            #region 定义自己的策略 失败重试策略

            var pollys = services.AddPolicyRegistry();
            pollys.Add("retryforever", Policy.HandleResult<HttpResponseMessage>(message =>
            {
                return message.StatusCode == System.Net.HttpStatusCode.Created;
            }).RetryForeverAsync());

            //策略应用 为指定的 http请求 使用指定的策略
            services.AddHttpClient<HttpClientApi2>().AddPolicyHandlerFromRegistry("retryforever");


            //根据请求参数来设置策略  如果不是GET请求 则不执行任何策略   否则执行retryforever策略
            services.AddHttpClient<HttpClientApi2>().AddPolicyHandlerFromRegistry((registry, message) =>
            {
                return message.Method == HttpMethod.Get ? registry.Get<IAsyncPolicy<HttpResponseMessage>>("retryforever") : Policy.NoOpAsync<HttpResponseMessage>();
            });


            //熔断和限流策略
            services.AddHttpClient<HttpClientApi2>().AddPolicyHandler(Policy<HttpResponseMessage>.Handle<HttpRequestException>().CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 10,//报错次数
                durationOfBreak: TimeSpan.FromSeconds(10),//熔断时间
                onBreak: (a, b) => { },//发声熔断时  触发的事件
                onReset: () => { },//熔断恢复时  触发的事件
                onHalfOpen: () => { }//服务恢复前  验证服务是否可用
                ));

            //高级熔断策略
            services.AddHttpClient<HttpClientApi2>().AddPolicyHandler(Policy<HttpResponseMessage>.Handle<HttpRequestException>().AdvancedCircuitBreakerAsync
                (failureThreshold: 0.6,//60%的请求出错时熔断
                samplingDuration: TimeSpan.FromSeconds(30),//采样时间范围内（30秒） 请求比例
                minimumThroughput: 50,//最小的采样请求数  即30秒内  最小的请求数大于等于50次 才能触发60% 的熔断机制
                durationOfBreak: TimeSpan.FromSeconds(10),//熔断时长 10秒钟
                onBreak: (a, b) => { },//发声熔断时  触发的事件
                onReset: () => { },//熔断恢复时  触发的事件
                onHalfOpen: () => { }//服务恢复前  验证服务是否可用
                ));



            //构造友好的响应异常信息
            var messages = new HttpResponseMessage
            {
                Content = new StringContent("出错了")
            };
            //服务降级策略 捕获熔断异常
            var fallback = Policy<HttpResponseMessage>.Handle<BrokenCircuitException>().FallbackAsync(messages);

            //定义熔断策略
            var breaker = Policy<HttpResponseMessage>.Handle<HttpRequestException>().AdvancedCircuitBreakerAsync
                (failureThreshold: 0.6,//60%的请求出错时熔断
                samplingDuration: TimeSpan.FromSeconds(30),//采样时间范围内（30秒） 请求比例
                minimumThroughput: 50,//最小的采样请求数  即30秒内  最小的请求数大于等于50次 才能触发60% 的熔断机制
                durationOfBreak: TimeSpan.FromSeconds(10),//熔断时长 10秒钟
                onBreak: (a, b) => { },//发声熔断时  触发的事件
                onReset: () => { },//熔断恢复时  触发的事件
                onHalfOpen: () => { }//服务恢复前  验证服务是否可用
                );

            //策略组合 然后应用到 HttpClient请求上
            var fallbackAndBreaker = Policy.WrapAsync(fallback, breaker);

            services.AddHttpClient<HttpClientApi2>().AddPolicyHandler(fallbackAndBreaker);



            //限流策略
            var bulk = Policy.BulkheadAsync<HttpResponseMessage>(
                maxParallelization: 100,//定义最大并发数是100
                maxQueuingActions: 30 //最大队列数（指的是 请求并发超过100后 剩余的请求数 队列最大为30） 超出队列 会抛出异常
                );

            //构造友好的响应异常信息
            var messages2 = new HttpResponseMessage
            {
                Content = new StringContent("我又出错了")
            };

            //捕获限流异常
            var fallbackbulk = Policy<HttpResponseMessage>.Handle<BulkheadRejectedException>().FallbackAsync(messages2);

            //组合限流策略和 限流异常
            var bulkAndFallback = Policy.WrapAsync(fallbackbulk, bulk);

            services.AddHttpClient<HttpClientApi2>().AddPolicyHandler(bulkAndFallback);
            #endregion

            #region 缓存处理
            services.AddMemoryCache(); //添加内存缓存
            //添加Redis缓存 （即分布式缓存）
            services.AddStackExchangeRedisCache(options =>
            {
                Configuration.GetSection("Redis").Bind(options);
            });

            //.net core 提供对应MVC action的缓存  对应之前的OutPutCache
            services.AddResponseCaching();

            services.AddEasyCaching(options =>
            {
                //将redis组件添加进来
                options.UseRedis(Configuration, name: "easycaching");
            });
            #endregion
        }

        //This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //开发环境默认的异常处理
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //生产环境 可以启用自定义错误页面
                app.UseExceptionHandler("/error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            #region 异常处理中间件
            //使用自定义异常信息页面处理
            //app.UseExceptionHandler("/error");

            //使用代理的异常处理方式
            //app.UseExceptionHandler(errorApp =>
            //{
            //    errorApp.Run(async context =>
            //    {
            //        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            //        IKnownException knownException = exceptionHandlerPathFeature.Error as IKnownException;
            //        if (knownException == null)
            //        {
            //            var logger = context.RequestServices.GetService<ILogger<MyExceptionFilterAttribute>>();
            //            logger.LogError(exceptionHandlerPathFeature.Error, exceptionHandlerPathFeature.Error.Message);
            //            knownException = KnownException.UnKown;
            //            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            //        }
            //        else
            //        {
            //            knownException = KnownException.FromKnownException(knownException);
            //            context.Response.StatusCode = StatusCodes.Status200OK;
            //        }
            //        var jsonOptions = context.RequestServices.GetService<IOptions<JsonOptions>>();
            //        context.Response.ContentType = "application/json; charset=utf-8";
            //        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(knownException, jsonOptions.Value.JsonSerializerOptions));
            //    });
            //});
            #endregion

            app.UseCors();//添加跨域中间件  使上面的注入生效  尽量放在靠前的位置

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseResponseCaching();//添加ResponseCaching中间件

            app.UseAuthentication();//身份认证 中间件
            app.UseAuthorization();//身份授权 中间件


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
