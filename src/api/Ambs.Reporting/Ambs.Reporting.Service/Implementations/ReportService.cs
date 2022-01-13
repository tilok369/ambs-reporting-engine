using Ambs.Reporting.DAL.Repository.Interfaces;
using Ambs.Reporting.Engine.Model;
using Ambs.Reporting.Service.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
