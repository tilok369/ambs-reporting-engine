using Ambs.Reporting.Engine.Model;
namespace Ambs.Reporting.Engine.Manager
{
    public interface IReportingEngine
    {
        ExportData GetExportData(ReportData data);
        List<ExportData> GetExportData(List<ReportData> datas);
        byte[] GetExcelData(List<ExportData> datas,string fileName);
        byte[] GetPdflData(List<ExportData> datas,string fileName);
    }
}
