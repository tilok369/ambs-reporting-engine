
namespace Ambs.Reporting.Service.Interfaces;

public interface IMetaDataService
{
    IEnumerable<MetaDatum> GetAll();
    MetaDatum Get(int id);
    MetaDatum Save(MetaDatum dashboard);
}
