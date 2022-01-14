
namespace Ambs.Reporting.Service.Implementations;

public class MetaDataService : IMetaDataService
{
    private readonly IGenericRepository _genericRepository;

    public MetaDataService(IGenericRepository genericRepository)
    {
        this._genericRepository = genericRepository;
    }

    public MetaDatum Get(int id)
    {
        return _genericRepository.Get<MetaDatum>(id);
    }

    public IEnumerable<MetaDatum> GetAll()
    {
        return _genericRepository.GetAll<MetaDatum>();
    }

    public MetaDatum Save(MetaDatum dashboard)
    {
        if (dashboard.Id == 0)
            return _genericRepository.Add<MetaDatum>(dashboard);

        return _genericRepository.Edit<MetaDatum>(dashboard);
    }
}
