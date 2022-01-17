namespace Ambs.Reporting.Api.Extensions;

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
            .AddScoped<IReportExportRepository, ReportExportRepository>()
            .AddScoped<IReportRepository,ReportRepository>();
    }
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IReportExportService, ReportExportService>()
            .AddScoped<IDashboardService, DashboardService>()
            .AddScoped<IMetaDataService, MetaDataService>()
            .AddScoped<IReportService, ReportService>()
            .AddScoped<IReportFilterService, ReportFilterService>()
            .AddScoped<ITablularFeatureService, TabularFeatureService>()
            .AddScoped<IGraphicalFeatureService, GraphicalFeatureService>();
    }
    public static IServiceCollection AddLogics(this IServiceCollection services)
    {
        return services
            .AddScoped<IReportExportLogic, ReportExportLogic>()
            .AddScoped<IExporter, Exporter>()
            .AddScoped<IDashboardLogic, DashboardLogic>()
            .AddScoped<IMetaDataLogic, MetaDataLogic>()
            .AddScoped<IReportingEngine, ReportingEngine>()
            .AddScoped<IReportLogic,ReportLogic>();
    }
}
