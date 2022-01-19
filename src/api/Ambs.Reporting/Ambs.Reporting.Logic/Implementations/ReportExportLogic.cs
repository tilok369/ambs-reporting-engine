using Ambs.Reporting.Engine.Model;
using System.Data;
using Microsoft.Data.SqlClient;
using Ambs.Reporting.Engine.Manager;

namespace Ambs.Reporting.Logic.Implementations;
public class ReportExportLogic : IReportExportLogic
{
    private readonly IReportExportService _reportService;
    private readonly IReportingEngine _reportingEngine;
    private readonly IExporter _exporter;
    public ReportExportLogic(IReportExportService reportService
        , IReportingEngine reportingEngine
        , IExporter exporter)
    {
        _reportService = reportService;
        _reportingEngine = reportingEngine;
        _exporter = exporter;
    }

    public async Task<List<ExportData>> GetReportData()
    {
        //var commandText = "SELECT [Id],[P_ProgramTypeId] as ProgramTypeId,[ShortName],[Name],[Description],[IsPrimary],[IsLongTerm],[IsCollectionSheet],[StartingDate],[EndingDate],[SortOrder],[IgnoreHoliday] FROM P_Program";
        //var paramBranchId = new SqlParameter("@BranchId", 2);
        //var paramDate = new SqlParameter("@Date", new DateTime(2021, 09, 10));
        //var (columns, rows) = await _reportService.GetReportData(commandText, CommandType.Text);
        //var ambsReportDataReceiveAndPayment = new ReportData { Columns = columns, Rows = rows };
        var commandText = "dbo.P_TransactionSummaryDailyReceiveAndPayment";
        var paramBranchId = new SqlParameter("@BranchId", 2);
        var paramDate = new SqlParameter("@Date", new DateTime(2021, 09, 10));
        var (columns, rows) = await _reportService.GetReportData(commandText, CommandType.StoredProcedure, new[] { paramBranchId, paramDate });
        var ambsReportDataReceiveAndPayment = new ReportData { Columns = columns, Rows = rows };
        //commandText = "dbo.P_TransactionSummaryLoanDisbursedAndFullPaid";
        //(columns, rows) = await _reportService.GetReportData(commandText, CommandType.StoredProcedure, new[] { paramBranchId, paramDate });
        //var ambsReportDataLoanDisburseAndFullPaid = new ReportData { Columns = columns, Rows = rows };
        return await _reportingEngine.GetExportData(new List<ReportData> { ambsReportDataReceiveAndPayment });
    }

    public async Task<byte[]> GetReportDataForExport(ExportType exportType,string contentRootPath)
    {
        var commandText = "dbo.P_TransactionSummaryDailyReceiveAndPayment";
        var paramBranchId = new SqlParameter("@BranchId", 2);
        var paramDate = new SqlParameter("@Date", new DateTime(2021, 09, 10));
        var (columns, rows) = await _reportService.GetReportData(commandText, CommandType.StoredProcedure, new[] { paramBranchId, paramDate });
        var ambsReportDataReceiveAndPayment = new ReportData { Columns = columns, Rows = rows };
        commandText = "dbo.P_TransactionSummaryLoanDisbursedAndFullPaid";
        (columns, rows) = await _reportService.GetReportData(commandText, CommandType.StoredProcedure, new[] { paramBranchId, paramDate });
        var ambsReportDataLoanDisburseAndFullPaid = new ReportData { Columns = columns, Rows = rows };
        var exportData =await _reportingEngine.GetExportData(new List<ReportData> { ambsReportDataReceiveAndPayment, ambsReportDataLoanDisburseAndFullPaid });
        return await(exportType == ExportType.Excel ? _exporter.GetExcelData(exportData, "Test", contentRootPath) : _exporter.GetPdfData(exportData, "Test", contentRootPath));
    }


    //public async Task<byte[]> GetReportExport(string fileName)
    //{
    //    return await _exporter.ReportExport(fileName);
    //}


}

