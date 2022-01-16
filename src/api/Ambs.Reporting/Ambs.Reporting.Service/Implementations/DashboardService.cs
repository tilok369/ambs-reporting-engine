
namespace Ambs.Reporting.Service.Implementations;

public class DashboardService : IDashboardService
{
    private readonly IGenericRepository _genericRepository;

    public DashboardService(IGenericRepository genericRepository)
    {
        this._genericRepository = genericRepository;
    }

    public Dashboard Get(long id)
    {
        return _genericRepository.Get<Dashboard>(id);
    }

    public IEnumerable<Dashboard> GetAll()
    {
        return _genericRepository.GetAll<Dashboard>();
    }

    public Dashboard Save(Dashboard dashboard)
    {
        if(dashboard.Id == 0)
            return _genericRepository.Add<Dashboard>(dashboard);

        return _genericRepository.Edit<Dashboard>(dashboard);
    }
}
