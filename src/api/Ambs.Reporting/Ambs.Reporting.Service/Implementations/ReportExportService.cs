using Microsoft.Data.SqlClient;
using System.Data;
namespace Ambs.Reporting.Service.Implementations;
public class ReportExportService : IReportExportService
{
    private readonly IReportExportRepository _reportRepository;
    public ReportExportService(IReportExportRepository reportRepository)
    {
        _reportRepository = reportRepository;
    }

    public async Task<(List<string>, List<List<string>>)> GetReportData(string commandText, CommandType commandType, string connectionString, SqlParameter[] parameters)
    {
        return await _reportRepository.GetReportData(commandText, commandType, connectionString, parameters);
    }
}
