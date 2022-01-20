using Ambs.Reporting.DAL.CalculativeModels;
using Ambs.Reporting.ViewModel.Reponse.Report;

namespace Ambs.Reporting.Service.Implementations;
public class ReportService : IReportService
{
    private readonly IGenericRepository _genericRepository;
    private readonly IReportRepository _reportRepository;
    public ReportService(IGenericRepository genericRepository
        , IReportRepository reportRepository)
    {
        _genericRepository= genericRepository;
        _reportRepository = reportRepository;
    }
    public Report Get(long id)
    {
        return _genericRepository.Get<Report>(id);
    }

    public IEnumerable<ReportList> GetAll(int page,int size)
    {
        return _reportRepository.GetAll(page,size);
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

    public IEnumerable<Report> GetByWidgetId(long widgetId)
    {
        return _genericRepository.Find<Report>(rp => rp.WidgetId == widgetId);
    }
}
