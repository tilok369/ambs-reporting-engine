using Ambs.Reporting.Engine.Model;
using System.Data;
using Microsoft.Data.SqlClient;
using Ambs.Reporting.Engine.Manager;
using static Ambs.Reporting.Utility.Enum.ReportEnum;
using Ambs.Reporting.Utility.Extensions;

namespace Ambs.Reporting.Logic.Implementations;
public class ReportExportLogic : IReportExportLogic
{
    private readonly IReportExportService _exportReportService;
    private readonly IReportingEngine _reportingEngine;
    private readonly IExporter _exporter;
    private readonly IReportService _reportService;
    private readonly ITablularFeatureService _tabularFeatureService;
    private readonly IGraphicalFeatureService _graphicalFeatureService;
    public ReportExportLogic(IReportExportService exportReportService
        , IReportingEngine reportingEngine
        , IExporter exporter
        ,IReportService reportService
        , ITablularFeatureService tabularFeatureService
        ,IGraphicalFeatureService graphicalFeatureService
        )
    {
        _exportReportService = exportReportService;
        _reportingEngine = reportingEngine;
        _exporter = exporter;
        _reportService = reportService;
        _tabularFeatureService = tabularFeatureService;
        _graphicalFeatureService = graphicalFeatureService;
    }
    private static string ConstructCommand(string script, Dictionary<string, string> parameters)
    {
        script = script.ToLower();
        foreach (var p in parameters)
        {
            script = script.Replace(p.Key.ToLower(), p.Value);
        }
        return script;
    }
    public async Task<List<ExportData>> GetReportData(long reportId,string paramVals)
    {
        //var commandText = "SELECT [Id],[P_ProgramTypeId] as ProgramTypeId,[ShortName],[Name],[Description],[IsPrimary],[IsLongTerm],[IsCollectionSheet],[StartingDate],[EndingDate],[SortOrder],[IgnoreHoliday] FROM P_Program";
        //var paramBranchId = new SqlParameter("@BranchId", 2);
        //var paramDate = new SqlParameter("@Date", new DateTime(2021, 09, 10));
        //var (columns, rows) = await _reportService.GetReportData(commandText, CommandType.Text);
        //var ambsReportDataReceiveAndPayment = new ReportData { Columns = columns, Rows = rows };
        var report=_reportService.Get(reportId);
        var script=(ReportType)report.Type==ReportType.Tabular ? _tabularFeatureService.GetByReportId(reportId).Script : _graphicalFeatureService.GetByReportId(reportId).Script;
        //var commandText = "dbo.P_TransactionSummaryDailyReceiveAndPayment";
        //var paramBranchId = new SqlParameter("@BranchId", 2);
        //var paramDate = new SqlParameter("@Date", new DateTime(2021, 09, 10));
        var dbScript = ConstructCommand(script, paramVals.ToDictionary());
        var (columns, rows) = await _exportReportService.GetReportData(dbScript, CommandType.Text);
        var ambsReportDataReceiveAndPayment = new ReportData { Columns = columns, Rows = rows };
        //commandText = "dbo.P_TransactionSummaryLoanDisbursedAndFullPaid";
        //(columns, rows) = await _exportReportService.GetReportData(commandText, CommandType.StoredProcedure, new[] { paramBranchId, paramDate });
        //var ambsReportDataLoanDisburseAndFullPaid = new ReportData { Columns = columns, Rows = rows };
        return await _reportingEngine.GetExportData(new List<ReportData> { ambsReportDataReceiveAndPayment});
    }

    public async Task<byte[]> GetReportDataForExport(ExportType exportType,string contentRootPath)
    {
        var commandText = "dbo.P_TransactionSummaryDailyReceiveAndPayment";
        var paramBranchId = new SqlParameter("@BranchId", 2);
        var paramDate = new SqlParameter("@Date", new DateTime(2021, 09, 10));
        var (columns, rows) = await _exportReportService.GetReportData(commandText, CommandType.StoredProcedure, new[] { paramBranchId, paramDate });
        var ambsReportDataReceiveAndPayment = new ReportData { Columns = columns, Rows = rows };
        commandText = "dbo.P_TransactionSummaryLoanDisbursedAndFullPaid";
        (columns, rows) = await _exportReportService.GetReportData(commandText, CommandType.StoredProcedure, new[] { paramBranchId, paramDate });
        var ambsReportDataLoanDisburseAndFullPaid = new ReportData { Columns = columns, Rows = rows };
        var exportData =await _reportingEngine.GetExportData(new List<ReportData> { ambsReportDataReceiveAndPayment, ambsReportDataLoanDisburseAndFullPaid });
        return await(exportType == ExportType.Excel ? _exporter.GetExcelData(exportData, "Test", contentRootPath) : _exporter.GetPdfData(exportData, "Test", contentRootPath));
    }


    //public async Task<byte[]> GetReportExport(string fileName)
    //{
    //    return await _exporter.ReportExport(fileName);
    //}


}

