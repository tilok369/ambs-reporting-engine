using Ambs.Reporting.Engine.Model;

namespace Ambs.Reporting.Logic.Interfaces;
public interface IReportExportLogic
{
    Task<byte[]> GetReportDataForExport(ExportType exportType, string contentRootPath);
    Task<List<ExportData>> GetReportData();
    //Task<byte[]> GetReportExport(string fileName);
}
