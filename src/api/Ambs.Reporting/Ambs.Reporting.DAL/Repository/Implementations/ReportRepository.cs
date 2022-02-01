using Ambs.Reporting.DAL.CalculativeModels;

namespace Ambs.Reporting.DAL.Repository.Implementations;

public class ReportRepository : IReportRepository
{
    private readonly DbContextOptionsBuilder<ReportEngineContext> _dbContextOptionBuilder;
    public ReportRepository(IApplicationConfigurationManager applicationConfigurationManager)
    {
        _dbContextOptionBuilder = new DbContextOptionsBuilder<ReportEngineContext>();
        _dbContextOptionBuilder.UseSqlServer(applicationConfigurationManager.GetConnectionString());
    }

    public IEnumerable<ReportList> GetAll(int page, int size)
    {
        using var context = new ReportEngineContext(_dbContextOptionBuilder.Options);
        return (from report in context.Reports
                join widget in context.Widgets on report.WidgetId equals widget.Id
                join dashboard in context.Dashboards on widget.DashboardId equals dashboard.Id
                select new ReportList(report.Id, report.Name, (bool)report.Status, (Utility.Enum.ReportEnum.ReportType)report.Type, widget.Name, dashboard.Name)
                ).ToList().Take(page..size);
    }

    public IEnumerable<ReportList> GetByWidget(long widgetId, int page, int size)
    {
        using var context = new ReportEngineContext(_dbContextOptionBuilder.Options);
        return (from report in context.Reports
                join widget in context.Widgets on report.WidgetId equals widget.Id
                join dashboard in context.Dashboards on widget.DashboardId equals dashboard.Id
                where report.WidgetId == widgetId
                select new ReportList(report.Id, report.Name, (bool)report.Status, (Utility.Enum.ReportEnum.ReportType)report.Type, widget.Name, dashboard.Name)
                ).ToList().Take(page..size);
    }
}

