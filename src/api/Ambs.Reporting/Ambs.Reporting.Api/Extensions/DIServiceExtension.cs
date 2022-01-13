using Ambs.Reporting.Service.Implementations;
using Ambs.Reporting.Service.Interfaces;
using Ambs.Reporting.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Ambs.Reporting.Logic.Interfaces;
using Ambs.Reporting.Logic.Implementations;
using Ambs.Reporting.DAL.Repository.Implementations;
using Ambs.Reporting.DAL.Repository.Interfaces;
using Ambs.Reporting.Engine.Manager;

namespace Ambs.Reporting.Api.Extensions
{
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
                .AddScoped<IReportRepository, ReportRepository>();
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services
                .AddScoped<IReportService, ReportService>();
        }
        public static IServiceCollection AddLogics(this IServiceCollection services)
        {
            return services
                .AddScoped<IReportLogic, ReportLogic>()
                .AddScoped<IReportingEngine, ReportingEngine>();
        }
    }
}
