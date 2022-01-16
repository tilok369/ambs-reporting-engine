using Microsoft.Data.SqlClient;
using System.Data;

namespace Ambs.Reporting.Service.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<(List<string>, List<List<string>>)> GetReportData(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            return await _reportRepository.GetReportData(commandText, commandType, parameters);
        }
    }
}
