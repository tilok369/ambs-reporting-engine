
namespace Ambs.Reporting.Service.Implementations;

public class MetaDataService : IMetaDataService
{
    private readonly IGenericRepository _genericRepository;
    private readonly IMetaDataRepository _metaDataRepository;

    public MetaDataService(IGenericRepository genericRepository
        , IMetaDataRepository metaDataRepository)
    {
        this._genericRepository = genericRepository;
        _metaDataRepository = metaDataRepository;
    }

    public MetaDatum Get(long id)
    {
        return _genericRepository.Get<MetaDatum>(id);
    }

    public IEnumerable<MetaDatum> GetAll()
    {
        return _genericRepository.GetAll<MetaDatum>();
    }

    public MetaDatum GetMetadataByDashboard(long dashboardId)
    {
        return _genericRepository.First<MetaDatum>(md => md.DashboardId == dashboardId);
    }

    public MetaDatum Save(MetaDatum dashboard)
    {
        if (dashboard.Id == 0)
            return _genericRepository.Add<MetaDatum>(dashboard);

        return _genericRepository.Edit<MetaDatum>(dashboard);
    }
}
