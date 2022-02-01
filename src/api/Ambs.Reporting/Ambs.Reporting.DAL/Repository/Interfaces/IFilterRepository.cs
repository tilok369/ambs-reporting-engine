using Ambs.Reporting.DAL.CalculativeModels;

namespace Ambs.Reporting.DAL.Repository.Interfaces;

public interface IFilterRepository
{
    IEnumerable<DropdownFilterCM> GetDrowpdownFilterValues(string script, string connectionString);
    IEnumerable<Filter> GetByReportId(long reportId);
}

