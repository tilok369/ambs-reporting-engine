using Ambs.Reporting.Engine.Model;

namespace Ambs.Reporting.Engine.Manager;

public interface IExporter
{
    byte[] GetExcelData(List<ExportData> datas, string fileName);
    byte[] GetPdfData(List<ExportData> datas, string fileName);
    byte[] ReportExport(string fileName);
}
