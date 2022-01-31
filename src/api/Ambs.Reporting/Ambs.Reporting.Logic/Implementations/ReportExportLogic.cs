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
    private readonly IMetaDataLogic _metaDataLogic;
    public ReportExportLogic(IReportExportService exportReportService
        , IReportingEngine reportingEngine
        , IExporter exporter
        ,IReportService reportService
        , ITablularFeatureService tabularFeatureService
        ,IGraphicalFeatureService graphicalFeatureService
        ,IMetaDataLogic metaDataLogic
        )
    {
        _exportReportService = exportReportService;
        _reportingEngine = reportingEngine;
        _exporter = exporter;
        _reportService = reportService;
        _tabularFeatureService = tabularFeatureService;
        _graphicalFeatureService = graphicalFeatureService;
        _metaDataLogic = metaDataLogic;
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
        var report=_reportService.Get(reportId);
        var script=(ReportType)report.Type==ReportType.Tabular ? _tabularFeatureService.GetByReportId(reportId).Script : _graphicalFeatureService.GetByReportId(reportId).Script;
        var dbScript = ConstructCommand(script, paramVals.ToDictionary());
        var metaData = _metaDataLogic.GetMetadataByReportId(reportId);
        if(metaData != null)
        {
            var (columns, rows) = await _exportReportService.GetReportData(dbScript, CommandType.Text, metaData.DataSource);
            var reportData = new ReportData { Columns = columns, Rows = rows, ReportName = report.Name };
            return await _reportingEngine.GetExportData(new List<ReportData> { reportData });
        }
        return null;
        
    }

    public async Task<byte[]> GetReportDataForExport(long reportId, string paramVals,ExportType exportType,string contentRootPath)
    {
        var exportData = await GetReportData(reportId,paramVals);
        return await(exportType == ExportType.Excel ? _exporter.GetExcelData(exportData, "Test", contentRootPath) : _exporter.GetPdfData(exportData, "Test", contentRootPath));
    }
}

