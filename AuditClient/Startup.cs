using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net.Repository.Hierarchy;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Logging;
using AuditClient.Provider;
using AuditClient.Repository;
using AuditClient.Data;
using Microsoft.EntityFrameworkCore;
using AuditClient.Helpers;

namespace AuditClient
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<ResponseDbContext>(options => options.UseSqlServer(_config.GetConnectionString("ResponseConnectionString")));
            services.AddSwaggerGen(c => { c.SwaggerDoc("AuditClient", new OpenApiInfo { Title = "Audit Client", Version = "1.0" }); });
            services.AddSession(op => op.IdleTimeout = TimeSpan.FromMinutes(30));
            services.AddScoped<ICheckListProvider, CheckListProvider>();
            services.AddScoped<ILoginProvider, LoginProvider>();
            services.AddScoped<ISeverityProvider, SeverityProvider>();
            services.AddScoped<IResponseRepo, ResponseRepo>();
            services.AddScoped<IClientAddress, ClientAddress>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
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
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/AuditClient/swagger.json", "AuditClient"));


            loggerFactory.AddLog4Net();
            app.UseStaticFiles();
            
            app.UseRouting();
            
            app.UseAuthorization();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
