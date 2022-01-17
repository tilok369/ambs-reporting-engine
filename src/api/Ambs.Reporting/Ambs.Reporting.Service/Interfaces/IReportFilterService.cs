namespace Ambs.Reporting.Service.Interfaces;
public interface IReportFilterService
{
    IEnumerable<ReportFilter> GetReportFiltersByReportId(long reportId);
    bool AddAll(List<ReportFilter> filters);
    bool DeleteAll(List<ReportFilter> filters);
    bool DeleteByReportId(long reportId);
}

