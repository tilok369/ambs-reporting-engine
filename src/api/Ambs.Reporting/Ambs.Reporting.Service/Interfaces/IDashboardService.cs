
namespace Ambs.Reporting.Service.Interfaces;

public interface IDashboardService
{
    IEnumerable<Dashboard> GetAll();
    Dashboard Get(long id);
    Dashboard Save(Dashboard dashboard);
}
