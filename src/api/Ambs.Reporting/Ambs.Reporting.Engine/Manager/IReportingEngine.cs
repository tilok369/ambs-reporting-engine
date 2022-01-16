using Ambs.Reporting.Engine.Model;
namespace Ambs.Reporting.Engine.Manager;

public interface IReportingEngine
{
    Task<ExportData> GetExportData(ReportData data);
    Task<List<ExportData>> GetExportData(List<ReportData> datas);
}

