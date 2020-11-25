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

            //����ע��
            services.AddServices();
            //ע�����ݿ�������
            services.AddDbContext(Configuration);
            //ע�Ἧ���¼�
            services.AddEventBus(Configuration);
            //ע��MediatR �����������¼�
            services.AddMediatRServices<Merchant>();

            #region �����֤
            //1.cookie Ĭ��ʹ��cookie��ʽ���������֤
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/account/login";//��ת����¼����
                options.LogoutPath = "/account/logout";//�ǳ�
                options.AccessDeniedPath = "/../";//��Ȩ��ʱ ��ת���Ľ���
                options.Cookie.HttpOnly = true;//ֻ�������Է���˵�http����  �ƶ��ˣ���׿��IOS���޷����� �����Է�ֹ��վ�ű�����
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);//��¼��Чʱ��
            })
            //2. jwt�����֤
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//�Ƿ���֤Issuer
                    ValidateAudience = true,//�Ƿ���֤Audience
                    ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                    ClockSkew = TimeSpan.FromSeconds(30),//ʧЧ��ƫ��ʱ�䣨��ʧЧ��30���� ��Ȼ����ʹ�ã�
                    ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                    ValidAudience = "localhost",//Audience
                    ValidIssuer = "localhost",//Issuer
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecurityKey"]))//�õ�SecurityKey
                };
            });
            #endregion

            #region ����վ�ű�����
            //services.AddAntiforgery(options =>
            //{
            //    options.HeaderName = "X-CSRF-TOKEN";
            //});

            //����ȫ�ֵ�AddAntiforgeryToken��֤(��ζ�� ���е�http post���� header����Я�� X-CSRF-TOKEN)
            //services.AddMvc(options => options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
            #endregion

            #region ������������
            services.AddCors(options =>
            {
                //options.AddPolicy("abc", builder =>
                //{   //ע��һ���� abc�Ĳ���  ����������� ����https://localhost:5001�Ŀ������� �������κ�header ����Я��cookie�����֤��Ϣ  �Լ�������ʷ��ص�header�б��磺header123 ......
                //    builder.WithOrigins("https://localhost:5001").AllowAnyHeader().AllowCredentials().WithExposedHeaders("header123");
                //});

                //options.AddDefaultPolicy(builder =>
                // {
                //     //����Ĭ�ϵĿ������  ��Ĭ�ϲ�����ȫ�ֲ���  �ò��Բ���Я�������֤��Ϣ
                // });

                //options.AddPolicy("wer", builder =>
                //{
                //    //��������������Դ ���п������� ͬʱЯ�������֤��Ϣ
                //    builder.SetIsOriginAllowed(option => true).AllowCredentials().AllowAnyHeader();
                //});
            });
            #endregion

            #region �쳣����������  �����쳣������ ֻ����� MVC,webApi������������ 
            services.AddMvc(mvcOptions =>
            {
                //mvcOptions.Filters.Add<MyExceptionFilter>();
                //mvcOptions.Filters.Add<MyExceptionFilterAttribute>();
            });
            #endregion

            #region web api ·�����ã�ע��WEB APIʹ�ã�
            //services.AddRouting(options =>
            //{
            //    options.ConstraintMap.Add("UserLogin", typeof(WebRouteConstraint));
            //});
            #endregion

            #region Http����Ĵ���ʽ
            //Http������
            //services.AddHttpClient();
            //services.AddScoped<HttpClientRequest>();

            //�����������ʽ
            //services.AddHttpClient("name", client =>
            //{
            //    client.BaseAddress = new Uri("https://localhost:5001/api/httptest/get");
            //    client.Timeout = TimeSpan.FromMinutes(10);
            //}).ConfigureHttpMessageHandlerBuilder(builder => builder.Services.GetService<HttpClientRequest>());
            #endregion

            #region ����GRPC ����
            //AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);//���Բ�����֤����з���
            services.AddGrpcClient<CreateOrder.CreateOrderClient>(options =>
            {
                options.Address = new Uri("https://localhost:5005");

                //Http2�ǻ���httpsЭ��  �������AppContext�Ĵ���
                //options.Address = new Uri("http://localhost:5007");
            }).ConfigurePrimaryHttpMessageHandler(provider =>
            {
                var handler = new SocketsHttpHandler();
                handler.SslOptions.RemoteCertificateValidationCallback = (a, b, c, d) => true;//������Ч������ǩ��֤��
                return handler;
            }).AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(10, i => TimeSpan.FromSeconds(2)));//��HTTP ��Ӧ����500��408ʱ ����10�� ÿ�����Լ��ʱ��Ϊ2�루HTTPclient ���ò��ԣ�
            #endregion

            services.AddHttpClient<HttpClientApi1>();

            #region �����Լ��Ĳ��� ʧ�����Բ���

            var pollys = services.AddPolicyRegistry();
            pollys.Add("retryforever", Policy.HandleResult<HttpResponseMessage>(message =>
            {
                return message.StatusCode == System.Net.HttpStatusCode.Created;
            }).RetryForeverAsync());

            //����Ӧ�� Ϊָ���� http���� ʹ��ָ���Ĳ���
            services.AddHttpClient<HttpClientApi2>().AddPolicyHandlerFromRegistry("retryforever");


            //����������������ò���  �������GET���� ��ִ���κβ���   ����ִ��retryforever����
            services.AddHttpClient<HttpClientApi2>().AddPolicyHandlerFromRegistry((registry, message) =>
            {
                return message.Method == HttpMethod.Get ? registry.Get<IAsyncPolicy<HttpResponseMessage>>("retryforever") : Policy.NoOpAsync<HttpResponseMessage>();
            });


            //�۶Ϻ���������
            services.AddHttpClient<HttpClientApi2>().AddPolicyHandler(Policy<HttpResponseMessage>.Handle<HttpRequestException>().CircuitBreakerAsync(handledEventsAllowedBeforeBreaking: 10,//�������
                durationOfBreak: TimeSpan.FromSeconds(10),//�۶�ʱ��
                onBreak: (a, b) => { },//�����۶�ʱ  �������¼�
                onReset: () => { },//�۶ϻָ�ʱ  �������¼�
                onHalfOpen: () => { }//����ָ�ǰ  ��֤�����Ƿ����
                ));

            //�߼��۶ϲ���
            services.AddHttpClient<HttpClientApi2>().AddPolicyHandler(Policy<HttpResponseMessage>.Handle<HttpRequestException>().AdvancedCircuitBreakerAsync
                (failureThreshold: 0.6,//60%���������ʱ�۶�
                samplingDuration: TimeSpan.FromSeconds(30),//����ʱ�䷶Χ�ڣ�30�룩 �������
                minimumThroughput: 50,//��С�Ĳ���������  ��30����  ��С�����������ڵ���50�� ���ܴ���60% ���۶ϻ���
                durationOfBreak: TimeSpan.FromSeconds(10),//�۶�ʱ�� 10����
                onBreak: (a, b) => { },//�����۶�ʱ  �������¼�
                onReset: () => { },//�۶ϻָ�ʱ  �������¼�
                onHalfOpen: () => { }//����ָ�ǰ  ��֤�����Ƿ����
                ));



            //�����Ѻõ���Ӧ�쳣��Ϣ
            var messages = new HttpResponseMessage
            {
                Content = new StringContent("������")
            };
            //���񽵼����� �����۶��쳣
            var fallback = Policy<HttpResponseMessage>.Handle<BrokenCircuitException>().FallbackAsync(messages);

            //�����۶ϲ���
            var breaker = Policy<HttpResponseMessage>.Handle<HttpRequestException>().AdvancedCircuitBreakerAsync
                (failureThreshold: 0.6,//60%���������ʱ�۶�
                samplingDuration: TimeSpan.FromSeconds(30),//����ʱ�䷶Χ�ڣ�30�룩 �������
                minimumThroughput: 50,//��С�Ĳ���������  ��30����  ��С�����������ڵ���50�� ���ܴ���60% ���۶ϻ���
                durationOfBreak: TimeSpan.FromSeconds(10),//�۶�ʱ�� 10����
                onBreak: (a, b) => { },//�����۶�ʱ  �������¼�
                onReset: () => { },//�۶ϻָ�ʱ  �������¼�
                onHalfOpen: () => { }//����ָ�ǰ  ��֤�����Ƿ����
                );

            //������� Ȼ��Ӧ�õ� HttpClient������
            var fallbackAndBreaker = Policy.WrapAsync(fallback, breaker);

            services.AddHttpClient<HttpClientApi2>().AddPolicyHandler(fallbackAndBreaker);



            //��������
            var bulk = Policy.BulkheadAsync<HttpResponseMessage>(
                maxParallelization: 100,//������󲢷�����100
                maxQueuingActions: 30 //����������ָ���� ���󲢷�����100�� ʣ��������� �������Ϊ30�� �������� ���׳��쳣
                );

            //�����Ѻõ���Ӧ�쳣��Ϣ
            var messages2 = new HttpResponseMessage
            {
                Content = new StringContent("���ֳ�����")
            };

            //���������쳣
            var fallbackbulk = Policy<HttpResponseMessage>.Handle<BulkheadRejectedException>().FallbackAsync(messages2);

            //����������Ժ� �����쳣
            var bulkAndFallback = Policy.WrapAsync(fallbackbulk, bulk);

            services.AddHttpClient<HttpClientApi2>().AddPolicyHandler(bulkAndFallback);
            #endregion

            #region ���洦��
            services.AddMemoryCache(); //����ڴ滺��
            //���Redis���� �����ֲ�ʽ���棩
            services.AddStackExchangeRedisCache(options =>
            {
                Configuration.GetSection("Redis").Bind(options);
            });

            //.net core �ṩ��ӦMVC action�Ļ���  ��Ӧ֮ǰ��OutPutCache
            services.AddResponseCaching();

            services.AddEasyCaching(options =>
            {
                //��redis�����ӽ���
                options.UseRedis(Configuration, name: "easycaching");
            });
            #endregion
        }

        //This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //��������Ĭ�ϵ��쳣����
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //�������� ���������Զ������ҳ��
                app.UseExceptionHandler("/error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            #region �쳣�����м��
            //ʹ���Զ����쳣��Ϣҳ�洦��
            //app.UseExceptionHandler("/error");

            //ʹ�ô�����쳣����ʽ
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

            app.UseCors();//��ӿ����м��  ʹ�����ע����Ч  �������ڿ�ǰ��λ��

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseResponseCaching();//���ResponseCaching�м��

            app.UseAuthentication();//�����֤ �м��
            app.UseAuthorization();//�����Ȩ �м��


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
