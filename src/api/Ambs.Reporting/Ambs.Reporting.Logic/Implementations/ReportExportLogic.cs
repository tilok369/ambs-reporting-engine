using Ambs.Reporting.Engine.Model;
using System.Data;
using Microsoft.Data.SqlClient;
using Ambs.Reporting.Engine.Manager;
using static Ambs.Reporting.Utility.Enum.ReportEnum;
using Ambs.Reporting.Utility.Extensions;
using Ambs.Reporting.ViewModel.Reponse;
using Ambs.Reporting.DAL.Repository.Interfaces;

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
    private readonly IGenericCacheRepository<ReportData> _genericCacheRepository;
    public ReportExportLogic(IReportExportService exportReportService
        , IReportingEngine reportingEngine
        , IExporter exporter
        ,IReportService reportService
        , ITablularFeatureService tabularFeatureService
        ,IGraphicalFeatureService graphicalFeatureService
        ,IMetaDataLogic metaDataLogic
        , IGenericCacheRepository<ReportData> genericCacheRepository
        )
    {
        _exportReportService = exportReportService;
        _reportingEngine = reportingEngine;
        _exporter = exporter;
        _reportService = reportService;
        _tabularFeatureService = tabularFeatureService;
        _graphicalFeatureService = graphicalFeatureService;
        _metaDataLogic = metaDataLogic;
        _genericCacheRepository = genericCacheRepository;
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
    private string GetCustomCacheKey(string reportName,string paramVals)
    {
        return reportName+ ":" + paramVals;
    }
    public async Task<List<ExportData>> GetReportData(long dasboardId, long reportId,string paramVals)
    {
        var report=_reportService.Get(reportId);
        if (report.IsCacheEnable && _genericCacheRepository.Ping() && _genericCacheRepository.Exists(GetCustomCacheKey(report.Name, paramVals)))
        {
            var data = _genericCacheRepository.GetByKey(GetCustomCacheKey(report.Name, paramVals));
            if (data != null)
                if ((DateTime.Now - data.ReportCacheTime).Minutes <= report.CacheAliveTime)
                    return await _reportingEngine.GetExportData(new List<ReportData> { data });
                else _genericCacheRepository.Delete(GetCustomCacheKey(report.Name, paramVals));
        }

        var script=(ReportType)report.Type==ReportType.Tabular ? _tabularFeatureService.GetByReportId(reportId).Script : _graphicalFeatureService.GetByReportId(reportId).Script;
        var dbScript = ConstructCommand(script, paramVals.ToDictionary());      
            
        var metaData = _metaDataLogic.GetMetadataByDashboard(dasboardId);        
        if(metaData != null)
        {            
            var (columns, rows) = await _exportReportService.GetReportData(dbScript, CommandType.Text, metaData.DataSource);
            var reportData = new ReportData { Columns = columns, Rows = rows, ReportName = report.Name,ReportCacheTime= DateTime.Now };
            if(report.IsCacheEnable && _genericCacheRepository.Ping())
                _genericCacheRepository.Add(reportData, GetCustomCacheKey(report.Name, paramVals));
            return await _reportingEngine.GetExportData(new List<ReportData> { reportData });
        }
        return null;
        
    }

    public async Task<byte[]> GetReportDataForExport(long dasboardId, long reportId, string paramVals,ExportType exportType,string filterValues,string contentRootPath)
    {
        var exportData = await GetReportData(dasboardId,reportId, paramVals);
        var reportExportFilters = new List<ReportExportFilter>();
        var sortOrder = 0;
        var metaData = _metaDataLogic.GetMetadataByDashboard(dasboardId);
        foreach (var filterValue in filterValues.ToDictionary())
        {
            sortOrder++;

            reportExportFilters.Add(new ReportExportFilter( filterValue.Key.Replace("@",string.Empty),Uri.UnescapeDataString(filterValue.Value), sortOrder ));
        }
        return await(exportType == ExportType.Excel ? _exporter.GetExcelData(exportData, "Test", contentRootPath, reportExportFilters, metaData?.BrandImage) : _exporter.GetPdfData(exportData, "Test", contentRootPath, reportExportFilters,metaData?.BrandImage));
    }
}

