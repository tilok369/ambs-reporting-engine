using Ambs.Reporting.Engine.Model;

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
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddScoped<IApplicationConfigurationManager, ApplicationConfigurationManager>()
            .AddScoped<IGenericRepository, GenericRepository>()
            .AddScoped<IReportExportRepository, ReportExportRepository>()
            .AddScoped<IReportRepository,ReportRepository>()
            .AddScoped<IFilterRepository,FilterRepository>()
            .AddScoped<IMetaDataRepository,MetaDataRepository>()
            .AddScoped<IInvalidateCacheRepository>(s=>new InvalidateCacheRepository(string.Empty))
            .AddScoped<IGenericCacheRepository<ReportData>>(s=>new GenericCacheRepository<ReportData>(string.Empty));
    }
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<IReportExportService, ReportExportService>()
            .AddScoped<IDashboardService, DashboardService>()
            .AddScoped<IMetaDataService, MetaDataService>()
            .AddScoped<IWidgetService, WidgetService>()
            .AddScoped<IReportService, ReportService>()
            .AddScoped<IFilterService, FilterService>()
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
            .AddScoped<IFilterLogic, FilterLogic>()
            .AddScoped<IWidgetLogic, WidgetLogic>()
            .AddScoped<IReportLogic,ReportLogic>()
            .AddScoped<IGraphicalFeatureLogic, GraphicalFeatureLogic>();
    }
}
