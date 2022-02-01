using Ambs.Reporting.Engine.GraphModels;
using Ambs.Reporting.Engine.Model;
using Ambs.Reporting.ViewModel.Reponse;

namespace Ambs.Reporting.Engine.Manager;

public interface IExporter
{
    Task<byte[]> GetExcelData(List<ExportData> datas, string fileName,string contentRootPath, List<ReportExportFilter> reportExportFilters,string image="");
    Task<byte[]> GetPdfData(List<ExportData> datas, string fileName, string contentRootPath, List<ReportExportFilter> reportExportFilters, string image = "");
    Task<byte[]> ReportExport(string fileName, IGraph graph);
}
