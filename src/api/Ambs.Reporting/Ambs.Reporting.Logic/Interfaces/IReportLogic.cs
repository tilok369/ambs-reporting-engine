namespace Ambs.Reporting.Logic.Interfaces;
public interface IReportLogic
{
    Task<byte[]> GetReportData(ExportType exportType, string contentRootPath);
    Task<byte[]> GetReportExport(string fileName);
}
