using Ambs.Reporting.Engine.Model;

namespace Ambs.Reporting.Engine.Manager;

public interface IExporter
{
    Task<byte[]> GetExcelData(List<ExportData> datas, string fileName);
    Task<byte[]> GetPdfData(List<ExportData> datas, string fileName);
    Task<byte[]> ReportExport(string fileName);
}
