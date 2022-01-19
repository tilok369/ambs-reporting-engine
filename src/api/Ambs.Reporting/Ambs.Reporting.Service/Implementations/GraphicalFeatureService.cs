namespace Ambs.Reporting.Service.Implementations;

public class GraphicalFeatureService : IGraphicalFeatureService
{
    private readonly IGenericRepository _genericRepository;
    public GraphicalFeatureService(IGenericRepository genericRepository)
    {
        _genericRepository = genericRepository;
    }
    public GraphicalFeature Delete(long id)
    {
        return _genericRepository.Delete<GraphicalFeature>(id);
    }

    public GraphicalFeature GetByReportId(long reportId)
    {
        return _genericRepository.First<GraphicalFeature>(gf => gf.ReportId == reportId);
    }

    public GraphicalFeature Add(GraphicalFeature graphicalFeature)
    {
        return _genericRepository.Add(graphicalFeature);
    }

    public GraphicalFeature Edit(GraphicalFeature graphicalFeature)
    {
        return _genericRepository.Edit(graphicalFeature);
    }

    public List<GraphicalFeature> GetAllByReportId(long reportId)
    {
        return _genericRepository.Find<GraphicalFeature>(gf => gf.ReportId == reportId).ToList();
    }

    public bool DeleteByReportId(long reportId)
    {
        return _genericRepository.DeleteByProperty<GraphicalFeature>(gf=>gf.ReportId == reportId);
    }
}

