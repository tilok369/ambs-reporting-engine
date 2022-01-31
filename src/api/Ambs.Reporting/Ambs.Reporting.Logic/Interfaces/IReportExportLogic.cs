using Ambs.Reporting.Engine.Model;

namespace Ambs.Reporting.Logic.Interfaces;
public interface IReportExportLogic
{
    Task<byte[]> GetReportDataForExport(long reportId, string paramVals, ExportType exportType, string contentRootPath);
    Task<List<ExportData>> GetReportData(long reportId,string paramVals);
    //Task<byte[]> GetReportExport(string fileName);
}
