namespace Ambs.Reporting.Service.Implementations;

public class TabularFeatureService : ITablularFeatureService
{
    private readonly IGenericRepository _genericRepository;
    public TabularFeatureService(IGenericRepository genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public TabularFeature Delete(long id)
    {
        return _genericRepository.Delete<TabularFeature>(id);
    }

    public TabularFeature GetByReportId(long reportId)
    {
        return _genericRepository.First<TabularFeature>(t=>t.ReportId == reportId);
    }

    public TabularFeature Add(TabularFeature tabularFeature)
    {
        return _genericRepository.Add<TabularFeature>(tabularFeature);
    }

    public TabularFeature Edit(TabularFeature tabularFeature)
    {
        return _genericRepository.Edit<TabularFeature>(tabularFeature);
    }

    public bool DeleteByReportId(long reportId)
    {
        return _genericRepository.DeleteByProperty<TabularFeature>(tf=>tf.ReportId == reportId);
    }
}

