﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeiXinDevelopDemo.ApiServer;
using Microsoft.Extensions.Caching.SqlServer;
using WeiXinDevelopDemo.Options;

namespace WeiXinDevelopDemo
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
         
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSingleton<IWeiXinApi, WeiXinApi>();
            //    services.AddDistributedMemoryCache();
            //将session 存储到数据中
            services.AddDistributedSqlServerCache(o =>
            {
                o.ConnectionString = "Data Source=.;Initial Catalog=WeiXinDevelop;Integrated Security=True;";
                o.SchemaName = "dbo";
                o.TableName = "TestCache";
            });
            //注册session 服务
            services.AddSession();
            services.Configure<WeiXinDevelopOption>( Configuration.GetSection("WeiXinDevelopOption"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        public void GetCode()
        {

        }
    }
}
