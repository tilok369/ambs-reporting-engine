using Ambs.Reporting.Engine.GraphModels;
using Ambs.Reporting.Engine.Manager;
using Ambs.Reporting.Logic.Factories;
using Ambs.Reporting.Utility.Extensions;
using Ambs.Reporting.Utility.Globals;
using Microsoft.Data.SqlClient;

namespace Ambs.Reporting.Logic.Implementations;

public class GraphicalFeatureLogic : IGraphicalFeatureLogic
{
    private readonly IGraphicalFeatureService _graphicalFeatureService;
    private readonly IReportExportService _reportExportService;
    private readonly IExporter _exporter;
    private readonly IMetaDataLogic _metaDataLogic;

    public GraphicalFeatureLogic(IGraphicalFeatureService graphicalFeatureService,
        IReportExportService reportExportService,
        IExporter exporter
        , IMetaDataLogic metaDataLogic)
    {
        _graphicalFeatureService = graphicalFeatureService;
        _reportExportService = reportExportService;
        _exporter = exporter;
        _metaDataLogic = metaDataLogic;
    }

    public IGraph GetByReport(long reportId, string parameterVals)
    {
        var gf = _graphicalFeatureService.GetByReportId(reportId);
        if (gf == null) return null;
        var (columns, rows) = _reportExportService.GetReportData(Helper.ConstructCommand(gf.Script, parameterVals.ToDictionary()), System.Data.CommandType.Text, _metaDataLogic.GetMetadataByReportId(reportId)?.DataSource, new SqlParameter[] { }).GetAwaiter().GetResult();


        var graph = new GraphFactory().GetGraph(gf.GraphType);      
        graph.SetDataPoints(columns, rows);

        graph.Title = gf.Title??"";
        graph.SubTitle = gf.SubTitle ?? "";
        graph.ShowLegend = gf.ShowLegend ?? false;
        graph.XaxisSuffix = gf.XaxisSuffix ?? "";
        graph.YaxisSuffix = gf.YaxisSuffix ?? "";
        graph.XaxisPrefix = gf.XaxisPrefix ?? "";
        graph.YaxisPrefix = gf.YaxisPrefix ?? "";
        graph.XaxisTitle = gf.XaxisTitle ?? "";
        graph.YaxisTitle = gf.YaxisTitle ?? "";

        return graph;
    }

    public async Task<byte[]> GetReportExport(string fileName, long reportId, string parameterVals)
    {
        var gf = _graphicalFeatureService.GetByReportId(reportId);
        if (gf == null) return null;

        var (columns, rows) = _reportExportService.GetReportData(Helper.ConstructCommand(gf.Script, parameterVals.ToDictionary()), System.Data.CommandType.Text,_metaDataLogic.GetMetadataByReportId(reportId)?.DataSource, new SqlParameter[] { }).GetAwaiter().GetResult();

        var graph = new GraphFactory().GetGraph(gf.GraphType);
        graph.SetDataPoints(columns, rows);

        graph.Title = gf.Title ?? "";
        graph.SubTitle = gf.SubTitle ?? "";
        graph.ShowLegend = gf.ShowLegend ?? false;
        graph.XaxisSuffix = gf.XaxisSuffix ?? "";
        graph.YaxisSuffix = gf.YaxisSuffix ?? "";
        graph.YaxisPrefix = gf.YaxisPrefix ?? "";
        graph.XaxisSuffix = gf.XaxisSuffix ?? "";
        graph.YaxisTitle = gf.YaxisTitle ?? "";
        graph.XaxisTitle = gf.XaxisTitle ?? "";

        return await _exporter.ReportExport(fileName, graph);
    }
}