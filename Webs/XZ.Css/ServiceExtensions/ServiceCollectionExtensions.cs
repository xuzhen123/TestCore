using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XZ.Core.Logs;
using XZ.Main.Repository;

namespace XZ.Css.ServiceExtensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<IMerchantRepository, MerchantRepository>();
            services.AddScoped<IMerchantUserRepository, MerchantUserRepository>();
            services.AddScoped<ISysUserRepository, SysUserRepository>();
            services.AddScoped<ISysRoleRepository, SysRoleRepository>();
            services.AddScoped<ISysUserAndRoleRepository, SysUserAndRoleRepository>();
            services.AddScoped<ISysPermissionRepository, SysPermissionRepository>();
            services.AddTransient<IMyLog, MyLog>();

            return services;
        }

        /// <summary>
        /// 注册DbContext 数据库上下文
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MainContext>(config =>
            {
                config.UseMySql(configuration.GetSection("MySql").Value);
            });

            return services;
        }

        /// <summary>
        /// 注册集成事件服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            //注册领域事件服务
            //services.AddTransient<ISubscriberService, SubscriberService>();

            //注册CAP组件
            services.AddCap(options =>
            {
                options.UseEntityFramework<MainContext>();

                options.UseRabbitMQ(option =>
                {
                    configuration.GetSection("RabbitMQ").Bind(option);
                });
                //options.UseDashboard();
            });

            return services;
        }

        /// <summary>
        /// 注入 MediatR 用来传递领域事件的服务
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMediatRServices<T>(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(MainContextTransactionBehavior<,>));
            return services.AddMediatR(typeof(T).Assembly, typeof(Program).Assembly);
        }
    }
}
