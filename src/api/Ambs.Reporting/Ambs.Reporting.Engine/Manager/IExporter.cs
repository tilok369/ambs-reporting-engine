using Ambs.Reporting.Engine.GraphModels;
using Ambs.Reporting.Engine.Model;

namespace Ambs.Reporting.Engine.Manager;

public interface IExporter
{
    Task<byte[]> GetExcelData(List<ExportData> datas, string fileName,string contentRootPath);
    Task<byte[]> GetPdfData(List<ExportData> datas, string fileName, string contentRootPath);
    Task<byte[]> ReportExport(string fileName, IGraph graph);
}
