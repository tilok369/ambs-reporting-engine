using Ambs.Reporting.Engine.Model;
using System.Data;
using Microsoft.Data.SqlClient;
using Ambs.Reporting.Engine.Manager;

namespace Ambs.Reporting.Logic.Implementations
{
    public class ReportLogic : IReportLogic
    {
        private readonly IReportService _reportService;
        private readonly IReportingEngine _reportingEngine;
        public ReportLogic(IReportService reportService
            , IReportingEngine reportingEngine)
        {
            _reportService = reportService;
            _reportingEngine = reportingEngine;
        }
        public async Task<byte[]> GetReportData()
        {
            var commandText = "dbo.P_TransactionSummaryDailyReceiveAndPayment";
            var paramBranchId = new SqlParameter("@BranchId", 2);
            var paramDate = new SqlParameter("@Date", new DateTime(2021,09,10));
            var (columns,rows)=await _reportService.GetReportData(commandText, CommandType.StoredProcedure, new[] {paramBranchId, paramDate });
            var ambsReportData= new ReportData { Columns = columns, Rows = rows };
            var exportData=_reportingEngine.GetExportData(ambsReportData);
            return _reportingEngine.GetExcelData(new List<ExportData> { exportData }, "Test");
        }
    }
}
