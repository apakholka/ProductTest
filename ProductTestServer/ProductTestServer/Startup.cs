using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using ProductTestServer.BusinessLogic.Services;
using ProductTestServer.BusinessLogic.Services.Interfaces;
using ProductTestServer.DataAccess.Ef;
using ProductTestServer.DataAccess.Interfaces;
using ProductTestServer.DataAccess.Repositories;
using ProductTestServer.Web.MapperConfiguration;

namespace ProductTestServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            var appsettingsSection = Configuration.GetSection("AppSettings");
            var dbType = appsettingsSection["DbType"];
            var connectionString = Configuration.GetConnectionString(dbType);

            //for different DB
            if (dbType == "MySql")
            {
                services.AddDbContext<ApplicationContext>(options => options.UseMySql(connectionString,
                    mysqlOptions =>
                    {
                        mysqlOptions.ServerVersion(new Version(8, 0, 12), ServerType.MySql);
                    }
                ));
            }
            else if (dbType == "MySqlServer")
            {
                services.AddDbContext<ApplicationContext>(options => options.UseMySql(connectionString,
                    mysqlOptions =>
                    {
                        mysqlOptions.ServerVersion(new Version(8, 0, 12), ServerType.MySql);
                    }
                ));
            }
            else if (dbType == "MsSql")
            {
                services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(connectionString));
            }
            else if (dbType == "Localdb")
            {
                services.AddDbContext<ApplicationContext>(options =>
                    options.UseSqlServer(connectionString));
            }

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(options => options.AddPolicy("ClientPolicy",
                builder =>
                {
                    builder.SetIsOriginAllowed(origin => true).AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                }
            ));

            // Auto Mapper Configurations
            var mappingConfig = new AutoMapper.MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            // Data access services
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IProductService, ProductService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseCors("ClientPolicy");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
