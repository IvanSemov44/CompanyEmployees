﻿namespace CompanyEmployees.Extensions
{
    using CompanyEmployees.Presentation.Controllers;
    using Contracts;
    using LoggerService;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Formatters;
    using Microsoft.AspNetCore.Mvc.Versioning;
    using Microsoft.EntityFrameworkCore;
    using Repository;
    using Service;
    using Service.Contracts;

    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("X-Pagination"));
            });

        public static void ConfigureLoggerService(this IServiceCollection services) =>
            services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();
        public static void ConfigureSqlContext(this IServiceCollection services,IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));

        public static void AddCustomMediaTypes(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(config =>
            {
                var systemTextJsonOutputFormatter = config.OutputFormatters
                .OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();

                if (systemTextJsonOutputFormatter != null)
                {
                    systemTextJsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vdn.codemaze.hateoas+json");
                    systemTextJsonOutputFormatter.SupportedMediaTypes
                    .Add("application/vdn.codemaze.apiroot+json");
                }
            });
        }

        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-verion");
                opt.Conventions.Controller<CompanyController>()
                    .HasApiVersion(new ApiVersion(1, 0));
                opt.Conventions.Controller<CompaniesV2Controller>()
                    .HasApiVersion(new ApiVersion(2, 0));
            });
        }
    }
}
