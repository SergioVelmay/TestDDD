using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json.Serialization;
using Domain.Domains;
using Domain.Services;
using Infrastructure.Shared.Interfaces;
using Domain.Shared.Repositories;
using Infrastructure.Authorization;

namespace S2T
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
            services.AddControllers();

            ConfigureBasicSettings(services);

            //ConfigureSQLServer(services);
            ConfigureMongoDB(services);
            ConfigureDomains(services);

            ConfigureIdentityServer4(services);

            services.AddScoped<ICompanyService, CompanyService>();
        }

        private static void ConfigureBasicSettings(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            services.AddMvcCore(options =>
            {
                options.EnableEndpointRouting = false;
            }).SetCompatibilityVersion(CompatibilityVersion.Latest).AddApiExplorer();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddOptions();
        }

        private void ConfigureDomains(IServiceCollection services)
        {
            services.AddScoped<ICompanyDomain, CompanyDomain>();
        }

        private void ConfigureMongoDB(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Infrastructure.Persistence.MongoDB.Helpers.Mappers());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.Configure<Infrastructure.Persistence.MongoDB.Helpers.MongoDBSettings>(options =>
            {
                options.CollectionName = Configuration.GetSection("MongoConnection:CollectionName").Value;
                options.Database = Configuration.GetSection("MongoConnection:Database").Value;
            });

            services.AddSingleton<IMongoClient>(new MongoClient(Configuration.GetSection("MongoConnection:ConnectionString").Value));

            services.AddScoped<IDbContext, Infrastructure.Persistence.MongoDB.Contexts.GenericDbContext<Infrastructure.Persistence.MongoDB.DAO.Company>>();
            services.AddScoped<IPersistence<Infrastructure.Persistence.MongoDB.DAO.Company, string>, Infrastructure.Persistence.MongoDB.Helpers.Persistence<Infrastructure.Persistence.MongoDB.DAO.Company, string>>();
            services.AddScoped<IQuery<Infrastructure.Persistence.MongoDB.DAO.Company, string>, Infrastructure.Persistence.MongoDB.Helpers.Query<Infrastructure.Persistence.MongoDB.DAO.Company, string>>();

            services.AddScoped<Domain.Shared.Repositories.ICompanyRepository, Infrastructure.Persistence.MongoDB.Repositories.CompanyRepository>();

        }

        private void ConfigureSQLServer(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Infrastructure.Persistence.SQLServer.Helpers.Mappers());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.AddDbContext<IDbContext, Infrastructure.Persistence.SQLServer.Contexts.GenericDbContext<Infrastructure.Persistence.SQLServer.DAO.Company>>(options => options.UseInMemoryDatabase("Company"), ServiceLifetime.Transient);

            services.AddScoped<IPersistence<Infrastructure.Persistence.SQLServer.DAO.Company, Guid?>, Infrastructure.Persistence.SQLServer.Helpers.Persistence<Infrastructure.Persistence.SQLServer.DAO.Company, Guid?>>();
            services.AddScoped<IQuery<Infrastructure.Persistence.SQLServer.DAO.Company, Guid?>, Infrastructure.Persistence.SQLServer.Helpers.Query<Infrastructure.Persistence.SQLServer.DAO.Company, Guid?>>();

            services.AddScoped<Domain.Shared.Repositories.ICompanyRepository, Infrastructure.Persistence.SQLServer.Repositories.CompanyRepository>();
        }

        private void ConfigureIdentityServer4(IServiceCollection services)
        {
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = Configuration.GetSection("IdentityServer:ISUrl").Value;
                    options.Audience = Configuration.GetSection("IdentityServer:ISAudience").Value;
                    options.RequireHttpsMetadata = false;
                });

            services.AddScoped<IIdentityServerRepository, IdentityServerRepository>();
            services.AddScoped<IIdentityServerServices, IdentityServerServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseMvc();

            app.UseAuthorization();
        }
    }
}
