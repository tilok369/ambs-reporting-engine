namespace Ambs.Reporting.Service.Implementations;
public class ReportFilterService : IReportFilterService
{
    private readonly IGenericRepository _repository;
    public ReportFilterService(IGenericRepository repository)
    {
        _repository = repository;
    }
    public bool AddAll(List<ReportFilter> filters)
    {
        return _repository.AddAll(filters);
    }

    public bool DeleteAll(List<ReportFilter> filters)
    {
        return _repository.DeleteAll(filters);
    }

    public bool DeleteByReportId(long reportId)
    {
        return _repository.DeleteByProperty<ReportFilter>(rf=>rf.ReportId == reportId);
    }

    public IEnumerable<ReportFilter> GetReportFiltersByReportId(long reportId)
    {
        return _repository.Find<ReportFilter>(rf=>rf.ReportId == reportId);
    }
}

