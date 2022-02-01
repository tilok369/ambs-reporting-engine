using Ambs.Reporting.DAL.CalculativeModels;

namespace Ambs.Reporting.DAL.Repository.Interfaces;
public interface IReportRepository
{
    IEnumerable<ReportList> GetAll(int page, int size);
    IEnumerable<ReportList> GetByWidget(long widgetId, int page, int size);
}

