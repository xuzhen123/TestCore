using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Test;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XZ.Main.Domain;
using XZ.Main.Repository;
using XZ.SSO.Ids4.ServiceExtensions;

namespace XZ.SSO.Ids4
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

            //ע�����ݿ�������
            services.AddDbContext<DomainContext>(config =>
            {
                config.UseMySql(Configuration.GetSection("MySql").Value);
            });

            services.Configure<IISOptions>(options =>
            {
                options.AuthenticationDisplayName = "Windows";
                options.AutomaticAuthentication = false;
            });

            services.Configure<IISServerOptions>(options =>
            {
                options.AuthenticationDisplayName = "Windows";
                options.AutomaticAuthentication = false;
            });

            services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
            .AddInMemoryIdentityResources(Config.Ids)
            .AddInMemoryApiResources(Config.Apis)
            .AddInMemoryClients(Config.Clients)
            .AddDeveloperSigningCredential();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseIdentityServer();//ע�� id4s�����м��
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
