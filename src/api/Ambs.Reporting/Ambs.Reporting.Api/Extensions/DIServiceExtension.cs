﻿namespace Ambs.Reporting.Api.Extensions;

public static class DIServiceExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ReportEngineContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("ReportDb"));
        }
        );
        return services;
    }
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<IApplicationConfigurationManager, ApplicationConfigurationManager>()
            .AddScoped<IGenericRepository, GenericRepository>()
            .AddScoped<IReportRepository, ReportRepository>();
    }
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IReportService, ReportService>()
            .AddScoped<IDashboardService, DashboardService>()
            .AddScoped<IMetaDataService, MetaDataService>()
            .AddScoped<IFilterService, FilterService>();
    }
    public static IServiceCollection AddLogics(this IServiceCollection services)
    {
        return services
            .AddScoped<IReportLogic, ReportLogic>()
            .AddScoped<IExporter, Exporter>()
            .AddScoped<IDashboardLogic, DashboardLogic>()
            .AddScoped<IMetaDataLogic, MetaDataLogic>()
            .AddScoped<IReportingEngine, ReportingEngine>()
            .AddScoped<IFilterLogic, FilterLogic>();

    }
}
