namespace Ambs.Reporting.Service.Implementations;
public class ReportService : IReportService
{
    private readonly IGenericRepository _genericRepository;
    public ReportService(IGenericRepository genericRepository)
    {
        _genericRepository= genericRepository;
    }
    public Report Get(long id)
    {
        return _genericRepository.Get<Report>(id);
    }

    public IEnumerable<Report> GetAll()
    {
        return _genericRepository.GetAll<Report>();
    }

    public Report Add(Report report)
    {
        return _genericRepository.Add(report);
    }

    public Report Edit(Report report)
    {
        return _genericRepository.Edit(report);
    }

    public Report Delete(long id)
    {
        return _genericRepository.Delete<Report>(id);
    }
}
