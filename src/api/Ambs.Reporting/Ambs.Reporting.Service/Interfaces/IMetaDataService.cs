
namespace Ambs.Reporting.Service.Interfaces;

public interface IMetaDataService
{
    IEnumerable<MetaDatum> GetAll();
    MetaDatum Get(long id);
    MetaDatum Save(MetaDatum dashboard);
    MetaDatum GetMetadataByDashboard(long dashboardId);
}
