namespace Ambs.Reporting.DAL.Repository.Implementations;
public class MetaDataRepository : IMetaDataRepository
{
    private readonly DbContextOptionsBuilder<ReportEngineContext> _dbContextOptionBuilder;
    public MetaDataRepository(IApplicationConfigurationManager applicationConfigurationManager)
    {
        _dbContextOptionBuilder = new DbContextOptionsBuilder<ReportEngineContext>();
        _dbContextOptionBuilder.UseSqlServer(applicationConfigurationManager.GetConnectionString());
    }
    public MetaDatum GetMetaDatumByReport(long reportId)
    {
        using var context = new ReportEngineContext(_dbContextOptionBuilder.Options);
        return (from metaData in context.MetaData
                join dashboard in context.Dashboards on metaData.DashboardId equals dashboard.Id
                join widget in context.Widgets on dashboard.Id equals widget.Id
                join report in context.Reports on widget.Id equals report.WidgetId
                where report.Id == reportId
                select metaData).FirstOrDefault();
    }
}

